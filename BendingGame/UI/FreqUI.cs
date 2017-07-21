using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Text; 
using UnityEngine.Events;

public class FreqUI : MonoBehaviour {

	public InputField[] FrequencyInput = new InputField[6];
	public GameObject freq5, freq6;
	public Dropdown channel;


	public static int channelUse;
	int levelID = 1;

	int ID_number = 0;
	public static float[] frequencies = new float[6];


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		channelUse = channel.value+1;
		Debug.Log(channelUse);
	}

	public void ID (int a)
	{
		ID_number = a;
	}

	public void level(int x)
	{
		switch(x)
		{
		case 0:
			//freq5.gameObject.SetActive(false);
			//freq6.gameObject.SetActive(false);
			levelID = 1;


			break;
		case 1:
			//freq5.gameObject.SetActive(true);
			//freq6.gameObject.SetActive(false);
			levelID = 2;

			break;
		case 2:
			//freq5.gameObject.SetActive(true);
			//freq6.gameObject.SetActive(true);
			levelID = 3;

			break;
		}
	}

	public void Frequency(string frequency)
	{
		float e = Single.Parse (frequency);
		if (e < 0.1 || e > 1000)
		{
			e = 0.1f;
			FrequencyInput[ID_number-1].text = "0.1";
			//displayText.text = "Frequency not within range. Setting frequency to 0.1";
			//changeFrequency.text = "0.1";
		}

		frequencies[ID_number] = e;

	}

	public void defaultfreq()
	{
		switch(levelID)
		{
		case 1:
			FrequencyInput[0].text = "10";
			frequencies[0] = 10;
			FrequencyInput[1].text = "17";
			frequencies[1] = 17;
			FrequencyInput[2].text = "28";
			frequencies[2] = 28;
			FrequencyInput[3].text = "48";
			frequencies[3] = 48;
            FrequencyInput[4].text = "80";
			frequencies[4] = 80;
			FrequencyInput[5].text = "100";
			frequencies[5] = 100;
			break;
		case 2:
			FrequencyInput[0].text = "10";
			frequencies[0] = 10;
			FrequencyInput[1].text = "17";
			frequencies[1] = 17;
			FrequencyInput[2].text = "28";
			frequencies[2] = 28;
			FrequencyInput[3].text = "48";
			frequencies[3] = 48;
			FrequencyInput[4].text = "80";
			frequencies[4] = 80;
            FrequencyInput[5].text = "100";
			frequencies[5] = 100;  
			break;
		case 3:
			FrequencyInput[0].text = "10";
			frequencies[0] = 10;
			FrequencyInput[1].text = "17";
			frequencies[1] = 17;
			FrequencyInput[2].text = "28";
			frequencies[2] = 28;
			FrequencyInput[3].text = "48";
			frequencies[3] = 48;
			FrequencyInput[4].text = "80";
			frequencies[4] = 80;
			FrequencyInput[5].text = "100";
			frequencies[5] = 100;
			break;
		}
	}
	public static void frequencySendVariables()
	{
		
		Client.neuroChannel = channelUse;

	}
}

