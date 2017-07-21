using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAtCogwheel : MonoBehaviour 
{
	[Tooltip("Apply object to look at")]
	public GameObject 	lookAtThis;
	public GameObject 	looktAtThisOtherThing;
	public Transform 	chopper;
	private Vector3 lookDir = Vector3.zero;

	void Start () 
	{
		
	}

	void Update () 
	{
		//Rotate the centerCylinder based on the position of the feet 
		if (swapLegControls.maleFemaleAvatar == true)
		{
			lookDir 	= transform.position - lookAtThis.transform.position;
		}
		else
		{
			lookDir 	= transform.position - looktAtThisOtherThing.transform.position;
		}
		
		Vector3 forward1    = Vector3.Cross(lookDir, chopper.transform.right);                  //Create forward z-direction by cross product of y and x directions

		Quaternion newRotation = Quaternion.LookRotation (forward1, -lookDir);
		transform.rotation = newRotation;
	}
}