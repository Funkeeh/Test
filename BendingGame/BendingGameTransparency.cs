using UnityEngine;
using System.Collections;

public class BendingGameTransparency : MonoBehaviour 
{
	private Color 	color;
	public static bool enableRend = false;

	void Start () 
	{
		color = 	GetComponent<Renderer> ().material.color;						//Get the color material from object
	}

	void Update () 
	{
		if (BendingGame.gameOver == false) 
		{
			float val = BendingGame.combinedPercentage;									//Get position value from BendingGame

			//Full transparency if game is done
			if (BendingGame.gameOver == true) 
			{
				color.a = 0.00f;
			}
			//Set initial transparency of the game objects (If a new round started and the user is not grabbing the nunchuck)
			else if ( BendingGame.startNextRound == true && BendingGameCylinderHandler.grabCylinderWorld == false ) 
			{
				color.a = 0.25f;
				enableRend = false;
			}

			//If user grabs the nunchuck
			if (BendingGameCylinderHandler.grabCylinderWorld == true) 
			{
				if (val < 0.25f) 
				{
					color.a = 75.0f;
					enableRend = false;
				} 
				else if (val > 0.25f && val < 0.80f) 
				{
					float clr = 1.0f - BendingGame.combinedPercentage;
					if (clr < 0.25f) 
					{
						clr = 0.25f;
					}
					color.a = clr;
					enableRend = true;
				} 
				else 
				{
					color.a = 0.25f;
					enableRend = true;
				}
			}

			GetComponent<Renderer> ().material.SetColor ("_Color", color);				//Set new alpha color value

			//Views values to grab the cylinder
			//		Debug.Log (	"Min Index Bend: " + BendingGameCylinderHandler.minBendIndexInput + "; Setting is: " + BendingGameCylinderHandler.minBendIndexSetting 			+ " ... " + 
			//					"Min Index To Wrist: " + BendingGameCylinderHandler.minIndextoHandInput + "; Setting is: " + BendingGameCylinderHandler.minIndextoHandSetting 	+ " ... " + 
			//					"Min Dist To Cylinder input: " + BendingGameCylinderHandler.minDistancetoCylInput + "; Setting is: " + BendingGameCylinderHandler.minDistancetoCylSetting);
		} 
		else 
		{
			color.a = 0.0f;
		}
	}
}