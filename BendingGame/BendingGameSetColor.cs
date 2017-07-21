using UnityEngine;
using System.Collections;

public class BendingGameSetColor : MonoBehaviour 
{
	public enum Sides
	{
		left 	= 0,
		right 	= 1
	};

	public 	Sides sides = Sides.left;

	private int	colorValue = 0;

	//GameObject Mats
	public 	Material		green;
	public 	Material		red;
	public 	Material		blue;
	public 	Material		yellow;
	//Particle Mats
	public 	Material		greenBubble;
	public 	Material		redBubble;
	public 	Material		blueBubble;
	public 	Material		yellowBubble;

	protected bool letPlay = true;

	public ParticleSystem	thisPS;

	void Update ()
	{
		if (sides == Sides.left) 
		{
			colorValue = BendingGame.colorLeftSide;
		} 
		else 
		{
			colorValue = BendingGame.colorRightSide;
		}

		//Start Particle System if next Round Starts
		if (BendingGame.startNextRound) 
		{
			if (!thisPS.isPlaying) 
			{
				thisPS.Play ();
			}
		} 
		else 
		{
			if (!thisPS.isPlaying) 
			{
				thisPS.Stop ();
			}
		}

		//Set Color Values based on Random Value from BendingGame
		switch ( colorValue )
		{
		case 0:
			//Startup
			break;
		case 1:		//Green
			GetComponent<Renderer> ().material							= green;
			thisPS.GetComponent<ParticleSystemRenderer> ().material 	= greenBubble;
			break;	

		case 2:		//Red
			GetComponent<Renderer> ().material							= red;
			thisPS.GetComponent<ParticleSystemRenderer> ().material 	= redBubble;
			break;	

		case 3:		//Blue
			GetComponent<Renderer> ().material							= blue;
			thisPS.GetComponent<ParticleSystemRenderer> ().material 	= blueBubble;
			break;	

		case 4:		//Yellow
			GetComponent<Renderer> ().material							= yellow;
			thisPS.GetComponent<ParticleSystemRenderer> ().material 	= yellowBubble;
			break;	

		default: 	//No Color
			Debug.Log ( "No Color here. How did you get here anyway?" );
			break;	
		}
	}
}