//The purpose of this script is to send the collision info to the button (deeper layers)
using UnityEngine;
using System.Collections;

public class SubmitButton : MonoBehaviour 
{
	public GameObject Button, Animator;
	public static int DoStimulate, TextureId, IsSubmitted, IsSuccess;
	public Color ButtonColor, ButtonPressColor;
	//bool TimerStarted=false;
	public static int WinOrLoseTimer=0;
	public static int FrequencyRealScore = 0;
	//ButtonColor is the standard color, ButtonPressColor is the activated one

	public AudioSource success;
	public AudioSource failure;

	//Managing Level Variables------------------------------------------------------------------------------------------//
	//Levels to activate/deactivate
	public GameObject level01;
	public GameObject level02;
	public GameObject level03;

	public static int 	amountOfButtons 	= 4;		//Sets amounts of buttons available. Used in SubmitButton script
	public static bool	trainingMode		= false;	//true = training session, false = game session
	public static int	amountofRounds		= 0;		//Amount of rounds for each level. 0 = unlimited
	public static int 	startingLevel		= 0;
	public static int	round				= 1;
	public static int 	level 				= 0;
	public static bool	gameOver			= false;
	public static bool 	gameNotStarted		= true;
	public static int 	iterations = 0;
		
	void Start () 
	{
		Button.GetComponent<Renderer> ().material.color = ButtonColor;
		level = startingLevel;

		//Initiate ScoreBoard
		if (amountofRounds == 0) 
		{ 	
			FrequencyGameScore.infiniteLevels = true;
			Debug.Log ("Infinite Rounds mode enabled");
		} 
	}

	void Update () 
	{
		if (Manager.TimeToSense == true) 
		{
			TextureId = 3;
		}

		Debug.Log ( "Starting level " + startingLevel + "; current level " + level + "; round " + round + "; Max. Rounds " + amountofRounds + "; Game over?: " + gameOver);
		Debug.Log ("Game note started? " + gameNotStarted);
	}

	void OnTriggerEnter () 
	{
		Animator.GetComponent<Animation> ().Play ("PressDown");

		if ( SubmitButton.gameNotStarted == false && SubmitButton.gameOver == false ) 
		{
			if (Manager.TimeToSense == false && Manager.TimeToSubmit ==true && WinOrLoseTimer==0) 
			{
				IsSubmitted 		= 1;
				WinOrLoseTimer 		= 1;
				StartCoroutine (Wait ());

				if (Manager.LastButtonPressed == Manager.ButtonPressId) 
				{
					Animator.GetComponent<AudioSource> ().Play ();

					success.Play (); 
					TextureId = 1;
					IsSuccess = 1;
				} 
				else 
				{
					//GetComponent<AudioSource> ().Play ();

					failure.Play ();
					TextureId = 2;
					IsSuccess = 0;
				}
			}
		}
	}
		
	void OnTriggerExit () 
	{
		Animator.GetComponent<Animation> ().Play ("PressUp");
	}

	//Starts Coroutine which manages the new level sequence
	IEnumerator Wait ()
	{
		yield return new WaitForSeconds (2);

		if (Manager.TimeToSense == false & Manager.TimeToSubmit ==true) 
		{
			if (Manager.LastButtonPressed == Manager.ButtonPressId) 
			{
				//Button.GetComponent<Renderer> ().material.color = new Color (1, 0, 0, 1);
				Manager.Score = Manager.Score + 1;
				//FrequencyRealScore = FrequencyRealScore + 1;

			} 
			else 
			{
				if (Manager.Score >= 1) 
				{
					//Manager.Score = Manager.Score - 1;
				}
			}

			//Level and Round Manager---------------------------------//
			//Game Session - Rounds
			if (amountofRounds == 0) 
			{ 	
				FrequencyGameScore.infiniteLevels = true;
			} 
			else if (round == amountofRounds ) 
			{
				Debug.Log ( "The Game is finished" );
				FrequencyGameScore.infiniteLevels = false;
				gameOver = true;
				gameNotStarted = true;
			}
			else 
			{
				round = round + 1;	//Used to display rounds
				Debug.Log ("Round is: " + round);
			}
			//else if (round > amountofRounds && level < 2)
			//{ 
			//	FrequencyGameScore.infiniteLevels = false;
			//	round = 1;
			//	level++;
			//	ChooseLevel (level);
			//}
			//Manager.Round = Manager.Round + 1;									

			int randomRange = amountOfButtons + 1;		//Checks level / amount of button
			Manager.ButtonPressId = Random.Range (1, randomRange);
			Manager.LastButtonPressed = 0;
			TextureId = 0;
			IsSubmitted = 0;
			Manager.TimeToSense = true;
			Manager.TimeToSubmit = false;
		}

		WinOrLoseTimer = 0;
	}

	//Manage Levels
	public void ChooseLevel (int levelChoice)
	{
		switch (levelChoice) 
		{
		case 0:		//Level 1: 4 Buttons
			amountOfButtons = 4;
			startingLevel	= 0;

			level01.SetActive (true);
			level02.SetActive (false);
			level03.SetActive (false);
			break;
		case 1:		//Level 2: 5 Buttons
			amountOfButtons = 5;
			startingLevel	= 1;

			level01.SetActive (false);
			level02.SetActive (true);
			level03.SetActive (false);
			break;
		case 2:		//Level 3: 6 Buttons
			amountOfButtons = 6;
			startingLevel	= 2;

			level01.SetActive (false);
			level02.SetActive (false);
			level03.SetActive (true);
			break;
		default:
			//Nothing
			break;
		}
	}

	//Used to change amount of rounds within UI
	public void SetRounds (string rnd)
	{
		iterations = System.Int32.Parse (rnd);

		if (amountofRounds == 0)
		{
			FrequencyGameScore.infiniteLevels = true;
		} 
		else 
		{
			FrequencyGameScore.infiniteLevels = false;
		}

		//Setting up maximum amount of rounds based on level
		if ( startingLevel == 0 ) 
		{
			amountofRounds = iterations * 4;
		}
		else if ( startingLevel == 1 ) 
		{
			amountofRounds = iterations * 5;
		} 
		else if ( startingLevel == 2 ) 
		{
			amountofRounds = iterations * 6;
		}
	}

	//Activate or Deactivate Training Mode in UI
	public void TrainingVersionActive (bool yesNo)
	{
		trainingMode = yesNo;
	}

	//Reset Game
	public void ResetEverything ()
	{
		gameOver = false;
		level = startingLevel;
		round = 1;
		Manager.Score = 0;

		FrequencyGameScore.currentTime = 0;

		if (amountofRounds == 0)
		{
			FrequencyGameScore.infiniteLevels = true;
		} 
		else 
		{
			FrequencyGameScore.infiniteLevels = false;
		}


		int maxRange = amountOfButtons + 1;
		Manager.ButtonPressId 	= Random.Range (1, maxRange);
		Manager.TimeToSense 	= true;		//Set game state to stimulation state
	}

	//Stop The Game
	public void StopGame ()
	{
		gameNotStarted = true;
	}

	//Start the Game
	public void StartGame ()
	{
		gameNotStarted = false;
		gameOver = false;
		level = startingLevel;
		round = 1;
		Manager.Score = 0;

		FrequencyGameScore.currentTime = 0;

		if (amountofRounds == 0)
		{
			FrequencyGameScore.infiniteLevels = true;
		} 
		else 
		{
			FrequencyGameScore.infiniteLevels = false;
		}
			
		//Setting up maximum amount of rounds based on level
		if ( startingLevel == 0 ) 
		{
			amountofRounds = iterations * 4;
		}
		else if ( startingLevel == 1 ) 
		{
			amountofRounds = iterations * 5;
		} 
		else if ( startingLevel == 2 ) 
		{
			amountofRounds = iterations * 6;
		}

		int maxRange = amountOfButtons + 1;
		Manager.ButtonPressId 	= Random.Range (1, maxRange);
		Manager.TimeToSense 	= true;		//Set game state to stimulation state
	}
}