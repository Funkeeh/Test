//The purpose of this script is to send the collision info to the button (deeper layers)
using UnityEngine;
using System;
using System.Collections;

public class LocationSubmitButton : MonoBehaviour 
{
	public GameObject Button, Animator;
	public static int DoStimulate, TextureId, IsSubmitted, IsSuccess;
	public Color ButtonColor, ButtonPressColor;
	bool TimerStarted=false;
	public static int WinOrLoseTimer=0;
	public static int LocationRealScore = 0;

	//Swtich between modes
	public static bool handMode = true;
	public GameObject hands;
	public GameObject noHands;
	public GameObject tracers;
	public GameObject tracersNoHands;

	//ButtonColor is the standard color, ButtonPressColor is the activated one

	//Checks what buttons are active
	public static bool button01 = false;
	public static bool button02 = false;
	public static bool button03 = false;
	public static bool button04 = false;
	public static bool button05 = false;
	public static bool button06 = false;

	public static int[] receivedValues = { 0, 0, 0 };

	public AudioSource success;
	public AudioSource failure;

	void Start () 
	{
		Button.GetComponent<Renderer> ().material.color = ButtonColor;

		hands.SetActive (true);
		tracers.SetActive (true);
		noHands.SetActive (false);
		tracersNoHands.SetActive (false);
	}

	void Update () 
	{
		if ( LocationManager.gameNotStarted == false && LocationManager.gameOver == false ) 
		{
			//Gray if not to be pressed
			if (LocationManager.TimeToSense == true && handMode == false) 
			{
				TextureId = 3;

				tracersNoHands.SetActive (false);
			} 
			if (LocationManager.TimeToSense == false && handMode == false)
			{
				tracersNoHands.SetActive (true);
			}
		}
	}

	void OnTriggerEnter () 
	{
		Animator.GetComponent<Animation> ().Play ("PressDown");

		if ( LocationManager.gameNotStarted == false && LocationManager.gameOver == false ) 
		{
			if (LocationManager.TimeToSense == false && LocationManager.TimeToSubmit == true && WinOrLoseTimer == 0) 
			{
				IsSubmitted 		= 1;
				WinOrLoseTimer 		= 1;
				StartCoroutine (Wait ());

				//Level 3
				if (LocationManager.levelManager == 3) 
				{
					//Sorts and compares the chosen values with the user pressed values
					int[] stats = new int[3];
					stats = LocationManager.chosenValues;
					Array.Sort (stats);

					int[] userValues = new int[3];
					userValues [0] = LocationWhatWasPressed.lastPressed;
					userValues [1] = LocationWhatWasPressed.secondLastPressed;
					userValues [2] = LocationWhatWasPressed.thirdLastPressed;
					Array.Sort (userValues);

					if (stats [0] == userValues [0] && stats [1] == userValues [1] && stats [1] == userValues [01]) 
					{
						Animator.GetComponent<AudioSource> ().Play ();

						success.Play ();
						TextureId = 1;
						IsSuccess = 1;
						LocationManager.SelectedColorId = 1;

						LocationManager.Score = LocationManager.Score + 1;
					}
					else 
					{
						failure.Play ();
						TextureId = 2;
						IsSuccess = 0;
						LocationManager.SelectedColorId = 2;
					}
				}
				//Level 2
				if (LocationManager.levelManager == 2) 
				{
					//Sorts and compares the chosen values with the user pressed values
					int[] stats = new int[2];
					stats = LocationManager.chosenValues2;
					Array.Sort (stats);

					int[] userValues = new int[2];
					userValues [0] = LocationWhatWasPressed.lastPressed;
					userValues [1] = LocationWhatWasPressed.secondLastPressed;
					Array.Sort (userValues);

					if ( stats [0] == userValues [0] && stats [1] == userValues [1] || stats [0] == userValues [1] && stats [1] == userValues [0] ) 
					{
						Animator.GetComponent<AudioSource> ().Play ();

						success.Play ();
						TextureId = 1;
						IsSuccess = 1;
						LocationManager.SelectedColorId = 1;

						LocationManager.Score = LocationManager.Score + 1;
					}
					else 
					{
						failure.Play ();
						TextureId = 2;
						IsSuccess = 0;
						LocationManager.SelectedColorId = 2;
					}
				}

				//Level 1
				if (LocationManager.levelManager == 1) 
				{
					if (LocationManager.LastButtonPressed == LocationManager.ButtonPressId) 
					{
						Animator.GetComponent<AudioSource> ().Play ();

						success.Play ();
						TextureId = 1;
						IsSuccess = 1;
						LocationManager.SelectedColorId = 1;

						LocationManager.Score = LocationManager.Score + 1;
					}
					else 
					{
						failure.Play ();
						TextureId = 2;
						IsSuccess = 0;
						LocationManager.SelectedColorId = 2;
					}
				}
			}
		}
	}

	void OnTriggerStay()
	{
		//Nothing
	}

	void OnTriggerExit () 
	{
		Animator.GetComponent<Animation> ().Play ("PressUp");
	}

	IEnumerator Wait ()
	{
		yield return new WaitForSeconds (2);

		LocationManager.Round = LocationManager.Round + 1;

		//Find new numbers
		LocationManager.chosenValues = LocationManager.FindThreeRandoms (LocationManager.chosenValues[0], LocationManager.chosenValues[1], LocationManager.chosenValues[2]);
		LocationManager.chosenValues2 [0] = LocationManager.chosenValues [0];
		LocationManager.chosenValues2 [1] = LocationManager.chosenValues [1];
		LocationManager.ButtonPressId = LocationManager.chosenValues [0];

		LocationManager.LastButtonPressed 	= 0;
		TextureId 							= 0;
		LocationManager.SelectedColorId 	= 0;
		LocationManager.SelectedId 			= 0;
		IsSubmitted 						= 0;
		LocationManager.TimeToSense 		= true;
		LocationManager.TimeToSubmit 		= false;

		//Resetting the previos pressed values
		LocationWhatWasPressed.lastPressed 			= 0;
		LocationWhatWasPressed.secondLastPressed 	= 0;
		LocationWhatWasPressed.thirdLastPressed 	= 0;

		//Button Color Status reset
		LocationButtonAnimator.changeToWhite 	= true;
		LocationButtonAnimator.somebool 		= false;

		WinOrLoseTimer = 0;

		//Level and Round Manager---------------------------------//

		//Game Session - Rounds
		if (LocationManager.AmountofRounds == 0) 
		{ 	
			LocationScore.LocInfiniteLevels = true;
		} 
		else if (LocationManager.Round > LocationManager.AmountofRounds ) 
		{
			Debug.Log ("The Game is finished");
			LocationScore.LocInfiniteLevels = false;
			LocationManager.gameOver = true;
			LocationManager.gameNotStarted = true;
		}
		//else if (LocationManager.Round > LocationManager.AmountofRounds && LocationManager.currentLevel == 1)
		//{ 
		//	LocationScore.LocInfiniteLevels = false;
		//	LocationManager.Round = 1;
		//	LocationManager.currentLevel++;
		//}
		//---------------------------------------------------------//
		//Manager.Round = Manager.Round + 1;								
	}

	public void SetHandMode (bool YesNo)
	{
		if (YesNo == true) 
		{
			hands.SetActive 			(true);
			tracers.SetActive 			(true);
			noHands.SetActive 			(false);
			tracersNoHands.SetActive	(false);
			handMode = true;
		}
		if (YesNo == false) 
		{
			hands.SetActive 			(false);
			tracers.SetActive 			(false);
			noHands.SetActive 			(true);
			tracersNoHands.SetActive 	(true);
			handMode = false;
		}
	}
}