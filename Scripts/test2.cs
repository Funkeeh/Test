using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test2 : MonoBehaviour 
{
	public GameObject cube;
	public GameObject controller;
	void Start () 
	{
		
	}

	void Update () 
	{	
		Vector3 newPos = new Vector3 (-controller.transform.position.x, controller.transform.position.y, controller.transform.position.z); 		
		cube.transform.position = newPos;
	}
}
