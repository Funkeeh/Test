using UnityEngine;
using System.Collections;

//Manages visual changes depending on levels within the games
public class LevelManager : MonoBehaviour 
{
	// 	Level:					Description:			Skybox:			Terrain:		Lighting:
	// 	0: Training Session		Grassy Plains			0 (17)			Plains
	// 	1: First Level			Blue Sky				1 (12)			Field
	// 	2: Seconds Level 		Islands (w. Water)		2 (8)			Plains
	// 	3: Thirds Level			Snowy Mountains			3 (15)	 		Grass and Snow
	// 	4: Fourth Level			Desert Mountains		4 (18)			Desert
	//	5: Fifth Level			Foggy Weather			5 (11)	 		Grass and Snow
	public Material[] skyBoxes				= new Material[6];
	private int terrains 		= 0;
	private int startingLevel 	= 0;		//What level the game should start in
	private int	chooseGame		= 0;

	//UI Scoreboard representations of each game to place here
	public GameObject	BendGame;
	public GameObject	LocaGame;
	public GameObject	FreqGame;

	//Terrains used for each level
	public GameObject		plains;
	public GameObject		field;
	public GameObject		grassSnow;
	public GameObject		desert;

	void Start () 
	{
		RenderSettings.skybox = skyBoxes [startingLevel];

		//Set correct Terrain
		plains.SetActive 	(true);
		field.SetActive 	(false);
		grassSnow.SetActive (false);
		desert.SetActive 	(false);
	}

	void Update () 
	{
		//Checks which Game is Active//-------------------------------------------//
		if 		(BendGame.activeSelf == true)
		{
			chooseGame = 1;
		}
		else if (LocaGame.activeSelf == true)
		{
			chooseGame = 2;
		}
		else if (FreqGame.activeSelf == true)
		{
			chooseGame = 3;
		}
		else
		{
			chooseGame = 0;
		}


		//Handles Input from each game//-------------------------------------------//
		switch (chooseGame) 
		{
		case 1:			//Bending Game
			RenderSettings.skybox = skyBoxes [BendingGame.level];
			terrains = BendingGame.level;
			break;
		case 2:			//Location Game
			RenderSettings.skybox = skyBoxes [LocationManager.currentLevel];
			terrains = LocationManager.currentLevel;
			break;
		case 3:			//Frequency Game
			if(SubmitButton.trainingMode == true)
			{
				RenderSettings.skybox = skyBoxes [0];
				terrains = 0;
			}
			else
			{
				RenderSettings.skybox = skyBoxes [SubmitButton.level];
				terrains = SubmitButton.level;
			}
			break;	
		default:		//No Game Active
			RenderSettings.skybox = skyBoxes [startingLevel];
			terrains = 0;
			break;
		}

		setTerrainsActive ();	//Decides which terrain should be active
	}

	//Decides which Terrain is to be active for the current game and level
	void	setTerrainsActive()
	{
		switch (terrains) 
		{
		case 1:
			plains.SetActive 	(false);
			field.SetActive 	(true);
			grassSnow.SetActive (false);
			desert.SetActive 	(false);
			break;
		case 2:
			plains.SetActive 	(true);
			field.SetActive 	(false);
			grassSnow.SetActive (false);
			desert.SetActive 	(false);
			break;
		case 3:
			plains.SetActive 	(false);
			field.SetActive 	(false);
			grassSnow.SetActive (true);
			desert.SetActive 	(false);
			break;
		case 4:
			plains.SetActive 	(false);
			field.SetActive 	(false);
			grassSnow.SetActive (false);
			desert.SetActive 	(true);
			break;
		case 5:
			plains.SetActive 	(false);
			field.SetActive 	(false);
			grassSnow.SetActive (true);
			desert.SetActive 	(false);
			break;
		default:
			plains.SetActive 	(true);
			field.SetActive 	(false);
			grassSnow.SetActive (false);
			desert.SetActive 	(false);
			break;
		}
	}
}
