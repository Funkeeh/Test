using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPoint : MonoBehaviour 
{
	public 	Material 	hitObject;
	private Material 	currentObject;

	private bool once = false;

	void Start () 
	{
		currentObject = GetComponent<Renderer> ().material;
	}

	void OnTriggerEnter()
	{
		//GetComponent<Renderer> ().material = hitObject;
	
		if (once == false) 
		{
			gyroCopterManager.ScoreTotal += 1;
			Debug.Log ("Score is " + gyroCopterManager.ScoreTotal);
			once = true;

           // levelManager.HitTarget();
		}
	}
	void OnTriggerExit()
	{
        //GetComponent<Renderer> ().enabled = false;
        once = false;
	}
}