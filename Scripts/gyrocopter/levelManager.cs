using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelManager : MonoBehaviour 
{
	public static bool levelIsActive = false;
	public GameObject helicopter;
	public Transform checkpoints;
	public Transform goalArea;
	public GameObject fadeToBlack;
	private float newAlphaValue = 0.0f;
	private Color initialFade;
	public AudioSource hit;
	public AudioSource miss;
	private Transform[] checkpointPos 		= new Transform[100];
	private  Vector3[] initialPositions 	= new Vector3[100];
	private int currentCheckPoint 	= 0;

	public static float checkPointAltitude = 0.0f;	//For altitudemeter script

    public static AudioSource hitStatic;
    public static AudioSource missStatic;

    void Start () 
	{
		setCheckpointPositions();
		initialFade = fadeToBlack.GetComponent<Renderer>().material.color;
		levelIsActive = true;

        hitStatic = hit;
        missStatic = miss;

    }

	void Update () 
	{
		float distance 		= checkpointPos[currentCheckPoint].position.z - helicopter.transform.position.z;			//Distance between active checkpoint and helicpoter
		float goalDist 		= goalArea.position.z - helicopter.transform.position.z;									//Dist between goal and chopper
		checkPointAltitude 	= checkpointPos[currentCheckPoint].position.y;												//For altitude meter

		//Debug.Log ("Goal Distance " + goalDist);
		
		if (distance > 0.0f && levelIsActive == true)																	//Go to next checkpoint when distance is positive
		{
			if (checkPointAltitude > helicopter.transform.position.y - 6.0f && checkPointAltitude < helicopter.transform.position.y + 6.0f)
			{
				hit.Play();
			}
			else
			{
				miss.Play();
			}

			checkpointPos[currentCheckPoint].position = new Vector3(0.0f, -200.0f, 0.0f);								//"Remove" past checkpoints
			currentCheckPoint = currentCheckPoint + 1;																	//Go to next checkp
		}
		//Fade to Black when done ---------------------------------- //
		if (goalDist > 0.0f)
		{
			Debug.Log("FINISH LINE HERE!!!!!11111");
			newAlphaValue = newAlphaValue + Time.deltaTime;
			fadeToBlack.GetComponent<Renderer>().enabled = true;
			Color value = new Color (initialFade.r, initialFade.g, initialFade.b, newAlphaValue);
			fadeToBlack.GetComponent<Renderer>().material.SetColor("_Color", value);
		}
		else
		{
			Color value = new Color (initialFade.r, initialFade.g, initialFade.b, 0.0f);
			fadeToBlack.GetComponent<Renderer>().material.SetColor("_Color", value);
			fadeToBlack.GetComponent<Renderer>().enabled = false;
			newAlphaValue = 0.0f;
		}
	}

	//Gets the transform info from all children and stores in array
	void setCheckpointPositions ()
	{
		int i = 0;
		foreach (Transform child in checkpoints)
		{
			checkpointPos[i] = checkpoints.GetChild(i);
			initialPositions[i] = checkpoints.GetChild(i).position;
            i++;
		}
	}

	public void resetCheckPointPos()
	{
		int i = 0;
		foreach (Transform child in checkpoints)
		{
			checkpoints.GetChild(i).position = initialPositions[i];
			i++;
			//Debug.Log("CHILD POS " + checkpoints.GetChild(i).position + " INITIAL POS " + initialPositions[i]);
		}
        currentCheckPoint = 0; 
    }

    public static void HitTarget()
    {
        hitStatic.Play();
    }
    public static void MissTarget()
    {
        missStatic.Play();
    }
}