using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speedometer : MonoBehaviour 
{
	private float speed 		= 0.0f;
	private float speedAngle 	= 0.0f;

	private float baseAngle = -118.0f;

	void Start () 
	{
		//Nothing
	}

	void Update () 
	{
		speed = addForce.velocityZ * -3.6f;

		if (speed > 3.0f && startPoint.hitStartPoint == true) 
		{
			speedAngle = (speed * 3.0f) + baseAngle;
		}
		else
		{
			speedAngle = baseAngle;
		}
		//Debug.Log (speed);

		transform.localEulerAngles = new Vector3(0, speedAngle, 0);
	}
}