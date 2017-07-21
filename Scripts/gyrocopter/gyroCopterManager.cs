using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gyroCopterManager : MonoBehaviour 
{
	public static int ScoreTotal 		= 0;
	public static bool enoughVelocity 	= false; 			//When to apply force.up
	//public static bool enoughVelocityY 	= false;		//When to apply force.up (for cycling game)
	public static float appliedForce 	= 0.0f;				//Amount of force applied
	public static int	level			= 1;				//Set level
	public static bool	gameStart		= false;			//Bool to start and stop the game

	public static float velocityLimit 	= 1.5f;

	public float viveControllerForce 	= 1000.0f;
	public float viveControllerSwing	= 1000.0f;
	public float viveControllerCycle	= 1000.0f;

	private float velocity 		= 0.0f;						//Force Variables
	private float velocityUp	= 0.0f;
	private float zDirection 	= 0.0f;
	private float yDirection 	= 0.0f;
	private float lastZ 		= 0.0f;
	private float lastY			= 0.0f;

	//Audio	
	public AudioSource engineStart;
	public AudioSource engineRunning;
	public AudioSource movingForward;
	private bool startEngineOnce = false;

	//Levels
	public GameObject level01;
	public GameObject level02;
	public GameObject gyroCopter;

	void Start () 
	{
        levelManager(level);
    }

	void Update () 
	{
		//Force Manager HTC VIVE Controller --------------------------------------//
		//Forward direction force 
		zDirection = swapControllers.activeFoot.position.z - addForce.velocity;
		velocity = (zDirection - lastZ) / Time.deltaTime;
		lastZ = zDirection;

		if (velocity < -velocityLimit || velocity > velocityLimit)		//Velocitylimit changes based on game version 
		{
			//enoughVelocity = true;
		}
		else
		{
			//enoughVelocity= false;
		}
		//Upward direction force 
		yDirection = swapControllers.activeFoot.position.y - addForce.velocityY;
		velocityUp = (yDirection - lastY) / Time.deltaTime;
		lastY = yDirection;
		//Debug.Log (velocityUp);
		if (velocityUp < -0.8f || velocityUp > 0.8f)
		{
			//enoughVelocityY = true;
		}
		else
		{
			//enoughVelocityY= false;
		}

		//Force Type Manager -----------------------------------------------------//
		if (swapLegControls.legMirrorVersions == 0) 
		{
			appliedForce = viveControllerForce;
		} 
		else if (swapLegControls.legMirrorVersions == 1) 
		{
			appliedForce = viveControllerSwing;
		}
		else if (swapLegControls.legMirrorVersions == 2) 
		{
			appliedForce = viveControllerCycle;
		}

		if (startPoint.hitStartPoint == true) 
		{
			if (startEngineOnce == false) 
			{
				movingForward.Play();
                Debug.Log("DING DONG");
				startEngineOnce = true;
			}
		}
	}

	//Level Manager --------------------------------------------------------------//
	public void levelManager (int setLevel)
	{
		switch (setLevel) 
		{
		case 0:
			level01.SetActive (true);
			level02.SetActive (false);

			gyroCopter.transform.position 					= new Vector3( 0.0f, 0.185f, 0.0f ); //Reset position of chopper
			gyroCopter.GetComponent<Rigidbody> ().velocity 	= new Vector3( 0.0f, 0.0f,   0.0f ); //Reset velocity of chopper
            level = 0;

            break;
		case 1:	
			level01.SetActive (false);
			level02.SetActive (true);

            gyroCopter.transform.position                   = new Vector3( 0.0f, 171.8f,  0.0f ); //Reset position of chopper
            gyroCopter.GetComponent<Rigidbody>().velocity   = new Vector3( 0.0f,   0.0f,  0.0f ); //Reset velocity of chopper
            level = 1;
            break;
		default:
			break;
		}
		//Debug.Log ("Current level is "  + setLevel); 
	}

	//FOR UI
	public void startStopGame ()
	{
		if (gameStart == true) 
		{
			gameStart = false;
			levelManager (level);
			startPoint.hitStartPoint = false;
            startEngineOnce          = false;
            ScoreTotal = 0;
			Debug.Log ("Game Stopped"); 
		} 
		else 
		{
			gameStart = true;
			levelManager (level);
			Debug.Log ("Game Started"); 
		}
	}

	IEnumerator playEngineSound()
	{
		engineStart.Play ();
		yield return new WaitForSeconds (engineStart.time);
		engineRunning.Play ();
	}
}