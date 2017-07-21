using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour 
{
	public float 	rotateSpeed = 0.0f;
	public int 		rotateOrientations = 0;

	void Update () 
	{
		if (rotateOrientations == 0) 
		{
			transform.RotateAround ( GetComponent<Transform>().position, Vector3.up, Time.deltaTime * rotateSpeed );
		}
		else if (rotateOrientations == 1) 
		{
			transform.RotateAround ( GetComponent<Transform>().position, Vector3.forward, Time.deltaTime * rotateSpeed );
		}
		else if (rotateOrientations == 2) 
		{
			transform.RotateAround ( GetComponent<Transform>().position, Vector3.right, Time.deltaTime * rotateSpeed );
		}
	}
}