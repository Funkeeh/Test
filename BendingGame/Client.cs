using UnityEngine;
using System.Collections;
using System;
using System.Net.Sockets;
using System.Text;

//Client script purpose is to:
//1: Connect Unity to Visual Studio Wrapper program (using local ip and port nr)
//2: Manage Neurostimulator Input (public variables) and convert them into a sinle string to send to wrapper program
//3: Send and recieve data to wrapper program regarding: Neurostim string update, connection status, start stimulation, stop stimulation
public class Client : MonoBehaviour
{
    public static string ip = "127.0.0.1";                                      //Ip adress. 127.0.0.1 = local
    public static int port = 8888;                                              //Port based on server port nr. 
	public static bool serverStatus = false;

    private TcpClient client;                                                       //Use to connect to server

    static private string message;                                                  //What is send to server

    // Buffer for reading data from server
    private Byte[] bytes = new Byte[256];                                           // 256 is the maximum amount of input
    private String dataRecieved = null;                                             // String for data

    //	INPUT TO NEUROSTIMULATOR
    private bool neuroStart = false;                                            //Startstimulator	
    public static bool NeuroDevice = true; //false = DNS //SET AFTUR FALSE											//Choose Device
                                            //public static NeuroDevice neuroDevice;
    public static int neuroChannel;                                                 //Declare Channel Input:  1 - 12
    public static float neuroImpedance;                                                 //Assign Impedance value (in kOhm): 0.1 - 25.5; 0 = maximum 
    public static float neuroVoltage;                                                   //DNS: 2 - 125; HCS: 1 - 410; 0 = Impedance Setting
    public enum NeuroPolarity { cathodic, anodic }                                   //Choose Polarity
    public static NeuroPolarity neuroPolarity;
	public static int 		neuroPulses;													//Choose amount of pulses in a sequence. 0 = Continuous
	public static int 		neuroPulseWidth;												//Width of pulses. Range: 50 - 1000000
	public static float 	neuroFrequency;													//Stimulation Range in Hz: 0.1 - 1000
	public static double 	neuroCurrent;													//Amplitude of stim pulse in mA: DNS: 0.01 - 25; HCS: 0.2 - 250
	public static bool 	neuroBipolar = false;		//skift aftur?											//True: Bipolar, False: Unipolar (Note: DNS can only be unipolar)
	public static bool 	neuroMultichannels = false;	// skift aftur?									//Declare the use of multichannels

	public static uint multichannel_number = 0;
	public static uint[] multichannel_order;                           // Array the order of channels: Eg. {5, 3, 11}
	public static uint[]  multichannel_pulseWdt;                       // Managing the widts of pulses, based on the multichannel_order 
	public static double[] multichannel_currents;                    // Managing the currents, based on the multichannel_order

    //	BOOLS TO VIZUALISE CONNECTION STATUS IN UI --READ ONLY
    public static bool 	wrapperConnection = false;									//Check for connection to wrapper program
	public static bool	neuroConnection = false;									//ISIS Neurostimulator connection status

	// BOOLS TO SEND STIMULATOR SETTINGS AND START/STOP STIMULATION
	public static bool sendStimulatorVariables = false;								//Submit current variables to wrapper
	public static bool startstimulation = false;									//Stimulate when true, stop when false
	public static bool toggle = true;
	private int onlyOnce = 0;
	private bool etellerandet = false;

	void Awake ()
	{
		//client = new TcpClient(ip, port);											//new client connecting to a tcpServer
	}

	void Start () 
	{
        //NeuroDevice = true;
        //neuroChannel = 1;
        //neuroImpedance = 1;
        //neuroVoltage = 1;
        //neuroPolarity = NeuroPolarity.cathodic;
        //neuroPulses = 0;
        //neuroPulseWidth = ;
        //neuroFrequency = 0;
        //neuroCurrent = 0;
        //multichannel_order = new uint[12] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        //multichannel_pulseWdt = new uint[12] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        //multichannel_currents = new double[12] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };


 		//client = new TcpClient(ip, port);											//new client connecting to a tcpServer

		//Initial variables to neurostimulator
		message = SendToServer (NeuroDevice, neuroChannel, neuroImpedance, neuroVoltage, neuroPolarity, neuroPulses, neuroPulseWidth, neuroFrequency, neuroCurrent, neuroBipolar, neuroMultichannels, multichannel_number, multichannel_order, multichannel_pulseWdt, multichannel_currents);

		wrapperConnection = true;
	}

	void Update () 
	{
		if (client.Connected == true) 
		{
			serverStatus = true;
		}

		//Send Information to Server when true
		if (sendStimulatorVariables == true) 
		{

			message = SendToServer (NeuroDevice, neuroChannel, neuroImpedance, neuroVoltage, neuroPolarity, neuroPulses, neuroPulseWidth, neuroFrequency, neuroCurrent, neuroBipolar, neuroMultichannels, multichannel_number, multichannel_order, multichannel_pulseWdt, multichannel_currents);

			try 
			{
				// Translate the passed message into ASCII and store it as a Byte array.
				Byte[] data = System.Text.Encoding.ASCII.GetBytes (message);         

				dataRecieved = null;

				// Get a client stream for reading and writing.
				//  Stream stream = client.GetStream();
				NetworkStream stream = client.GetStream ();  

				// Send the message to the connected TcpServer. 
				stream.Write (data, 0, data.Length); 

				// Receive the server response
				stream.Read(bytes, 0, bytes.Length);
				dataRecieved = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
				Debug.Log(dataRecieved);

				//Check if Server response indicates that Neurostimulator is connected (Checks for 0 or 1)
				if(dataRecieved == "1")
				{
					neuroConnection = true;
				}
				else if (dataRecieved == "0")
				{
					neuroConnection = false;
				}

				wrapperConnection = true;
			}

			catch (SocketException  e) //Null if no data is recieved
			{ 				
				wrapperConnection = false;
				Debug.Log (e + ": No connection to the wrapper program was made.");
			}
		}

		//Tell wrapper program to start stimulating while enter is down
		if (startstimulation == true && toggle == true) 
		{
			string stimulate = "1";
			Debug.Log ("Start Stim");

			try 
			{
				// Translate the passed message into ASCII and store it as a Byte array.
				Byte[] data = System.Text.Encoding.ASCII.GetBytes (stimulate);         

				dataRecieved = null;

				// Get a client stream for reading and writing.
				//  Stream stream = client.GetStream();
				NetworkStream stream = client.GetStream ();  

				// Send the message to the connected TcpServer. 
				stream.Write (data, 0, data.Length); 
			}

			catch (SocketException  e) //Null if no data is recieved
			{ 				
				Debug.Log (e + ": No connection to the wrapper program was made.");
			}
			etellerandet = true;
			toggle = false;
		}

		//Tell wrapper program to stop stimulating while enter is up
		if (startstimulation == false && etellerandet == true) 
		{
			
			string stimulate = "0";
			Debug.Log ("Stop Stim");

			try 
			{
				// Translate the passed message into ASCII and store it as a Byte array.
				Byte[] data = System.Text.Encoding.ASCII.GetBytes (stimulate);         

				dataRecieved = null;

				// Get a client stream for reading and writing.
				//  Stream stream = client.GetStream();
				NetworkStream stream = client.GetStream ();  

				// Send the message to the connected TcpServer. 
				stream.Write (data, 0, data.Length); 
			}

			catch (SocketException  e) //Null if no data is recieved
			{ 				
				Debug.Log (e + ": No connection to the wrapper program was made.");
			}
			etellerandet = false;
			toggle = true;
		}
	}
		
	//Compile Stimulator input variables into a String
	string SendToServer (bool NeuroDevice, int channel, float impedance, float voltage, NeuroPolarity polarity, int pulse, int pulseWid, float frequency, double currency, bool bipolar, bool multichannel, uint multichannelnr, uint[] multichannelorder, uint[] multichannelpulse, double[] multichannelcurrent)
	{
		//Current won't go above 15 mA
		if (currency > 15) 
		{
			currency = 15;
		}

		string c = ",";
		string dev;
		string chn 	= channel.ToString ();
		string imp 	= impedance.ToString ();
		string vol 	= voltage.ToString ();
		string pol;
		string pul 	= pulse.ToString ();
		string pulW = pulseWid.ToString ();
		string freq = frequency.ToString ();
		string cur 	= currency.ToString ();
		string bip;
		string multi;
			
		string multiNr = multichannel_number.ToString();

		string[] multiOr = Array.ConvertAll(multichannelorder, x=>x.ToString());
		string multiorder = string.Join("-", multiOr);

		string[] multiPu = Array.ConvertAll(multichannelpulse, x=>x.ToString());
		string multipulse = string.Join("-", multiPu);

		//Current won't go above 15 mA
		for (int i = 0; i < multichannelcurrent.Length; i++) 
		{
			if (multichannelcurrent [i] > 15) 
			{
				multichannelcurrent [i] = 15;
			}
		}

		string[] multiCu = Array.ConvertAll(multichannelcurrent, x=>x.ToString());
		string multicurr = string.Join("-", multiCu);


		//Device
		if (NeuroDevice == false) 
		{
			dev = "1"; //0x01 = DNS
		} else 
		{
			dev = "2"; //0x02 = HCS
		}
		//Polarity
		if (polarity == NeuroPolarity.cathodic) 
		{
			pol = "1";	//0X01 = CATHODIC
		} else 
		{
			pol = "2";	//0x02 = ANODIC
		}
		//Bipolar
		if (bipolar == true) 
		{
			bip = "t";
		} else 
		{
			bip = "f";
		}
		//Multichannel
		if (multichannel == true) 
		{
			multi = "t";
		} else 
		{
			multi = "f";
		}

		//Combine and return string
		string ret = dev + c + chn + c + imp + c + vol + c + pol + c + pul + c + pulW + c + freq + c + cur + c + bip + c + multi + c + multiNr + c + multiorder + c + multipulse + c + multicurr;
		return ret;
	}

	public void ConnectToServer ()
	{
		for (int i = onlyOnce; i < 1; i++) 
		{
			client = new TcpClient(ip, port);
			onlyOnce++;
		}
	}

	public void DisconnectToServer ()
	{
		//client.Close ();
	}
}
