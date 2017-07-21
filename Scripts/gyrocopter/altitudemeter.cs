using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class altitudemeter : MonoBehaviour 
{
	private float altitudeAngle 	= 0.0f;
	private float baseAngle 		= -125.0f;
	private float currentAltitude	= 0.0f;

	//public Transform scoreTest;

	[Tooltip("0 = altitudeArrow, 1 = destinationArrow")]
	public int version				= 0;

	void Start () 
	{
		//Nothing
	}

	void Update ()
	{
		if (version == 0)
		{
			currentAltitude = addForce.altitude;
		}
		else
		{	
			currentAltitude = levelManager.checkPointAltitude + 10.0f;
		}


		if (currentAltitude < 0.0f) 
		{
			altitudeAngle = baseAngle;
		}
		else if (currentAltitude > 0.0f && currentAltitude < 250.0f) 
		{
			altitudeAngle = baseAngle + currentAltitude;
		}
		else
		{
			altitudeAngle = -baseAngle;
		}

		if (altitudeAngle > 125.0f) 
		{
			altitudeAngle = -baseAngle;
		}

		transform.localEulerAngles = new Vector3 (0, altitudeAngle, 0);
	}
}