using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Net.Sockets;
using System.Text; 
using UnityEngine.Events;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Linq;
using System.Globalization;


//[Serializable]
//class PlayerData 
//{
//	public bool device;
//	public int channel;
//	public float impedence;
//	public float voltage;
//	public enum Polarity {anodic, cathodic}
//	public Polarity polarity;
//	public string polaritystring;
//	public int pulses;
//	public int pulsewidth;
//	public float frequency;
//	public double current;
//	public bool bipolar;
//	public bool multichannels;
//	public uint multichannel_number;
//	public uint[] multichannel_order;
//	public uint[] multichannel_pulsewidth;
//	public double[] multichannel_current;

//	public int[] savePlacements = new int[12];
//	public String changeImpedence, changeVoltageHCS, changeVoltageDNS, changeFrequency, changeCurrentHCS, changeCurrentDNS, changePulseWidthHCS, changePulseWidthDNS, changePulses;
//	public bool[] ChannelToggle = new bool[12];
//	public String[] currentss = new String[12];
//	public String[] pulsewidthss = new String[12];

//}
//[Serializable]
//class SaveFiles
//{
//    public ArrayList saveFileNames;
//}

public class UiController : MonoBehaviour {


	public static ArrayList channelOrder = new ArrayList ();
	public static ArrayList pulsewidths = new ArrayList ();
	public static ArrayList currents = new ArrayList ();

	public static uint[] pulsewidthArray = {0,0,0,0,0,0,0,0,0,0,0,0};
	public static double[] currentArray = {0,0,0,0,0,0,0,0,0,0,0,0,0};
	public static uint[] channelOrderArray = {0,0,0,0,0,0,0,0,0,0,0,0,0};

	public static uint ID_number;
	public static String saveName;
	public static ArrayList Savefiles = new ArrayList();
	public static uint holdamountofchannels = 0;
    

    
    public Text displayText;
    public InputField changeImpedence, changeVoltageHCS, changeVoltageDNS, changeFrequency, changeCurrentHCS, changeCurrentDNS, changePulseWidthHCS, changePulseWidthDNS, changePulses,
        changemultiCurrent1, changemultiPulseWidth1, changemultiCurrent2, changemultiPulseWidth2, changemultiCurrent3, changemultiPulseWidth3, changemultiCurrent4, changemultiPulseWidth4,
        changemultiCurrent5, changemultiPulseWidth5, changemultiCurrent6, changemultiPulseWidth6, changemultiCurrent7, changemultiPulseWidth7, changemultiCurrent8, changemultiPulseWidth8,
        changemultiCurrent9, changemultiPulseWidth9, changemultiCurrent10, changemultiPulseWidth10, changemultiCurrent11, changemultiPulseWidth11, changemultiPulseWidth12, changemultiCurrent12;

    public Toggle[] ChannelToggle = new Toggle[12];
    public InputField[] currentss = new InputField[12];
    public InputField[] pulsewidthss = new InputField[12];
	public Dropdown[] placements = new Dropdown[12];
    public Toggle bipolar;
    public Button[] deviceButtons = new Button[2];
    public Dropdown polarity;
    public InputField savenamefield;

    public Text[] buttons = new Text[30];

    static int realCurrent = 0;
	public static Dropdown freqChannel;

    private List<string[]> rowData = new List<string[]>();
    

    void Awake()
    {
        if (File.Exists(getPath()))
        {
          LoadNames();
        }
    //    BinaryFormatter bf = new BinaryFormatter();
    //    FileStream file = File.Open(Application.persistentDataPath + "Names_of_saves" + ".dat", FileMode.Open);
    //    SaveFiles data = (SaveFiles)bf.Deserialize(file);
    //    file.Close();

    //    string[] names = (string[])data.saveFileNames.ToArray(typeof(string));

    //    for (int i = 0; i < names.Length; i++)
    //    {
    //        buttons[i].text = names[i];
    //        Debug.Log(names[i]);
    //    }
    }

    void Start ()
	{
		// initialize savefiles to get access to them later.

	}
	
	void Update () {
        //information.GetComponent < Text > = instruction;
	}

	public void SaveName(String name)
	{
		saveName = name;
	}

    //public void save()
    //{
    //	if(File.Exists(Application.persistentDataPath + saveName + ".dat"))
    //	{
    //		//overwrite? some pop-up
    //	}

    //	if(saveName != null)
    //	{
    //		BinaryFormatter bf = new BinaryFormatter();
    //		FileStream file = File.Create(Application.persistentDataPath + saveName + ".dat");
    //		Debug.Log(Application.persistentDataPath);

    //		PlayerData data = new PlayerData();
    //		data.device = Client.NeuroDevice;
    //		data.channel = Client.neuroChannel;
    //		data.impedence = Client.neuroImpedance;
    //		data.voltage = Client.neuroVoltage;
    //		//data.polarity = Client.neuroPolarity; dont know how to save enums..
    //	    //data.polaritystring = Client.NeuroPolarity.toString();
    //		data.pulses = Client.neuroPulses;
    //		data.pulsewidth = Client.neuroPulseWidth;
    //		data.frequency = Client.neuroFrequency;
    //		data.current = Client.neuroCurrent;
    //		data.bipolar = Client.neuroBipolar;
    //		data.multichannels = Client.neuroMultichannels;
    //		//data.multichannel_number = Client.multichannel_number;
    //		data.multichannel_order = Client.multichannel_order;
    //		data.multichannel_pulsewidth = Client.multichannel_pulseWdt;
    //		data.multichannel_current = Client.multichannel_currents;


    //		data.changeImpedence = changeImpedence.text;
    //		data.changeFrequency = changeFrequency.text;
    //		data.changePulses = changePulses.text;
    //		if (Client.NeuroDevice == true)
    //		{
    //			data.changeVoltageHCS = changeVoltageHCS.text;
    //			data.changeCurrentHCS = changeCurrentHCS.text;
    //			data.changePulseWidthHCS = changePulseWidthHCS.text;
    //		}
    //		else
    //		{
    //			data.changeVoltageDNS = changeVoltageDNS.text;
    //			data.changeCurrentDNS = changeCurrentDNS.text;
    //			data.changePulseWidthDNS = changePulseWidthDNS.text;
    //		}

    //		for(int i = 0; i <=11; i++)
    //		{
    //			data.ChannelToggle[i] = ChannelToggle[i].isOn;
    //			data.currentss[i] = currentss[i].text;
    //			data.pulsewidthss[i] = pulsewidthss[i].text;
    //			data.savePlacements[i] = placements[i].value;
    //		}

    //		bf.Serialize(file, data);
    //           Savefiles.Add(saveName);
    //           file.Close();

    //           if (File.Exists(Application.persistentDataPath + "Names_of_saves" + ".dat"))
    //           {
    //               FileStream files = File.OpenWrite(Application.persistentDataPath + "Names_of_saves" + ".dat");
    //               SaveFiles save = new SaveFiles();
    //               save.saveFileNames.Add(saveName);

    //               bf.Serialize(files, save);
    //               file.Close();
    //           }
    //           else
    //           {
    //               FileStream filess = File.Create(Application.persistentDataPath + "Names_of_saves" + ".dat");
    //               SaveFiles save = new SaveFiles();
    //               save.saveFileNames.Add(saveName);
    //               bf.Serialize(filess, save);
    //               filess.Close();
    //           }



    //          // UIsave.GetNames();
    //	}
    //}

    //public void Load()
    //{
    //	if (File.Exists(Application.persistentDataPath + saveName + ".dat"))
    //	{
    //		BinaryFormatter bf = new BinaryFormatter();
    //		FileStream file = File.Open(Application.persistentDataPath + saveName + ".dat", FileMode.Open);
    //		PlayerData data = (PlayerData)bf.Deserialize(file);
    //		file.Close();

    //		Client.NeuroDevice = data.device;
    //		Client.neuroChannel = data.channel;
    //		Client.neuroImpedance = data.impedence;
    //		Client.neuroVoltage = data.voltage;
    //		Client.neuroPulses = data.pulses;
    //		Client.neuroPulseWidth = data.pulsewidth;
    //		Client.neuroFrequency = data.frequency;
    //		Client.neuroCurrent = data.current;
    //		Client.neuroBipolar = data.bipolar;
    //		Client.neuroMultichannels = data.multichannels;
    //		Client.multichannel_number = 0;
    //		Client.multichannel_order = data.multichannel_order;
    //		Client.multichannel_pulseWdt = data.multichannel_pulsewidth;
    //		Client.multichannel_currents = data.multichannel_current;

    //		changeImpedence.text = data.changeImpedence;
    //		changeFrequency.text = data.changeFrequency;
    //		changePulses.text = data.changePulses;
    //		if (data.device == true)
    //		{
    //			changeVoltageHCS.text = data.changeVoltageHCS;
    //			changeCurrentHCS.text = data.changeCurrentHCS;
    //			changePulseWidthHCS.text = data.changePulseWidthHCS;
    //		}
    //		else
    //		{
    //			changeVoltageDNS.text = data.changeVoltageDNS;
    //			changeCurrentDNS.text = data.changeCurrentDNS;
    //			changePulseWidthDNS.text = data.changePulseWidthDNS;
    //		}
    //		for(int i = 0; i <= 11; i++)
    //		{
    //			ChannelToggle[i].isOn = data.ChannelToggle[i];
    //			currentss[i].text = data.currentss[i];
    //			pulsewidthss[i].text = data.pulsewidthss[i];
    //			placements[i].value = data.savePlacements[i];
    //			Debug.Log("current at " + i + "is: " + Client.multichannel_currents[i]);
    //		}
    //		//Debug.Log("impedence " + Client.neuroMultichannels);
    //	}
    //}

    public void Save()
    {
        
        
        string[] multiOr = Array.ConvertAll(Client.multichannel_order, x => x.ToString());
        string multiorder = string.Join("-", multiOr);

        string[] multiPu = Array.ConvertAll(Client.multichannel_pulseWdt, x => x.ToString());
        string multipulse = string.Join("-", multiPu);

        string[] multiCu = Array.ConvertAll(Client.multichannel_currents, x => x.ToString());
        string multicurr = string.Join("-", multiCu);
                  
        string[] channelToggle = new string[12];
        string[] multicutxt = new string[12];        
        string[] multiputxt = new string[12];
        string[] placement = new string[12];
        
        for (int i = 0; i <= 11; i++)
        {
            channelToggle[i] = Convert.ToString(ChannelToggle[i].isOn);
            multicutxt[i] = currentss[i].text;
            multiputxt[i] = pulsewidthss[i].text;
            placement[i] = Convert.ToString(placements[i].value);
        }

        string channelTogglesave = string.Join("-", channelToggle);
        string multicurrtxt = string.Join("-", multicutxt);
        string multipulstxt = string.Join("-", multiputxt);
        string placementsave = string.Join("-", placement);

        

            // Creating First row of titles manually..
            if (File.Exists(Application.dataPath + "Saved_data.csv") == false)
            {
            string[] rowDataTempa = new string[31];
            rowDataTempa[0] = "Name";
                rowDataTempa[1] = "Date";
                rowDataTempa[2] = "Device";
                rowDataTempa[3] = "Channel";
                rowDataTempa[4] = "Impedence";
                rowDataTempa[5] = "Voltage";
                rowDataTempa[6] = "Polarity";
                rowDataTempa[7] = "Pulses";
                rowDataTempa[8] = "Pulse Width";
                rowDataTempa[9] = "Frequency";
                rowDataTempa[10] = "Current";
                rowDataTempa[11] = "Bipolar";
                rowDataTempa[12] = "Multichannel";
                rowDataTempa[13] = "Multichannel number";
                rowDataTempa[14] = "Multichannel order";
                rowDataTempa[15] = "Multichannel pulse widths";
                rowDataTempa[16] = "Multichannel currents";                
                rowDataTempa[17] = "Impedence text";
                rowDataTempa[18] = "Frequency text";
                rowDataTempa[19] = "Pulses text";
                rowDataTempa[20] = "Voltage HCS text";
                rowDataTempa[21] = "Current HCS text";
                rowDataTempa[22] = "Pulse w HCS text";
                rowDataTempa[23] = "Voltage DNS text";
                rowDataTempa[24] = "Current DNS text";
                rowDataTempa[25] = "Pulse w DNS text";
                rowDataTempa[26] = "Channel toggle";
                rowDataTempa[27] = "Multichannel curr text";
                rowDataTempa[28] = "Multichannel pwidth text";
                rowDataTempa[29] = "Placements";
                rowDataTempa[30] = "Bipolar";

                rowData.Add(rowDataTempa);
            }
        else
        {
            rowData.Clear();
        }
                string[] rowDataTemp = new string[31];
        
        // You can add up the values in as many cells as you want.
        // for (int i = 0; i < rowDataTemp.Length; i++)
        // {
        
                rowDataTemp[0] = saveName;
                rowDataTemp[1] = DateTime.Now.ToString();
                rowDataTemp[2] = Client.NeuroDevice.ToString();
                rowDataTemp[3] = Client.neuroChannel.ToString();
                rowDataTemp[4] = Client.neuroImpedance.ToString();
                rowDataTemp[5] = Client.neuroVoltage.ToString();
                if (Client.neuroPolarity == Client.NeuroPolarity.anodic)
                {
                    rowDataTemp[6] = "0";
                }
                else
                {
                    rowDataTemp[6] = "1";
                }

                rowDataTemp[7] = Client.neuroPulses.ToString();
                rowDataTemp[8] = Client.neuroPulseWidth.ToString();
                rowDataTemp[9] = Client.neuroFrequency.ToString();
                rowDataTemp[10] = Client.neuroCurrent.ToString();
                rowDataTemp[11] = Client.neuroBipolar.ToString();
                rowDataTemp[12] = Client.neuroMultichannels.ToString();
                rowDataTemp[13] = Client.multichannel_number.ToString();
                rowDataTemp[14] = multiorder;
                rowDataTemp[15] = multipulse;
                rowDataTemp[16] = multicurr;                             
                rowDataTemp[17] = changeImpedence.text;
                rowDataTemp[18] = changeFrequency.text;
                rowDataTemp[19] = changePulses.text;
                rowDataTemp[20] = changeVoltageHCS.text;
                rowDataTemp[21] = changeCurrentHCS.text;
                rowDataTemp[22] = changePulseWidthHCS.text;
                rowDataTemp[23] = changeVoltageDNS.text;
                rowDataTemp[24] = changeCurrentDNS.text;
                rowDataTemp[25] = changePulseWidthDNS.text;
                if (rowDataTemp[23] == "")
                {
                    rowDataTemp[23] = "0";
                    rowDataTemp[24] = "0";
                    rowDataTemp[25] = "0";
                }
                if (rowDataTemp[20] == "")
                {
                    rowDataTemp[20] = "0";
                    rowDataTemp[21] = "0";
                    rowDataTemp[22] = "0";
                }
                rowDataTemp[26] = channelTogglesave;
                rowDataTemp[27] = multicurrtxt;
                rowDataTemp[28] = multipulstxt;
                rowDataTemp[29] = placementsave;
                rowDataTemp[30] = bipolar.isOn.ToString();
                rowData.Add(rowDataTemp);
           // }

            string[][] output = new string[rowData.Count][];

            for (int i = 0; i < output.Length; i++)
            {
                output[i] = rowData[i];
            }

            int length = output.GetLength(0);
            string delimiter = ",";

            StringBuilder sb = new StringBuilder();

            for (int index = 0; index < length; index++)
                sb.AppendLine(string.Join(delimiter, output[index]));


            string filePath = getPath();

            StreamWriter outStream = System.IO.File.AppendText(filePath);
            outStream.Write(sb);
            outStream.Close();
            rowData.Clear();
        
       // ijk = 1;
    }

    // Following method is used to retrive the relative path as device platform
    private string getPath()
    {
#if UNITY_EDITOR
        return Application.dataPath + "Saved_data.csv";
#elif UNITY_ANDROID
        return Application.persistentDataPath+"Saved_data.csv";
#elif UNITY_IPHONE
        return Application.persistentDataPath+"/"+"Saved_data.csv";
#else
        return Application.dataPath + "Saved_data.csv";
#endif
    }

    public void Load()
    {
        if (File.Exists(getPath()))
        {
            using (StreamReader stream = new StreamReader(getPath(), System.Text.Encoding.UTF8))
            {
                rowData.Clear();
                string line;
                
                while ((line = stream.ReadLine()) != null)
                {
                    if (line != "")
                    {
                        string[] split = line.Split(',');
                        rowData.Add(split);                    
                    }
                }
                for (int i = 0; i <= rowData.Count; i++)
                {
                    if (rowData[i][0] == saveName)
                    {
                        Client.NeuroDevice = Convert.ToBoolean(rowData[i][2]);
                        if(Client.NeuroDevice == true)
                        {
                            deviceButtons[0].onClick.Invoke();
                        }
                        else
                        {
                            deviceButtons[1].onClick.Invoke();
                        }
                        Client.neuroChannel = Int32.Parse(rowData[i][3]);
                        Client.neuroImpedance = float.Parse(rowData[i][4], CultureInfo.InvariantCulture);
                        Client.neuroVoltage = float.Parse(rowData[i][5], CultureInfo.InvariantCulture);
                        if (rowData[i][6] == "0")
                        {
                            Client.neuroPolarity = Client.NeuroPolarity.anodic;
                        }
                        else
                        {
                            Client.neuroPolarity = Client.NeuroPolarity.cathodic;
                        }
                        Client.neuroPulses = Convert.ToInt32(rowData[i][7]);
                        Client.neuroPulseWidth = Convert.ToInt32(rowData[i][8]);
                        Client.neuroFrequency = float.Parse(rowData[i][9], CultureInfo.InvariantCulture);
                        Client.neuroCurrent = double.Parse(rowData[i][10], CultureInfo.InvariantCulture);
                        Client.neuroBipolar = Convert.ToBoolean(rowData[i][11]);
                        Client.neuroMultichannels = Convert.ToBoolean(rowData[i][12]);
                        Client.multichannel_number = Convert.ToUInt32(rowData[i][13]);
                        Client.multichannel_order = rowData[i][14].Split('-').Select(n => Convert.ToUInt32(n)).ToArray();
                        Client.multichannel_pulseWdt = rowData[i][15].Split('-').Select(n => Convert.ToUInt32(n)).ToArray();
                        Client.multichannel_currents = rowData[i][16].Split('-').Select(n => double.Parse(n, CultureInfo.InvariantCulture)).ToArray();

                        changeImpedence.text = rowData[i][17];
                        changeFrequency.text = rowData[i][18];
                        changePulses.text = rowData[i][19];
                        if(Client.neuroPolarity == Client.NeuroPolarity.anodic)
                        {
                            polarity.value = 0;
                        }
                        else
                        {
                            polarity.value = 1;
                        }
                        if (Client.NeuroDevice == true)
                        {
                            changeVoltageHCS.text = rowData[i][20];
                            changeCurrentHCS.text = rowData[i][21];
                            changePulseWidthHCS.text = rowData[i][22];
                        }
                        else
                        {
                            changeVoltageDNS.text = rowData[i][23];
                            changeCurrentDNS.text = rowData[i][24];
                            changePulseWidthDNS.text = rowData[i][25];
                        }
                        bool[] channeltogglehold = rowData[i][26].Split('-').Select(n => Convert.ToBoolean(n)).ToArray();
                        string[] multicurrenthold = rowData[i][27].Split('-').Select(n => (n)).ToArray();
                        string[] multipulsewhold = rowData[i][28].Split('-').Select(n => (n)).ToArray();
                        int[] placementshold = rowData[i][29].Split('-').Select(n => Convert.ToInt32(n)).ToArray();
                        bipolar.isOn = Convert.ToBoolean(rowData[i][30]);

                        for (int j = 0; j <= 11; j++)
                        {   
                            if(ChannelToggle[j].isOn != channeltogglehold[j])
                            {
                                ChannelToggle[j].isOn = channeltogglehold[j];
                            }                                                    
                                                        
                            currentss[j].text = multicurrenthold[j];
                            pulsewidthss[j].text = multipulsewhold[j];
                            placements[j].value = placementshold[j];
                        }

                    }
                }
            }
        }
    }

    public void LoadNames()
    {
        string[] hold = new string[31];
        if (File.Exists(getPath()))
        {
            using (StreamReader stream = new StreamReader(getPath(), System.Text.Encoding.UTF8))
            {
                rowData.Clear();
                string line;

                while ((line = stream.ReadLine()) != null)
                {
                    if (line != "")
                    {
                        string[] split = line.Split(',');
                        rowData.Add(split);                        
                    }                    
                }
                for (int i = 0; i <= rowData.Count; i++)
                {
                    buttons[i].text = rowData[i+1][0];
                }
            }
        }
    }

    public void changeInputField(int index)
    {
        
        savenamefield.text = buttons[index].text;
    }

    public void StartStim(bool toggle)
	{
		//int pulseSize = 0;
		//dint currentSize = 0;

		if (toggle == true) 
		{
//				if (Client.neuroMultichannels == true)
//				{
//				
//
//				//			uint pulseArray;
////			pulseArray = new uint[pulseSize];
////			Client.multichannel_pulseWdt = pulseArray;
////
////			uint currentArray;
////			currentArray = new double[currentSize];
////			Client.multichannel_currents = currentArray;
//
//				//uint[] hold = (uint[]) channelOrder.ToArray(typeof(uint));
//			//Client.multichannel_order = hold;
//
////				for (int i = 0; i<holdorder.Length; i++)
////				{
////					Debug.Log("Channel order: "+holdorder[i]);
////					Debug.Log("pulsewidth: "+holdpulse[i]);
////					Debug.Log("Currents: "+holdcurrent[i]);
////				}
//
//				} 

			Client.startstimulation = true;  //TENDRA AFTUR
		}
		else 
		{
			Client.startstimulation = false;
		}
	}

	public void DNS()
	{
		Client.NeuroDevice = false;

	}

	public void HCS()
	{
		Client.NeuroDevice = true;

	}

	public void Channel(int channel)
	{
		//channel = GameObject.Find.Channeldropdown
		//Client.neuroChannel = channel+1;
		Client.neuroChannel = channel;
	}

	public void Impedence(string impedence)
	{
        
		float a = Single.Parse (impedence);
        if (a < 0.1)
        {
            a = 0.1f;
            displayText.text = "Impedence value is set below 0.1. Setting impedence to 0.1.";
            changeImpedence.text = "0.1";
        }

        if (a > 25)
        {
            a = 25;
            displayText.text = "Impedence value is set above 25. Setting impedence to 25.";
            changeImpedence.text = "25";
        }
		Client.neuroImpedance = a;
	}

	public void Voltage(string voltage)
	{
		float b = Single.Parse (voltage);
        if (Client.NeuroDevice == false && b < 2 || Client.NeuroDevice == false && b > 250)
        {
            b = 2;
            displayText.text = "Voltage is set below 2 or above 250. Setting voltage to 2.";
            changeVoltageDNS.text = "2";
        }

        if (Client.NeuroDevice == true && b < 1 || Client.NeuroDevice == true && b > 410)
        {
            b = 1;
            displayText.text = "Voltage is set above 410 or below 1. Setting voltage to 1.";
            changeVoltageHCS.text = "1";
        }
        Client.neuroVoltage = b;
	}


	public void Polarity(int polarity)
	{
		if (polarity == 0) 
		{
			Client.neuroPolarity = Client.NeuroPolarity.anodic;
		}
		else Client.neuroPolarity = Client.NeuroPolarity.cathodic;
	}

	public void Pulses(string pulses)
	{
		int c = int.Parse (pulses);
		Client.neuroPulses = c;
	}

	public void PulseWidth(string pulseWidth)
	{
		int d = int.Parse (pulseWidth);
        if (d < 50 || d > 1000000)
        {
            d = 200;
            displayText.text = "Pulse width is not within range. Setting pulse width to 200.";
            changePulseWidthHCS.text = "200";
            changePulseWidthDNS.text = "200";
        }
		Client.neuroPulseWidth = d; 
	}

	public void Frequency(string frequency)
	{
		float e = Single.Parse (frequency);
        if (e < 0.1 || e > 1000)
        {
            e = 0.1f;
            displayText.text = "Frequency not within range. Setting frequency to 0.1";
            changeFrequency.text = "0.1";
        }
		Client.neuroFrequency = e;
	}
	public void Current(string current)
	{
		double f = double.Parse (current);
        if (Client.NeuroDevice == false && f < 0.01 || Client.NeuroDevice == false && f > 25)
        {
            f = 0.01f;
            displayText.text = "Current not set within range. Setting current to 0.01.";
            changeCurrentDNS.text = "0.01";
        }
        if (Client.NeuroDevice == true && f < 0.2 || Client.NeuroDevice == true && f > 250)
        {
            f = 0.2f;
            displayText.text = "Current not set within range. Setting current to 0.2.";
            changeCurrentHCS.text = "0.2";
        }
        else
        {
            changeCurrentHCS.text = current;
        }
        
		Client.neuroCurrent = f;
	}

	public void Bipolar(bool bipolar)
	{
		if (bipolar == true) {
			Client.neuroBipolar = true;
		} else
			Client.neuroBipolar = false;
	}

	public void Multichannels(bool multichannels)
	{
		if (multichannels == true) {
			Client.neuroMultichannels = true;
		} else
			Client.neuroMultichannels = false;
	}

	public void Number_of_Channels(bool toggle)
	{
		if (toggle == true)
		{
			Client.multichannel_number++;
		}
		else
		{
			Client.multichannel_number--;
		}
		if (Client.multichannel_number > 1 && Client.NeuroDevice == true) //if there are more than one active channels & if device is HCS
		{
			Client.neuroMultichannels = true;
		}
		else
		{
			Client.neuroMultichannels = false;
		}
		holdamountofchannels = Client.multichannel_number;
		Debug.Log(Client.multichannel_number);
	}

	public void HowManyChannels()
	{
		if (Client.multichannel_number > 1 && Client.NeuroDevice == true) //if there are more than one active channels & if device is HCS
		{
			Client.neuroMultichannels = true;
		}
		else
		{
			Client.neuroMultichannels = false;
		}
	}

	public void Multichannel_Order(bool toggle)
	{
		if (toggle == true)
		{
			//channelOrder.Add (ID_number);
			//channelorder[ID_number] = 1;
			channelOrderArray[ID_number] = ID_number+1;
		}
		else
		{
			//channelOrder.Remove(ID_number);
			channelOrderArray[ID_number] = 0;
			pulsewidthArray[ID_number] = 0;
			currentArray[ID_number] = 0;
		}

		//Debug.Log(Client.multichannel_order);

	}

	public void Multichannel_ID(int ID)
	{
		uint u = Convert.ToUInt32(ID);
		ID_number = u;
	}

	public void Multichannel_Pulse_Width(string pulse)
	{
		//pulsewidths.Add (pulse);
		uint d = uint.Parse (pulse); 
        if (d < 50 || d > 1000000)
        {
            d = 200;
            displayText.text = "Pulse width on channel " + ID_number + " is not set within range. Changing pulse width to 200.";
            switch (ID_number)
            {
                case 0:
                    changemultiPulseWidth1.text = "200";
                    break;
                case 1:
                    changemultiPulseWidth2.text = "200";
                    break;
                case 2:
                    changemultiPulseWidth3.text = "200";
                    break;
                case 3:
                    changemultiPulseWidth4.text = "200";
                    break;
                case 4:
                    changemultiPulseWidth5.text = "200";
                    break;
                case 5:
                    changemultiPulseWidth6.text = "200";
                    break;
                case 6:
                    changemultiPulseWidth7.text = "200";
                    break;
                case 7:
                    changemultiPulseWidth8.text = "200";
                    break;
                case 8:
                    changemultiPulseWidth9.text = "200";
                    break;
                case 9:
                    changemultiPulseWidth10.text = "200";
                    break;
                case 10:
                    changemultiPulseWidth11.text = "200";
                    break;
                case 11:
                    changemultiPulseWidth12.text = "200";
                    break;
            }
            //info
        }
		//uint u = Convert.ToUInt32(pulse);
		pulsewidthArray[ID_number] = d;
		//Client.multichannel_pulseWdt = pulsewidthArray;
	}

	public void Multichannel_Current(string current)
	{
		double d = double.Parse(current);
        if (d < 0.2 || d > 250)
        {
            d = 0.2;
            displayText.text = "Current on channel " + ID_number + " is not set within range. Changing current to 0.2.";
            switch (ID_number)
            {
                case 0:
                    changemultiCurrent1.text = "0.2";
                    break;
                case 1:
                    changemultiCurrent2.text = "0.2";
                    break;
                case 2:
                    changemultiCurrent3.text = "0.2";
                    break;
                case 3:
                    changemultiCurrent4.text = "0.2";
                    break;
                case 4:
                    changemultiCurrent5.text = "0.2";
                    break;
                case 5:
                    changemultiCurrent6.text = "0.2";
                    break;
                case 6:
                    changemultiCurrent7.text = "0.2";
                    break;
                case 7:
                    changemultiCurrent8.text = "0.2";
                    break;
                case 8:
                    changemultiCurrent9.text = "0.2";
                    break;
                case 9:
                    changemultiCurrent10.text = "0.2";
                    break;
                case 10:
                    changemultiCurrent11.text = "0.2";
                    break;
                case 11:
                    changemultiCurrent12.text = "0.2";
                    break;
            }
                
            //info til information box og UI
        }

		currentArray[ID_number] = d;
	}

	public void Send_Stimulation()
	{
        int x = 0;
		int count = 0;
        if (Client.NeuroDevice == true)
        {
            if (changeImpedence.text == "")
            {
                Client.neuroImpedance = 0.1f;
                changeImpedence.text = "0.1";
			
            }
            if (changeVoltageHCS.text == "")
            {
                Client.neuroVoltage = 1;
                changeVoltageHCS.text = "1";
            }
            if (changeFrequency.text == "")
            {
                Client.neuroFrequency = 0.1f;
                changeFrequency.text = "0.1";
            }
            if(changePulses.text == "")
            {
                Client.neuroPulses = 0;
                changePulses.text = "0";
            }
            //if()

            //if (changeCurrentHCS.text == "")
            //{
            //    Client.neuroCurrent = 0.02;
            //}
            Client.multichannel_number = 0;
            for (int i = 0; i <= 11; i++)
            {
                if (ChannelToggle[i].isOn == true && currentss[i].text == "")
                {
                    currentArray[i] = 0.2;
                    currentss[i].text = "0.2";
                }
				if(ChannelToggle[i].isOn == true && currentss[i].text != "")
				{
					double hold = Convert.ToDouble(currentss[i].text);
					currentArray[i] = hold;	
				}

                if (ChannelToggle[i].isOn == false)
                {
                    currentArray[i] = 0;
                    //currentss[i].text = "";
                }
                if (ChannelToggle[i].isOn == true && pulsewidthss[i].text == "")
                {
                    pulsewidthArray[i] = 200;
                    pulsewidthss[i].text = "200";
                }
				if (ChannelToggle[i].isOn == true && pulsewidthss[i].text != "")
				{
					uint hold = Convert.ToUInt32(pulsewidthss[i].text);
					pulsewidthArray[i] = hold;
				}

                if (ChannelToggle[i].isOn == false)
                {
                    pulsewidthArray[i] = 0;
                    //pulsewidthss[i].text = "";
                } 
                if(ChannelToggle[i].isOn == true)
                {
                    Client.multichannel_number++;
                }
            }

            for (x = 0; x <= 11; x++)
            {
                if (ChannelToggle[x].isOn == true)
                {
                    Client.neuroChannel = x + 1;
                    Debug.Log(x);
                    break;
                }
            }

            for (x = 0; x <= 11; x++)
            {
                if (currentArray[x] == 0)
                {
					Client.neuroCurrent = 0.2;
					Client.neuroPulseWidth = 50;
                }
                else
                {
                    Client.neuroCurrent = currentArray[x];

                    uint y = pulsewidthArray[x];
                    int z = Convert.ToInt32(y);
                    Client.neuroPulseWidth = z;

                    Debug.Log("current " + Client.neuroCurrent);
                    Debug.Log("pulsewidth " + Client.neuroPulseWidth);

                    break;
                }
            }

            for (int i = 0; i <= 11; i++)
            {
                if (pulsewidthArray[i] != 0)
                {
                    //pulseSize +=1;
                    pulsewidths.Add(pulsewidthArray[i]);
                }
				else
				{
					count++;
				}
            }
			if (count == 12)
			{
				pulsewidthArray[0] = 0;
				pulsewidths.Add(pulsewidthArray[0]);

			}
			count = 0;

            uint[] holdpulse = (uint[])pulsewidths.ToArray(typeof(uint));
            Client.multichannel_pulseWdt = holdpulse;

            for (int i = 0; i <= 11; i++)
            {
                if (currentArray[i] != 0)
                {
                    //currentSize +=1;
                    currents.Add(currentArray[i]);
                }
				else
				{
					count++;
				}
            }
			if (count == 12)
			{
				currentArray[0] = 0;
				currents.Add(currentArray[0]);
			}
			count = 0;

            double[] holdcurrent = (double[])currents.ToArray(typeof(double));
            Client.multichannel_currents = holdcurrent;

            for (int i = 0; i <= 11; i++)
            {
                if (channelOrderArray[i] != 0)
                {
                    //currentSize +=1;
                    channelOrder.Add(channelOrderArray[i]);
                }
				else
				{
					count++;
				}
            }
			if(count == 12)
			{
				channelOrderArray[0] = 0;
				channelOrder.Add(channelOrderArray[0]); 
			}
			count = 0; 

            uint[] holdorder = (uint[])channelOrder.ToArray(typeof(uint));
            Client.multichannel_order = holdorder;

            

            Client.sendStimulatorVariables = true;
        }
        //if (Client.neuroVoltage <= 410 && Client.neuroVoltage >=1 && Client.neuroCurrent <= 15 && Client.neuroCurrent >= 1)
        if (Client.NeuroDevice == false)
        {
            if (changeVoltageDNS.text == "")
            {
                Client.neuroVoltage = 1;
                changeVoltageDNS.text = "1";
            }
            Client.sendStimulatorVariables = true;
        }
		

		channelOrder.Clear();
		pulsewidths.Clear();
		currents.Clear();	
	}

	public void Stop_stimulation()
	{
		Client.sendStimulatorVariables = false;
	}

	public static void freqSendVariables()
	{
		
		//realCurrent = Convert.ToInt32(FreqUI.channelUse);
		//realCurrent = (uint) (int) FreqUI.channelUse;
		Client.multichannel_number = 0;
		Client.neuroChannel = FreqUI.channelUse;
		Client.neuroMultichannels = false;
		Client.neuroCurrent = currentArray[FreqUI.channelUse-1];
		Client.neuroPulseWidth = (int) (uint) pulsewidthArray[FreqUI.channelUse-1];

//		channelOrder.Clear();
//		pulsewidths.Clear();
//		currents.Clear();

//		for (int i = 0; i <=11; i++)
//		{
//			channelOrderArray[i] = 0;
//			currentArray[i] = 0;
//			pulsewidthArray[i] = 0;
//		}
//
//		for (int i = 0; i <= 11; i++)
//		{
//			if (pulsewidthArray[i] != 0)
//			{
//				//pulseSize +=1;
//				pulsewidths.Add(pulsewidthArray[i]);
//			}
//		}
//
//		uint[] holdpulse = (uint[])pulsewidths.ToArray(typeof(uint));
//		Client.multichannel_pulseWdt = holdpulse;
//
//		for (int i = 0; i <= 11; i++)
//		{
//			if (currentArray[i] != 0)
//			{
//				//currentSize +=1;
//				currents.Add(currentArray[i]);
//			}
//		}
//
//		double[] holdcurrent = (double[])currents.ToArray(typeof(double));
//		Client.multichannel_currents = holdcurrent;
//
//		for (int i = 0; i <= 11; i++)
//		{
//			if (channelOrderArray[i] != 0)
//			{
//				//currentSize +=1;
//				channelOrder.Add(channelOrderArray[i]);
//			}
//		}
//
//		uint[] holdorder = (uint[])channelOrder.ToArray(typeof(uint));
//		Client.multichannel_order = holdorder;

		 

		Client.sendStimulatorVariables = true;
			
	}
}
