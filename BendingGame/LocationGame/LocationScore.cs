using UnityEngine;
using System.Collections;

public class LocationScore : MonoBehaviour 
{
	public TextMesh userName;
	public TextMesh level;
	public TextMesh round;
	public TextMesh score;
	public TextMesh time;
	public static double currentTime;
	public static bool	LocInfiniteLevels = false;

	public GameObject roundGOround;

	// Update is called once per frame
	void Update () 
	{
		if (LocationManager.gameOver == false) 
		{
			//Check if round bar is present in UI
			if (roundGOround.activeSelf == false) 
			{
				roundGOround.SetActive (true);
			}

			//LEVEL------------------------------------------------------
			int myLevel				= LocationManager.currentLevel;
			string levelString		= myLevel.ToString();
			level.text 				= "0" + levelString;

			//ROUND------------------------------------------------------
			string	roundString		= LocationManager.Round.ToString ();
			int roundDigits 		= roundString.Length;

			if (LocInfiniteLevels == false) 
			{
				if (roundDigits == 1) 
				{
					round.text = "0" + roundString;
				} 
				else if (roundDigits == 2) 
				{
					round.text = roundString;
				} 
			} else 
			{
				round.text = "(" + roundString + ")"; 
			} 

			//SCORE------------------------------------------------------
			string scoreString 		= LocationManager.Score.ToString ();
			int scoreDigits 		= scoreString.Length;

			if 		( scoreDigits == 1 ) 
			{
				score.text 			= "00" + scoreString;
			}
			else if ( scoreDigits == 2 ) 
			{
				score.text 			= "0"  + scoreString;
			} 
			else if ( scoreDigits == 3 )
			{
				score.text 			=        scoreString;
			}

			//TIMER------------------------------------------------------
			if (LocationManager.gameNotStarted == false) 
			{
				currentTime += Time.deltaTime;
				double 	timeDouble 		= System.Math.Round ( currentTime, 1 );
				string 	timeString 		= timeDouble.ToString ( "0.0" );
				time.text 				= timeString;
			}
		} 
		else 
		{
			level.text = "Game Finished";
			roundGOround.SetActive (false);
		}

	}
}