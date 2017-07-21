using UnityEngine;
using System.Collections;
using System;

//Sends the correct variables to the Neurostimulator when the Stim. Hands are pressed and the "Time to send" is true.
public class LocationStimulationHands : MonoBehaviour 
{
	public int DoStimulate;
	public Color ButtonColor, ButtonPressColor;										//ButtonColor is the standard color, ButtonPressColor is the activated one
	public static bool 	currentlyPressing = false;
	private int counter 	= 0;
	private float timer 	= 0.0f;

	public AudioSource stimulationSound;


	void Start () 
	{
		//GetComponent<Renderer> ().material.color = ButtonColor;
	}

	void Update () 
	{	
		if (counter != 0) 
		{
			Debug.Log ("counter " + counter + " and timer " + timer);
			timer += Time.deltaTime;
		}
	}

	//Triggers--------------------------------------------------------------------------------//
	void OnTriggerEnter () 
	{
		counter++;
		
		//If The user needs to get the initial stimulation input
		if (LocationManager.gameNotStarted == true) 
		{
			//Do nothing
		}
		else if (LocationManager.gameOver == true) 
		{
			//Do nothing
		}
		else if (LocationManager.TimeToSense == true) 
		{
			//Hand Trigger visuals handled by LocationCheckerHands Script

			stimulationSound.Play ();
			currentlyPressing = true;

			//If only one channel is used
			if (LocationManager.levelManager == 1) 
			{
				Client.neuroMultichannels = false;

				switch (LocationManager.ButtonPressId) 
				{
				case 1: 
					Client.neuroChannel = 1;
					Client.neuroCurrent = UiController.currentArray [0];
					Client.neuroPulseWidth = (int)(uint) UiController.pulsewidthArray [0];
					Client.sendStimulatorVariables = true;
					break;
				case 2: 
					Client.neuroChannel = 2;
					Client.neuroCurrent = UiController.currentArray [1];
					Client.neuroPulseWidth = (int)(uint) UiController.pulsewidthArray [1];
					Client.sendStimulatorVariables = true;
					break;
				case 3: 
					Client.neuroChannel = 3;
					Client.neuroCurrent = UiController.currentArray [2];
					Client.neuroPulseWidth = (int)(uint) UiController.pulsewidthArray [2];
					Client.sendStimulatorVariables = true;
					break;
				case 4: 
					Client.neuroChannel = 4;
					Client.neuroCurrent = UiController.currentArray [3];
					Client.neuroPulseWidth = (int)(uint) UiController.pulsewidthArray [3];
					Client.sendStimulatorVariables = true;
					break;
				case 5: 
					Client.neuroChannel = 5;
					Client.neuroCurrent = UiController.currentArray [4];
					Client.neuroPulseWidth = (int)(uint) UiController.pulsewidthArray [4];
					Client.sendStimulatorVariables = true;
					break;
				case 6: 
					Client.neuroChannel = 6;
					Client.neuroCurrent = UiController.currentArray [5];
					Client.neuroPulseWidth = (int)(uint) UiController.pulsewidthArray [5];
					Client.sendStimulatorVariables = true;
					break;
				}
			} 

			//If multiple channels are used (level 2 or 3)
			else if (LocationManager.levelManager > 1) 
			{
				Client.neuroMultichannels = true;

				uint[] clearOrder = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
				UiController.channelOrderArray = clearOrder;

				if (LocationManager.levelManager == 3) 
				{
					
					Array.Sort (LocationManager.chosenValues);
					//Debug.Log ("Array order is : " + LocationManager.chosenValues[0] + " " + LocationManager.chosenValues[1] + " " + LocationManager.chosenValues[2] );

					UiController.channelOrderArray [0] = (uint)(int)LocationManager.chosenValues [0];
					UiController.channelOrderArray [1] = (uint)(int)LocationManager.chosenValues [1];
					UiController.channelOrderArray [2] = (uint)(int)LocationManager.chosenValues [2];
					//Debug.Log ("NeuroChannelOrderArray is : " + UiController.channelOrderArray [0] + " " + UiController.channelOrderArray [1] + " " + UiController.channelOrderArray [2] );

					Client.sendStimulatorVariables = true;
				} 
				else 
				{
					int[] firstTwoValues 	= new int[2];
					firstTwoValues [0] 		= LocationManager.chosenValues [0];
					firstTwoValues [1] 		= LocationManager.chosenValues [1];

					Array.Sort (firstTwoValues);

					uint[] order = new uint[2];
					double[] currents = new double[2];
					uint[] pulsewidth = new uint[2];

					order [0] = (uint)(int)firstTwoValues [0];
					order [1] = (uint)(int)firstTwoValues [1];
					Client.multichannel_order = order;

					currents [0] = UiController.currentArray [firstTwoValues[0]-1];
					currents [1] = UiController.currentArray [firstTwoValues[1]-1];
					Client.multichannel_currents = currents;

					pulsewidth [0] = UiController.pulsewidthArray [firstTwoValues [0]-1];
					pulsewidth [1] = UiController.pulsewidthArray [firstTwoValues [1]-1];
					Client.multichannel_pulseWdt = pulsewidth;
					Client.multichannel_number = 2;

					Client.sendStimulatorVariables = true;

				}
			}
			Client.startstimulation = true;
		}
	}

	void OnTriggerStay()
	{
		//Nothing
	}

	void OnTriggerExit () 
	{
		LocationWhatWasPressed.maxButtonsPressed = false;

		if ( LocationManager.gameNotStarted == false && LocationManager.gameOver == false ) 
		{
			//WaitFunction makes sure the user gets more initial stimulation for set amount of seconds before exiting
			StartCoroutine (WaitFunction());
		}
		counter--;
	}

	IEnumerator WaitFunction ()
	{
		if (timer < 1.0f) 
		{
			Debug.Log ("Wait 2 Seconds");
			yield return new WaitForSeconds (2);
		}
		else if (timer > 1.0f && timer < 2.0f) 
		{
			Debug.Log  ("Wait 1 Second");
			yield return new WaitForSeconds (1);
		}
			
		stimulationSound.Stop ();

		currentlyPressing = false;

		if (LocationManager.TimeToSense == true) 
		{
			Client.sendStimulatorVariables 	= false;
			Client.startstimulation 		= false;
			Client.multichannel_number = UiController.holdamountofchannels;
			//Debug.Log (Client.multichannel_number);

			GetComponent<Renderer> ().material.color = ButtonColor;
			LocationManager.TimeToSense	 = false;			//Stop Stimulation part
		}

		timer = 0;
		counter = 0;
	}
}