using UnityEngine;
using System.Collections;

public class LocationManager : MonoBehaviour 
{
	public static int stage 				= 1; 	//Maybe not in use?
	public static int ButtonPressId 		= 0;		//Gives an ID for user to find: Used in LocationStimulationHands
	public static int LastButtonPressed 	= 0;		//What user last pressed
	public static int Score 				= 0;		//Game Score
	public static int Round 				= 1; 		//Round checker
	public static int AmountofRounds		= 0;
	public static int SelectedId			= 0; 		//Maybe not in use?
	public static int SelectedColorId		= 0;		//Color indicators for LocationTextureCheckerHands

	public static int iterations = 0;

	public static int levelManager			= 1;			//Level Indicator
	public static int currentLevel			= 1;
	public static int[] chosenValues		= new int[3];	//Valuse to find
	public static int[] chosenValues2		= new int[2];	

	public static bool Success 			= false;		//Complete game
	public static bool SendStimulation 	= false;		//When to send to Neuro
	public static bool TimeToSense 		= true;			//When user should get stim input. False when Stim. Hands onTriggerExit.	
	public static bool TimeToSubmit		= false;		//When user can press submit button
	public static bool gameOver			= false;
	public static bool gameNotStarted 	= true;	

	void Awake()
	{
		ResetEverything ();
	}

	void Start () 
	{
		SendStimulation = false;

		//Find and Assign numbers for all levels
		//ChooseLevel (levelManager);
		chosenValues = FindThreeRandoms (chosenValues[0], chosenValues[1], chosenValues[2]);	//Lvl3

		chosenValues2 [0] = chosenValues [0];													//Lvl2
		chosenValues2 [1] = chosenValues [1];
		ButtonPressId = chosenValues [0];														//lvl1

		currentLevel = levelManager;

		LastButtonPressed 	= 0;

		//Recheck if game is in infinite rounds mode
		if (AmountofRounds == 0) 
		{
			LocationScore.LocInfiniteLevels = true;
		} 
		else 
		{
			LocationScore.LocInfiniteLevels = false;
		}
	}

	void Update ()
	{
		//Debug.Log ( "Level Manager " + levelManager + "; Current Level " + currentLevel + "; Round " + Round + "; Amount of Rounds " + AmountofRounds + " Game Over?: " + gameOver );
	}
		
	//Choose levels
	public void ChooseLevel (int lvl)
	{
		switch (lvl) 
		{
		case 0:			//lvl 1 - 1 Location
			levelManager = 1;
			currentLevel = 1;
			break;
		case 1:			//lvl 2 - 2 Locations
			levelManager = 2;
			currentLevel = 2;
			break;
//		case 2:			//lvl 3 - 3 Locations
//			levelManager = 3;
//			currentLevel = 3;
//			break;
		}
	}

	//Randomize 3 different values/Locations 
	public static int[] FindThreeRandoms (int one, int two, int three)
	{
		one = Random.Range (1, 7);

		do {
			two = Random.Range (1, 7);
		} while (two == one);

		do {
			three = Random.Range (1, 7);
		} while (three == one || three == two);

		Debug.Log ("Numbers are: " + one + " " + two + " " + three);
		int[] rvalues = {one, two, three};

		return rvalues;
	} 

	//Resets the game
	public void ResetEverything ()
	{
		//Find new variables
		chosenValues = LocationManager.FindThreeRandoms (LocationManager.chosenValues[0], LocationManager.chosenValues[1], LocationManager.chosenValues[2]);
		chosenValues2 [0] 	= chosenValues [0];
		chosenValues2 [1] 	= chosenValues [1];
		ButtonPressId 		= LocationManager.chosenValues [0];

		//Reset to Initial Values
		LastButtonPressed 	= 0;
		LocationSubmitButton.TextureId 		= 0;
		SelectedColorId 	= 0;
		SelectedId 			= 0;
		LocationSubmitButton.IsSubmitted 	= 0;
		TimeToSense 		= true;
		TimeToSubmit 		= false;

		//Resetting the previos pressed values
		LocationWhatWasPressed.lastPressed 			= 0;
		LocationWhatWasPressed.secondLastPressed 	= 0;
		LocationWhatWasPressed.thirdLastPressed 	= 0;

		//Button Color Status reset
		LocationButtonAnimator.changeToWhite 		= true;
		LocationButtonAnimator.somebool 			= false;
		LocationWhatWasPressed.maxButtonsPressed 	= false;

		//Level, Round, Score and Time
		Round = 1;
		Score = 0;
		LocationScore.currentTime = 0;
		currentLevel = levelManager;

		if (AmountofRounds == 0) 
		{
			LocationScore.LocInfiniteLevels = true;
		} 
		else 
		{
			LocationScore.LocInfiniteLevels = false;
		}

		gameOver = false;
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

		if (levelManager == 1) 
		{
			AmountofRounds = iterations * 6;
		}
		if (levelManager == 2) 
		{
			AmountofRounds = iterations * 15;
		}

		//Find new variables
		chosenValues = LocationManager.FindThreeRandoms (LocationManager.chosenValues[0], LocationManager.chosenValues[1], LocationManager.chosenValues[2]);
		chosenValues2 [0] 	= chosenValues [0];
		chosenValues2 [1] 	= chosenValues [1];
		ButtonPressId 		= LocationManager.chosenValues [0];

		//Reset to Initial Values
		LastButtonPressed 	= 0;
		LocationSubmitButton.TextureId 		= 0;
		SelectedColorId 	= 0;
		SelectedId 			= 0;
		LocationSubmitButton.IsSubmitted 	= 0;
		TimeToSense 		= true;
		TimeToSubmit 		= false;

		//Resetting the previos pressed values
		LocationWhatWasPressed.lastPressed 			= 0;
		LocationWhatWasPressed.secondLastPressed 	= 0;
		LocationWhatWasPressed.thirdLastPressed 	= 0;

		//Button Color Status reset
		LocationButtonAnimator.changeToWhite 		= true;
		LocationButtonAnimator.somebool 			= false;
		LocationWhatWasPressed.maxButtonsPressed 	= false;

		//Level, Round, Score and Time
		Round = 1;
		Score = 0;
		LocationScore.currentTime = 0;
		currentLevel = levelManager;

		if (AmountofRounds == 0) 
		{
			LocationScore.LocInfiniteLevels = true;
		} 
		else 
		{
			LocationScore.LocInfiniteLevels = false;
		}

		gameOver = false;
	}

	//Used to change amount of rounds within UI
	public void SetRounds (string rnd)
	{
		iterations = System.Int32.Parse (rnd);

		if (AmountofRounds == 0) 
		{
			LocationScore.LocInfiniteLevels = true;
		} 
		else 
		{
			LocationScore.LocInfiniteLevels = false;
		}
			
		if (levelManager == 1) 
		{
			AmountofRounds = iterations * 6;
		}
		if (levelManager == 2) 
		{
			AmountofRounds = iterations * 15;
		}

	}
}