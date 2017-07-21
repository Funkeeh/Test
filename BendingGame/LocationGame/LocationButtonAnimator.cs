//The purpose of this script is to send the collision info to the button (deeper layers)
using UnityEngine;
using System.Collections;

public class LocationButtonAnimator : MonoBehaviour 
{
	public GameObject Button, Animator;
	public int id, DoStimulate;
	public Color ButtonGrayColor, ButtonBrightColor, ButtonPressColor, ButtonSuccessColor, ButtonFailureColor;

	//Button status indicators
	public static bool somebool 		= false;
	public static bool changeToWhite 	= true;

	private bool pressing 				= false;
	private bool startStimulation		= false;

	public AudioSource click;

	void Start () 
	{
		ButtonGrayColor		= new Color (0.5f, 0.5f, 0.5f, 1);					//Inactive Color
		ButtonBrightColor 	= new Color (0.9f, 0.9f, 0.9f, 0.9f);				//Active Color
		ButtonSuccessColor	= new Color (0, 1, 0, 1);							//Success
		ButtonFailureColor	= new Color (1, 0, 0, 1);							//Failure

		Button.GetComponent<Renderer> ().material.color = ButtonGrayColor;
	}

	void Update () 
	{
		//FIRST PART: If the User has not submitted his answer (Pressed the submit buttons) (0 = not submitted, 1 = submitting)
		if (LocationManager.gameNotStarted == true)
		{
			Button.GetComponent<Renderer> ().material.color = ButtonGrayColor;
		}
		else if (LocationManager.gameOver == true)
		{
			Button.GetComponent<Renderer> ().material.color = ButtonGrayColor;
		}
		else if (LocationSubmitButton.IsSubmitted == 0) 
		{
			//STIM STAGE: If button should look inactive --------------------------------------------------------------//
			if (LocationManager.TimeToSense == true) 
			{
				Button.GetComponent<Renderer> ().material.color = ButtonGrayColor;		//Gray Clr
			} 
			else if (LocationWhatWasPressed.resetValue == id) //resetValue = Return to 0
			{
				Button.GetComponent<Renderer> ().material.color = ButtonBrightColor;	//Gray/White
				somebool = true;
			}
			else if (changeToWhite == true) 				//Turn white once after timetosense
			{
				Button.GetComponent<Renderer> ().material.color = ButtonBrightColor;	//Gray/White
			} 
			else if (LocationManager.currentLevel == 1 && LocationManager.LastButtonPressed == id) 
			{
				if ( pressing == true )
				{
					Button.GetComponent<Renderer> ().material.color = LocationTextures.ManageTexturePressing (2);

					startStimulation = true;
				}
				else
				{
					Button.GetComponent<Renderer> ().material.color = LocationTextures.ManageTexture (2);
				}
			}
			else if (LocationManager.currentLevel == 1 && LocationManager.LastButtonPressed != id) 
			{
				Button.GetComponent<Renderer> ().material.color = ButtonBrightColor;	//Gray/White
			}
			else if (LocationManager.currentLevel == 2 && id == LocationWhatWasPressed.lastPressed || LocationManager.currentLevel == 2 && id == LocationWhatWasPressed.secondLastPressed) 
			{
				if ( pressing == true )
				{
					Button.GetComponent<Renderer> ().material.color = LocationTextures.ManageTexturePressing (2);

					startStimulation = true;
				}
				else
				{
					Button.GetComponent<Renderer> ().material.color = LocationTextures.ManageTexture (2);
				}
			}
			else if (LocationManager.currentLevel == 2 && id != LocationWhatWasPressed.lastPressed || LocationManager.currentLevel == 2 && id != LocationWhatWasPressed.secondLastPressed)
			{
				Button.GetComponent<Renderer> ().material.color = ButtonBrightColor;	//Gray/White
			}
			else if ( LocationManager.currentLevel == 3 )
			{
				if (id == LocationWhatWasPressed.lastPressed || id == LocationWhatWasPressed.secondLastPressed || id == LocationWhatWasPressed.thirdLastPressed) 
				{
					Button.GetComponent<Renderer> ().material.color = LocationTextures.ManageTexture (2);
				}
				else
				{
					Button.GetComponent<Renderer> ().material.color = ButtonBrightColor;	//Gray/White
				}
			}
		}
		//SECOND PART: If the user Is submitting the answer (by pressing the submit buttons)
		else 
		{
			if (LocationSubmitButton.IsSuccess == 1) 		//Correct answer
			{
				Button.GetComponent<Renderer> ().material.color = ButtonSuccessColor;
			}
			if (LocationSubmitButton.IsSuccess == 0) 		//Wrong answer
			{
				//Give the correct button(s) a yellow color
				if (LocationManager.currentLevel == 2) 
				{
					if ( id == LocationManager.chosenValues2[0] || id == LocationManager.chosenValues2[1] )
					{
						Button.GetComponent<Renderer> ().material.color = ButtonSuccessColor;
					}
					else
					{
						Button.GetComponent<Renderer> ().material.color = ButtonFailureColor;
					}
				}
				else if (LocationManager.currentLevel == 1) 
				{
					if ( id == LocationManager.ButtonPressId )
					{
						Button.GetComponent<Renderer> ().material.color = ButtonSuccessColor;
					}
					else
					{
						Button.GetComponent<Renderer> ().material.color = ButtonFailureColor;
					}
				}
			}
		}
	}

	void OnTriggerEnter () 
	{
		changeToWhite 	= false;
		pressing 		= true;

		if ( LocationBigCollider.thereCanBeOnlyLocation == true && LocationManager.gameNotStarted == false && LocationManager.gameOver == false ) 	//Big Collision Box to remove multiple hits by accident
		{
			if (LocationManager.TimeToSense == false) 
			{
				LocationManager.SelectedId 			= id;
				LocationManager.LastButtonPressed 	= id;
			}
				
			Animator.GetComponent<Animation> ().Play ("PressDown");
			click.Play (); 

			//Level 01  --------------------------------------------------------------------------------------//
			if (LocationManager.currentLevel == 1) 
			{
				if (LocationManager.LastButtonPressed != id) 
				{
					Button.GetComponent<Renderer> ().material.color = LocationTextures.ManageTexture (id);		//ID Color
					somebool = true;
				}
				else if (LocationManager.LastButtonPressed == id && somebool == true) 
				{
					Button.GetComponent<Renderer> ().material.color = ButtonBrightColor;	//Gray/White
					somebool = false;
				}
				if (LocationManager.LastButtonPressed == id && LocationWhatWasPressed.maxButtonsPressed == false) 
				{
					Button.GetComponent<Renderer> ().material.color = LocationTextures.ManageTexture (id);		//ID Color
					somebool = true;
				}
			} 

			//Level 02 and 03  --------------------------------------------------------------------------------//
			else 
			{
				LocationWhatWasPressed.loopOnce = 0;
				if (LocationWhatWasPressed.maxButtonsPressed == false) 
				{
					//Nothing
				}
			}
				
			//What to send to Neurostimulator--------------------------------------------------------------------//
			if ( LocationManager.gameOver == false && LocationManager.TimeToSense == false && LocationSubmitButton.WinOrLoseTimer == 0 && LocationWhatWasPressed.maxButtonsPressed == false ) 
			{
				GetComponent<AudioSource> ().Play ();

				Client.neuroMultichannels = false;

				switch(id)
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
				Client.startstimulation = true;
				Debug.Log ("Starting Stimulation: " + Client.startstimulation );

				if (startStimulation == true) 
				{
					Stimulate ();
				}
			}
			LocationBigCollider.thereCanBeOnlyLocation = false;
		}
	}

	void OnTriggerStay()
	{
		if ( LocationBigCollider.thereCanBeOnlyLocation == true && LocationManager.gameNotStarted == false && LocationManager.gameOver == false ) 
		{
			if (LocationManager.TimeToSense == false & LocationSubmitButton.WinOrLoseTimer == 0)
			{
				if (startStimulation == true) 
				{
					Stimulate ();
				}
			}
		}
	}

	void OnTriggerExit () 
	{
		Animator.GetComponent<Animation> ().Play ("PressUp");

		pressing 			= false;
		startStimulation 	= false;

		if ( LocationManager.TimeToSense == false && LocationSubmitButton.WinOrLoseTimer == 0 && LocationManager.gameNotStarted == false && LocationManager.gameOver == false )
		{
			DoNotStimulate ();

			Client.sendStimulatorVariables 	= false;
			Client.startstimulation 		= false;

			LocationManager.TimeToSubmit 	= true;
			LocationSubmitButton.TextureId 	= 0;
		}
	}

	//Stim Functions-------------------------------------------//
	void Stimulate()
	{
		DoStimulate = 1;
		LocationManager.SendStimulation = true;
	}

	void DoNotStimulate()
	{
		DoStimulate = 0;
		LocationManager.SendStimulation = false;
	}
}