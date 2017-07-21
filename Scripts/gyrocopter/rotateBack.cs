using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateBack : MonoBehaviour 
{
	void Update () 
	{
		//Define RotationSpeed of backRotater 
		switch (addForce.backRotaterSpeed)
		{
		case 0:
			//Nothing
			break;
		case 1:
			transform.RotateAround ( GetComponent<Transform>().position, Vector3.forward, Time.deltaTime * 400.0f );
			break;
		case 2:
			transform.RotateAround ( GetComponent<Transform>().position, Vector3.forward, Time.deltaTime * 500.0f );
			break;
		case 3:
			transform.RotateAround ( GetComponent<Transform>().position, Vector3.forward, Time.deltaTime * 600.0f );
			break;
		default:
			//Nothing
			break;
		}
	}
}