using UnityEngine;
using System.Collections;

public class BendingGame : MonoBehaviour 
{
	//Cylinder and GameObjects / Handlers ----------------------------------------------------------------------------------------------------------------------//
	public GameObject cylinderHandle;						//GO: BG_CylinderLeft > Handle
	public GameObject cylinderJoint;						//GO: BG_CylinderLeft > Joint
	public GameObject gameCylinder;							//GO: BG_GameObjectLeft
	public GameObject gameHandle;							//GO: BG_GameObjectLeft > Handle
	public GameObject gameJoint;							//GO: BG_GameObjectLeft > Joint

	public GameObject gameResetPos;							//GO: BG_ResetPosition //--//Cube representing resetposition area for Goal Area
	public AudioSource newLevel;							//Audio Source > AudioClip: newLevel
	public AudioSource newRound;
	public AudioSource wrong;

	//GameObject and Cylinder Variables ------------------------------------------------------------------------------------------------------------------------//
	private float 	jointDistanceStart		= 0.50f;
	private float 	jointDistanceEnd		= 0.10f;
	private float 	handleDistanceStart 	= 0.80f;
	private float 	handleDistanceEnd 		= 0.20f;

	//Base Score Points ----------------------------------------------------------------------------------------------------------------------------------------//
	public float	scoreJointBase	 		= 10.0f;
	public float	scoreHandleBase			= 10.0f;
	//public float	scoreBonus				= 5.0f;
	//public float	scoreTimeBase 			= 50.0f;

	//Score Values and Game Mechanics --------------------------------------------------------------------------------------------------------------------------//
	private float	scoreJoint				= 0.0f;
	private float	scoreHandle				= 0.0f;
	private float	scoreRound				= 0.0f;
	public static float	scoreTotal			= 0.0f;			//Total score, used in BendingGameScore script
	public static float roundValue			= 0.0f;			//Round score, used in BendingGameScoreParticle script

	private float 	percentage				= 0.0f;
	private float 	percentageH				= 0.0f;
	public static float	combinedPercentage	= 0.0f;			//To manage goal area transparency, BendingGameTransparency script

	private bool 	jointPosition 			= false;
	private bool 	handlePosition 			= false;

	private float	timer					= 0.0f;

	public static int		round			= 1;
	public static int		level			= 1;
	public static bool 		startNextRound	= false;		//Check if the game starts a new round (used in multiple scrips)
	public static bool		gameOver		= false;		//Checks if game is finished (used in multiple scrips)

	public static int 		colorLeftSide 	= 0;			//Color cylinder values
	public static int 		colorRightSide 	= 0;

	//Difficulty Variables -------------------------------------------------------------------------------------------------------------------------------------//

	//1. Positions and Scale Cube-Startposition;  
	//Distribution (easy, medium, hard)		   Pos,Scale     Pos,Scale      Pos,Scale
	private float[] 	resetPosScaleX 		= {  -2.0f, 1.0f,  -2.5f, 1.1f,   -2.5f, 1.25f };
	private float[] 	resetPosScaleY 		= { 12.25f, 1.5f, 12.25f, 2.0f,  12.25f, 2.5f };
	private float[] 	resetPosScaleZ 		= {  7.25f, 0.5f,  7.25f, 0.75f,  7.25f, 1.0f };
	//2. Rotations: Distribution	   	        Init       Easy          Medium          Hard
	//										       0 	   1	 2  	  3      4 		 5     6
	private float[] 	resetRotZ			= { 0.0f,  -1.0f, 0.0f,  -1.50f, 0.00f,  -1.75f, 0.10f };
	private float[] 	resetRotX 			= { 0.0f,   0.0f, 0.0f,  -0.15f, 0.20f,  -0.25f, 0.25f };

	//Moving the Game Cylinder
	//Variables: X-Axis: Easy: resetPosScaleX 0,1; Medium: resetPosScaleX 2,3; Hard: resetPosScaleX 4,5
	//			 Y and Z-Axis = gameCylinder.transform.position
	private Vector3	translateMin		= new Vector3 ( 0, 0, 0 );
	private Vector3 translateMax 		= new Vector3 ( 0, 0, 0 );
	//										Easy  Med   Hard
	private float[]  setTranslateSpeed	= { 0.1f, 0.2f, 0.3f };
	private float	translateSpeed		= 0.0f;
	private bool 	keepMoving			= true;				//Allow Movment Game Mechanic
	private bool 	moveRight 			= false; 
	private bool 	moveLeft 			= true;

	private int 	StartNextRoundOnce 	= 0;	//Makes sure the StartNextRound Only Loops Once

	void Awake() 
	{
		gameCylinder.transform.rotation = new Quaternion (0, 0, 0, 1);		//Reset rotation of GameCylinder	
	}

	void Start () 
	{
		StartNextRound ();
	}
	
	void Update () 
	{
		startNextRound = false;

		//Check object handle and joint distances
		float handleDist 		= ( cylinderHandle.transform.position 	- gameHandle.transform.position ).magnitude;
		float jointDist			= ( cylinderJoint.transform.position 	- gameJoint.transform.position 	).magnitude;

		/*------------------------------- IF-STATEMENTS INCOMIMG - HOLD ON TO YOUR HAT ------------------------------------*/

		//Calculates points and enabling round completion for the Joint Position
		if ( jointDist < jointDistanceStart && jointDist > jointDistanceEnd )
		{
			float distance 		= jointDistanceStart 	- jointDistanceEnd;				
			float distanceUser	= jointDist 			- jointDistanceEnd;
			percentage 			= 1.0f - ( distanceUser / distance );
			scoreJoint 			= Mathf.Round (percentage * 10.0f);
			jointPosition		= true;
		}
		else if ( jointDist < jointDistanceEnd )
		{
			scoreJoint  		= scoreJointBase;
			percentage 			= 10.0f;
			jointPosition 		= true;
		}
		else
		{
			scoreJoint 			= 0.0f;
			percentage 			= 0.0f;
			jointPosition 		= false;
		}

		//Calculates points and enabling round completion for the for Handle Position
		if ( handleDist < handleDistanceStart && handleDist > handleDistanceEnd )
		{
			float distance 		= handleDistanceStart 	- handleDistanceEnd;				
			float distanceUser	= handleDist 			- handleDistanceEnd;
			percentageH 		= 1.0f - ( distanceUser / distance );
			scoreHandle 		= Mathf.Round (percentageH * 10.0f);
			handlePosition 		= true;
		}
		else if ( handleDist < handleDistanceEnd )
		{
			scoreHandle  		= scoreHandleBase;
			percentageH 		= 10.0f;
			handlePosition 		= true;
		}
		else
		{
			scoreHandle			= 0.0f;
			percentageH 		= 0.0f;
			handlePosition 		= false;
		}
		//Debug.Log ("Joint Points: " + scoreJoint + " and Handle Points: " + scoreHandle);

		combinedPercentage = (percentage + percentageH) / 2.0f;

		//Checks if Cylinder (Nunchuck) is within the acceptable range of the Game Cylinder AND The User is releasing the Nunchuck
		//If true: Then stop the movement of the Game Cylinder, thus keeping it within point range and enabling pressing button for points
		if (jointPosition == true && BendingGameCylinderHandler.relasingCylinder == true || handlePosition == true && BendingGameCylinderHandler.relasingCylinder == true) 
		{
			keepMoving = false;
		} 
		else 
		{
			keepMoving = true;
		}

		//Manages moving the Game Object/Cylinder left and right depending on current level, difficulty and position
		if (level != 1 && keepMoving == true) 
		{
			if 		( gameCylinder.transform.position.x == translateMin.x ) 
			{
				moveRight = true;
				moveLeft = false;
			} 
			if 		( gameCylinder.transform.position.x == translateMax.x ) 
			{
				moveRight = false;
				moveLeft = true;
			} 

			if (moveLeft == true) 	//Go Left
			{
				gameCylinder.transform.position = Vector3.MoveTowards (gameCylinder.transform.position, translateMin, translateSpeed * Time.deltaTime);
			}
			if (moveRight == true) 	//Go Right
			{
				gameCylinder.transform.position = Vector3.MoveTowards (gameCylinder.transform.position, translateMax, translateSpeed * Time.deltaTime);
			}
		}

		//Submit position and receive points
		if( BendingGameButtonActivator.pressButton == true )
		{
			for (int i = StartNextRoundOnce; i < 1; i++) //Makes sure the StartNextRound Only Loops Once
			{
				if (jointPosition == false && handlePosition == false) 
				{
					if ( timer < 0.0f ) 
					{
						wrong.Play ();
						timer = wrong.clip.length;
					}

					BendingGameButtonActivator.TextureIdBG = 2;

					Debug.Log ("Error. Handle out of range. Try again");

				} 
				else 
				{
					if ( round < 11 && timer < 0.0f ) 
					{
						newRound.Play ();
						timer = newRound.clip.length;
					}

					BendingGameButtonActivator.TextureIdBG = 1;

					scoreRound = scoreJoint + scoreHandle;
					scoreTotal += scoreRound;	

					roundValue = scoreRound;

					ManageRoundsAndLevels ();							//Manages Levels and Rounds
					StartNextRound ();									//Manages which difficulties to apply when, based on levels

					//Reset Materials on Cylinder (Nunchuck) and Game Cylinder
					int[] setMaterial = { Random.Range (1, 5), Random.Range (1, 5) };
					if (setMaterial [0] == setMaterial [1]) 
					{
						if (setMaterial [1] == 4) 
						{
							setMaterial [1]--;
						} 
						else 
						{
							setMaterial [1]++;
						}
					}

					colorLeftSide  = setMaterial [0];
					colorRightSide = setMaterial [1];

					startNextRound = true;
				}
			}
			StartNextRoundOnce = 1; //Makes sure the StartNextRound Only Loops Once
		}
		else if (BendingGameButtonActivator.pressButton == false)
		{
			StartNextRoundOnce = 0; //Makes sure the StartNextRound Only Loops Once
		}

		//Timer for Audio Sources
		if (timer > -2.0f) 
		{
			timer += -Time.deltaTime;
			//Debug.Log ("Timer is: " + timer);
		}
	}
		
	//--- Functions used in the game ---------------------------------------------------------------------------------------------//

	//Handle Rounds and Level
	//Game runs: 5 Levels with 9 Rounds per Level
	void ManageRoundsAndLevels ()
	{
		if 		( round == 10 && level == 5 )
		{
			Debug.Log ("The Game is finished");
			gameCylinder.transform.position = new Vector3 (0.0f, -10.0f, 0.0f);
			gameOver = true;
		}
		else if ( round <  10 && level <  6 ) 
		{
			round++;	//Go 1 round up
		}
		else if ( round == 10 && level <  6 )
		{
			round = 1;
			level++;	//Go 1 level up, reset round to 1
			newLevel.Play ();
			timer = newLevel.clip.length;
		}
	}

	//Get Variables Based on Round and Level, and Sets the Position, Rotation and Mechanics for the next Round
	void StartNextRound()
	{
		float rotXmin; float rotXmax; 		//Placeholders to assign current rotations
		float rotZmin; float rotZmax;		

		//Manages Levels
		switch (level)
		{
		case 1:		//Level 01
			//POSITION: EASY
			gameResetPos.transform.position 	= new Vector3 ( resetPosScaleX [0], resetPosScaleY [0], resetPosScaleZ [0] );
			gameResetPos.transform.localScale 	= new Vector3 ( resetPosScaleX [1], resetPosScaleY [1], resetPosScaleZ [1] );

			//ROTATION: EASY
			rotXmin = resetRotX [1]; rotXmax = resetRotX [2]; 
			rotZmin = resetRotZ [1]; rotZmax = resetRotZ [2];

			//MOVEMENT: NONE
			break;

		case 2:		//Level 02
			//POSITION: EASY
			gameResetPos.transform.position 	= new Vector3 ( resetPosScaleX [0], resetPosScaleY [0], resetPosScaleZ [0] );
			gameResetPos.transform.localScale 	= new Vector3 ( resetPosScaleX [1], resetPosScaleY [1], resetPosScaleZ [1] );

			//ROTATION: MEDIUM
			rotXmin = resetRotX [1]; rotXmax = resetRotX [2]; 
			rotZmin = resetRotZ [1]; rotZmax = resetRotZ [2];

			//MOVEMENT: EASY
			translateMin 	= new Vector3 ( resetPosScaleX[0] - (resetPosScaleX[1] / 2), gameCylinder.transform.position.y, gameCylinder.transform.position.z );
			translateMax 	= new Vector3 ( resetPosScaleX[0] + (resetPosScaleX[1] / 2), gameCylinder.transform.position.y, gameCylinder.transform.position.z );
			translateSpeed 	= setTranslateSpeed[0];
			break;
		
		case 3:		//Level 03
			//POSITION: MEDIUM
			gameResetPos.transform.position 	= new Vector3 ( resetPosScaleX [2], resetPosScaleY [2], resetPosScaleZ [2] );
			gameResetPos.transform.localScale 	= new Vector3 ( resetPosScaleX [3], resetPosScaleY [3], resetPosScaleZ [3] );

			//ROTATION: HARD
			rotXmin = resetRotX [3]; rotXmax = resetRotX [4]; 
			rotZmin = resetRotZ [3]; rotZmax = resetRotZ [4];

			//MOVEMENT: EASY
			translateMin 	= new Vector3 ( resetPosScaleX[0] - (resetPosScaleX[1] / 2), gameCylinder.transform.position.y, gameCylinder.transform.position.z );
			translateMax 	= new Vector3 ( resetPosScaleX[0] + (resetPosScaleX[1] / 2), gameCylinder.transform.position.y, gameCylinder.transform.position.z );
			translateSpeed 	= setTranslateSpeed[0];
			break;
		
		case 4:		//Level 04
			//POSITION: HARD
			gameResetPos.transform.position 	= new Vector3 ( resetPosScaleX [4], resetPosScaleY [4], resetPosScaleZ [4] );
			gameResetPos.transform.localScale 	= new Vector3 ( resetPosScaleX [5], resetPosScaleY [5], resetPosScaleZ [5] );

			//ROTATION: HARD
			rotXmin = resetRotX [3]; rotXmax = resetRotX [4]; 
			rotZmin = resetRotZ [3]; rotZmax = resetRotZ [4];

			//MOVEMENT: MEDIUM
			translateMin 	= new Vector3 ( resetPosScaleX[2] - (resetPosScaleX[3] / 2), gameCylinder.transform.position.y, gameCylinder.transform.position.z );
			translateMax 	= new Vector3 ( resetPosScaleX[2] + (resetPosScaleX[3] / 2), gameCylinder.transform.position.y, gameCylinder.transform.position.z );
			translateSpeed 	= setTranslateSpeed[1];
			break;
		
		case 5:		//Level 05
			//POSITION: HARD
			gameResetPos.transform.position 	= new Vector3 ( resetPosScaleX [4], resetPosScaleY [4], resetPosScaleZ [4] );
			gameResetPos.transform.localScale 	= new Vector3 ( resetPosScaleX [5], resetPosScaleY [5], resetPosScaleZ [5] );

			//ROTATION: HARD
			rotXmin = resetRotX [5]; rotXmax = resetRotX [6]; 
			rotZmin = resetRotZ [5]; rotZmax = resetRotZ [6];

			//MOVEMENT: HARD
			translateMin 	= new Vector3 ( resetPosScaleX[4] - (resetPosScaleX[5] / 2), gameCylinder.transform.position.y, gameCylinder.transform.position.z );
			translateMax 	= new Vector3 ( resetPosScaleX[4] + (resetPosScaleX[5] / 2), gameCylinder.transform.position.y, gameCylinder.transform.position.z );
			translateSpeed 	= setTranslateSpeed[2];
			break;
		
		default:	//Dummy Level
			//ROTATION: INITIAL
			rotXmin = resetRotX [0]; rotXmax = resetRotX [0]; 
			rotZmin = resetRotZ [0]; rotZmax = resetRotZ [0];
			break;
		}

		//Get and Set random position based on level
		Vector3 getPosition 	= gameResetPos.transform.position;
		Vector3 getScale 		= new Vector3 ( gameResetPos.transform.localScale.x, gameResetPos.transform.localScale.y, gameResetPos.transform.localScale.z );

		Vector3 setMin 			= new Vector3 ( getPosition.x - (getScale.x / 2), getPosition.y - (getScale.y / 2), getPosition.z - (getScale.z / 2) );
		Vector3 setMax 			= new Vector3 ( getPosition.x + (getScale.x / 2), getPosition.y + (getScale.y / 2), getPosition.z + (getScale.z / 2) );
		Vector3 setPosition 	= new Vector3 ( Random.Range (setMin.x, setMax.x), Random.Range (setMin.y, setMax.y), Random.Range (setMin.z, setMax.z) );
		gameCylinder.transform.position 	= setPosition;

		//Get and Set random rotation based on level
		Quaternion setRotation	= new Quaternion ( Random.Range (rotXmin, rotXmax), 0.0f, Random.Range (rotZmin, rotZmax), 1.0f );
		gameCylinder.transform.rotation 	= setRotation;
	}

	//Use to Reset All variables (For UI)
	public void ResetEverything ()
	{
		round = 1;
		level = 1;
		scoreTotal = 0;
		BendingGameScore.currentTime = 0;

		gameOver = false;
		BendingGameButtonActivator.TextureIdBG = 0;
		
		StartNextRound ();	
		startNextRound = true;
	}
}