//The purpose of this script is to send the collision info to the button (deeper layers)
using UnityEngine;
using System.Collections;

public class ButtonAnimator : MonoBehaviour 
{
	
	public GameObject Button, Animator;
	public int id, DoStimulate;
	public Color ButtonGrayColor, ButtonBrightColor, ButtonPressColor, ButtonSuccessColor, ButtonFailureColor, ButtonBrightPressing;
	//ButtonColor is the standard color, ButtonPressColor is the activated one

	private bool currentlyPressingButton = false;

	void Start () 
	{
		ButtonGrayColor		= new Color (0.5f, 0.5f, 0.5f, 1);
		ButtonBrightColor 	= new Color (0.9f, 0.9f, 0.9f, 0.9f);
		ButtonPressColor	= new Color (1, 1, 0.15f, 1);
		ButtonSuccessColor	= new Color (0, 1, 0, 1);
		ButtonFailureColor	= new Color (1, 0, 0, 1);
		ButtonBrightPressing = new Color (1.0f, 1.0f, 0.60f, 1);

		Button.GetComponent<Renderer> ().material.color = ButtonGrayColor;
	}

	void Update () 
	{
		if (SubmitButton.gameNotStarted == true) 
		{
			Button.GetComponent<Renderer> ().material.color = ButtonGrayColor;
		}
		else if (SubmitButton.gameOver == true) 
		{
			Button.GetComponent<Renderer> ().material.color = ButtonGrayColor;
		}
		//If Submit button is not being pressed
		else if (SubmitButton.IsSubmitted == 0) 														
		{
			if (Manager.LastButtonPressed == id) 												//If The pressed button is = the right choice
			{
				Button.GetComponent<Renderer> ().material.color = ButtonPressColor;				//Change the color of button to the Yellow "Active" Color

				if (currentlyPressingButton == true) 											//If active button is being pressed
				{
					Button.GetComponent<Renderer> ().material.color = ButtonBrightPressing;
				}
			} 
			else 																			
			{
				if (Manager.TimeToSense == true) 												//If The User presses another button								
				{
					Button.GetComponent<Renderer> ().material.color = ButtonGrayColor;			//Grey not being pressed/active Color
					//Button Inactive
				} 
				else 																			//If The User should press the stimulation button
				{
					Button.GetComponent<Renderer> ().material.color = ButtonBrightColor;		//Inactive Brigth Grey Color
					//Button Active
				}
			}
		} 
		else 
		{
			if (SubmitButton.IsSuccess == 1) 													//If chosen button is correct
			{
				Button.GetComponent<Renderer> ().material.color = ButtonSuccessColor;
				//Correct
			}
			if (SubmitButton.IsSuccess == 0) 													//If chosen button is wrong										
			{
				if (Manager.ButtonPressId == id) 
				{
					Button.GetComponent<Renderer> ().material.color = ButtonSuccessColor;
				} 
				else 
				{
					Button.GetComponent<Renderer> ().material.color = ButtonFailureColor;
				}

				//Wrong
			}
		}
	}

	void OnTriggerEnter () 
	{
		if (FrequencyGameBigCollider.thereCanBeOnlyOneAlsoHere == true) 
		{
			Animator.GetComponent<Animation> ().Play ("PressDown");

			if ( Manager.TimeToSense == false && SubmitButton.WinOrLoseTimer == 0 && SubmitButton.gameNotStarted == false  && SubmitButton.gameOver == false ) 
			{
				Manager.LastButtonPressed = id;
				GetComponent<AudioSource> ().Play ();

				//Client.neuroChannel = 1;
				Client.neuroMultichannels = false;
				UiController.freqSendVariables();
				//Debug.Log("ID: "+id);
				switch(id)
				{
				case 1: 
					Client.neuroFrequency = FreqUI.frequencies[0];
					//Client.sendStimulatorVariables = true; // change to frequency send stimulator function
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
				//Client.sendStimulatorVariables = false;
				Client.startstimulation = true;

				Stimulate ();

				FrequencyGameBigCollider.thereCanBeOnlyOneAlsoHere = false;
			}
		}
	}

	void OnTriggerStay()
	{
		if (FrequencyGameBigCollider.thereCanBeOnlyOneAlsoHere == true) 
		{
			if (Manager.TimeToSense == false) 
			{
				Stimulate ();
			}
		}

		currentlyPressingButton = true;
	}
		
	void OnTriggerExit () 
	{
		Animator.GetComponent<Animation> ().Play ("PressUp");

		if (SubmitButton.gameNotStarted == false  && SubmitButton.gameOver == false ) 
		{
			if (Manager.TimeToSense == false & SubmitButton.WinOrLoseTimer==0) 
			{
				DoNotStimulate ();

				Client.sendStimulatorVariables = false;
				Client.startstimulation = false;

				Manager.TimeToSubmit = true;
				SubmitButton.TextureId = 0;
			}
		}

		currentlyPressingButton = false;
	}

	//When to send the given stimulation
	void Stimulate()
	{
		DoStimulate = 1;
		Manager.SendStimulation = true;
	}

	void DoNotStimulate()
	{
		DoStimulate = 0;
		Manager.SendStimulation = false;
	}
}