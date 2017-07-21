using UnityEngine;
using System.Collections;

public class BendingGameScore : MonoBehaviour 
{
	public TextMesh userName;
	public TextMesh level;
	public TextMesh score;
	public TextMesh time;
	public static double currentTime;

	public GameObject round;
	public Material[] roundMats = new Material[11];

	// Update is called once per frame
	void Update () 
	{
		if (BendingGame.gameOver == false) 
		{
			//Check if round bar is present in UI
			if (round.activeSelf == false) 
			{
				round.SetActive (true);
			}

			//LEVEL------------------------------------------------------
			string	levelString		= BendingGame.level.ToString ();
			int 	levelDigits 	= levelString.Length;

			//Handle Scoreboard visualiser
			if (levelDigits == 1) 
			{
				level.text = "0" + levelString;
			} 
			else 
			{
				level.text = levelString;
			}

			//ROUND------------------------------------------------------
			round.GetComponent<Renderer>().material = roundMats[BendingGame.round];

			//SCORE------------------------------------------------------
			float 	scoreBoard 		= BendingGame.scoreTotal;
			string scoreString 		= scoreBoard.ToString ();
			int scoreDigits 		= scoreString.Length;

				//Handle Scoreboard visualiser
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
			currentTime += Time.deltaTime;
			double 	timeDouble 		= System.Math.Round ( currentTime, 1 );
			string 	timeString 		= timeDouble.ToString ( "0.0" );
			time.text 				= timeString;
		}

		else if(BendingGame.gameOver == true)
		{
			round.SetActive (false);
			level.text = "Game Finished!";
		}

		//Debug.Log ("Score: " + score.text + " Time: " + time.text + "Level and Round: " + level.text + ", " + round.text);
	}
}
