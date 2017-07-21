using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameStatus : MonoBehaviour 
{
	public Text	gameStatus;
	public Text gameLevel;
	public Text gameRound;
	public Text gameTime;
	public Text gameScore;

	public GameObject bendingGame;
	public GameObject locationGame;
	public GameObject frequencyGame;
	public GameObject freqTraining;
	public GameObject locTraining;

	private static int mode = 0;

	void Update () 
	{
		//FREQUENCY GAME --------------------------------------------------------------------//
		if (frequencyGame.activeSelf == true) 
		{
			if (SubmitButton.gameOver == true) 
			{
				gameStatus.text = "Finished";
				gameStatus.color = Color.green;
			}
			else if (SubmitButton.gameNotStarted == true) 
			{
				gameStatus.text = "Stopped";
				gameStatus.color = Color.red;

				if (mode != 1) 
				{
					gameLevel.text 	= "-";
					gameRound.text 	= "-";
					gameTime.text 	= "-";
					gameScore.text 	= "-";
				}
			}
			else
			{
				gameStatus.text = "Playing";
				gameStatus.color = Color.black;
				mode = 1;

				//LEVEL------------------------------------------------------
				int myLevel				= SubmitButton.level +1;
				string levelString		= myLevel.ToString();
				gameLevel.text 			= "0" + levelString;

				//ROUND------------------------------------------------------
				string	roundString		= SubmitButton.round.ToString ();
				int roundDigits 		= roundString.Length;

				if (FrequencyGameScore.infiniteLevels == false) 
				{
					if (roundDigits == 1) 
					{
						gameRound.text = "0" + roundString;
					} 
					else if (roundDigits == 2) 
					{
						gameRound.text = roundString;
					} 
				}
				else 
				{
					gameRound.text = "Inf: " + roundString; 
				}

				//TIMER------------------------------------------------------
				if (SubmitButton.gameNotStarted == false) 
				{
					double time = FrequencyGameScore.currentTime;
					double 	timeDouble 		= System.Math.Round ( time, 1 );
					string 	timeString 		= timeDouble.ToString ( "0.0" );
					gameTime.text 			= timeString;
				}

				//SCORE------------------------------------------------------
				string scoreString 		= Manager.Score.ToString ();
				int scoreDigits 		= scoreString.Length;
				gameScore.text 			= scoreString;
			}
		}

		//LOCATION GAME --------------------------------------------------------------------//
		if (locationGame.activeSelf == true) 
		{
			if (LocationManager.gameOver == true) 
			{
				gameStatus.text = "Finished";
				gameStatus.color = Color.green;
			}
			else if (LocationManager.gameNotStarted == true) 
			{
				gameStatus.text = "Stopped";
				gameStatus.color = Color.red;

				if (mode != 2) 
				{
					gameLevel.text 	= "-";
					gameRound.text 	= "-";
					gameTime.text 	= "-";
					gameScore.text 	= "-";
				}
			}
			else
			{
				gameStatus.text = "Playing";
				gameStatus.color = Color.black;
				mode = 2;

				//LEVEL------------------------------------------------------
				int myLevel				= LocationManager.currentLevel;
				string levelString		= myLevel.ToString();
				gameLevel.text 			= "0" + levelString;

				//ROUND------------------------------------------------------
				string	roundString		= LocationManager.Round.ToString ();
				int roundDigits 		= roundString.Length;

				if (LocationScore.LocInfiniteLevels == false) 
				{
					if (roundDigits == 1) 
					{
						gameRound.text = "0" + roundString;
					} 
					else if (roundDigits == 2) 
					{
						gameRound.text = roundString;
					} 
				}
				else 
				{
					gameRound.text = "Inf: " + roundString; 
				}

				//TIMER------------------------------------------------------
				if (LocationManager.gameNotStarted == false) 
				{
					double time = LocationScore.currentTime;
					double 	timeDouble 		= System.Math.Round ( time, 1 );
					string 	timeString 		= timeDouble.ToString ( "0.0" );
					gameTime.text 			= timeString;
				}

				//SCORE------------------------------------------------------
				string scoreString 		= LocationManager.Score.ToString ();
				int scoreDigits 		= scoreString.Length;
				gameScore.text 			= scoreString;
			}
		}

		//BENDING GAME --------------------------------------------------------------------//
		if (bendingGame.activeSelf == true) 
		{
			if (BendingGame.gameOver == true) 
			{
				gameStatus.text = "Finished";
				gameStatus.color = Color.green;
			}
			else
			{
				gameStatus.text = "Playing";
				gameStatus.color = Color.black;
				mode = 3;

				//LEVEL------------------------------------------------------
				int myLevel				= BendingGame.level;
				string levelString		= myLevel.ToString();
				gameLevel.text 			= "0" + levelString;

				//ROUND------------------------------------------------------
				string	roundString		= BendingGame.round.ToString ();
				int roundDigits 		= roundString.Length;

				if (roundDigits == 1) 
				{
					gameRound.text = "0" + roundString;
				} 
				else if (roundDigits == 2) 
				{
					gameRound.text = roundString;
				} 

				//TIMER------------------------------------------------------
				double time = BendingGameScore.currentTime;
				double 	timeDouble 		= System.Math.Round ( time, 1 );
				string 	timeString 		= timeDouble.ToString ( "0.0" );
				gameTime.text 			= timeString;

				//SCORE------------------------------------------------------
				string scoreString 		= BendingGame.scoreTotal.ToString ();
				int scoreDigits 		= scoreString.Length;
				gameScore.text 			= scoreString;
			}
		}

		//FREQUENCY TRAINING --------------------------------------------------------------------//
		if (freqTraining.activeSelf == true) 
		{
			if (TrainingManager.startStimulation == false) 
			{
				gameStatus.text = "Stopped";
				gameStatus.color = Color.red;

				if (mode != 1) 
				{
					gameLevel.text 	= "-";
					gameRound.text 	= "-";
					gameTime.text 	= "-";
					gameScore.text 	= "-";
				}
			}
			else
			{
				gameStatus.text = "Playing";
				gameStatus.color = Color.black;
				mode = 1;

				//LEVEL------------------------------------------------------
				int myLevel				= TrainingManager.level;
				string levelString		= myLevel.ToString();
				gameLevel.text 			= "0" + levelString;

				//ROUND------------------------------------------------------
				string	roundString		= TrainingManager.currentRound.ToString ();
				int roundDigits 		= roundString.Length;

				if (roundString.Length == 1) 
				{
					gameRound.text = "0" + roundString;
				} 
				else 
				{
					gameRound.text = roundString;
				}

				gameTime.text = "...";
				gameScore.text = "...";
			}
		}

		//LOCATION TRAINING --------------------------------------------------------------------//
		if (locTraining.activeSelf == true) 
		{
			if (TrainingLocationManager.startStimulation == false) 
			{
				gameStatus.text = "Stopped";
				gameStatus.color = Color.red;

				if (mode != 2) 
				{
					gameLevel.text 	= "-";
					gameRound.text 	= "-";
					gameTime.text 	= "-";
					gameScore.text 	= "-";
				}
			}
			else
			{
				gameStatus.text = "Playing";
				gameStatus.color = Color.black;
				mode = 2;

				//LEVEL------------------------------------------------------
				int myLevel				= TrainingLocationManager.level;
				string levelString		= myLevel.ToString();
				gameLevel.text 			= "0" + levelString;

				//ROUND------------------------------------------------------
				string	roundString		= TrainingLocationManager.currentRound.ToString ();
				int roundDigits 		= roundString.Length;

				if (roundString.Length == 1) 
				{
					gameRound.text = "0" + roundString;
				} 
				else 
				{
					gameRound.text = roundString;
				}


				gameTime.text = "...";
				gameScore.text = "...";
			}
		}
	}
}
