//The purpose of this script is to send the collision info to the button (deeper layers)
using UnityEngine;
using System.Collections;

public class StimulatorButton : MonoBehaviour 
{
	public GameObject Button, Animator;
	public int DoStimulate;
	public Color ButtonColor, ButtonPressColor;
	public Color ButtonInactiveClr =  new Color (0.5f, 0.5f, 0.5f, 1);

	public AudioSource stimSound;
	//ButtonColor is the standard color, ButtonPressColor is the activated one
	void Start () 
	{
		Button.GetComponent<Renderer> ().material.color = ButtonColor;
	}

	void Update () 
	{
		//Nothing
	}

	//Triggers--------------------------------------------------------------------------------//
	void OnTriggerEnter () 
	{
		if (SubmitButton.gameNotStarted == true) 
		{
			Button.GetComponent<Renderer> ().material.color = Color.gray;
		}
		else if (SubmitButton.gameOver == true) 
		{
			Button.GetComponent<Renderer> ().material.color = ButtonInactiveClr;
		}
		else if (Manager.TimeToSense == true) 
		{
			stimSound.Play ();

			Button.GetComponent<Renderer> ().material.color = ButtonPressColor;
			Stimulate ();

			//Client.neuroChannel = 1;

			//Debug.Log("ID: "+id);
			switch(Manager.ButtonPressId)
			{
			case 1: 
				Client.neuroFrequency = FreqUI.frequencies[0];
				//Client.sendStimulatorVariables = true;
				break;
			case 2: 
				Client.neuroFrequency = FreqUI.frequencies[1];
				//Client.sendStimulatorVariables = true;
				break;
			case 3: 
				Client.neuroFrequency = FreqUI.frequencies[2];
				//Client.sendStimulatorVariables = true;
				break;
			case 4: 
				Client.neuroFrequency = FreqUI.frequencies[3];
				//Client.sendStimulatorVariables = true;
				break;
			case 5: 
				Client.neuroFrequency = FreqUI.frequencies[4];
				//Client.sendStimulatorVariables = true;
				break;
			case 6: 
				Client.neuroFrequency = FreqUI.frequencies[5];
				//Client.sendStimulatorVariables = true;
				break;
			}
			UiController.freqSendVariables();
			Client.startstimulation = true;
		}
		Animator.GetComponent<Animation> ().Play ("PressDown");
	}

	void OnTriggerStay()
	{
		if (Manager.TimeToSense == true) 
		{
			Stimulate ();
		}
	}

	void OnTriggerExit () 
	{
		if ( SubmitButton.gameNotStarted == false  && SubmitButton.gameOver == false ) 
		{ 
			Animator.GetComponent<Animation> ().Play ("PressUp");

			//WaitFunction makes sure the user gets more initial stimulation for set amount of seconds before exiting
			StartCoroutine (WaitFunction());	
		}
	}
		
	//Functions------------------------------------------------------------------------------//
	void Stimulate()
	{
		DoStimulate = 1;
		Manager.SendStimulation = true;
	}

	void DoNotStimulate()
	{
		{
			DoStimulate = 0;
			Manager.SendStimulation = false;
		}
	}

	IEnumerator WaitFunction ()
	{
		yield return new WaitForSeconds (1);

		stimSound.Stop ();

		if (Manager.TimeToSense == true) 
		{
			DoNotStimulate ();

			Client.sendStimulatorVariables = false;
			Client.startstimulation = false;

			Button.GetComponent<Renderer> ().material.color = ButtonColor;
			Manager.TimeToSense = false;
		}
	}
}