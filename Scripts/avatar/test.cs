using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour 
{
	public GameObject head;
	//private Quaternion testQ;



	void Start () 
	{
		//testQ = transform.rotation;
	}
	

	void Update () 
	{
		//Vector3 newRot = Vector3.RotateTowards ( cube.transform.rotation.eulerAngles, head.transform.position, 2.0f, 0.0f  ); 
		//cube.transform.rotation = Quaternion.LookRotation (newRot);

		//Quaternion newROt = Quaternion.RotateTowards (testQ, head.transform.rotation, 1.0f); 
		//transform.rotation = newROt;
		// 	Vector3 relativePos = head.transform.position - transform.position;
		//  	Quaternion rotation = Quaternion.LookRotation(relativePos);
		//  Quaternion rotation2 = Quaternion.Euler (rotation.x, testQ.y, rotation.z); 
		//  transform.rotation = rotation2;

		//  Quaternion q = Quaternion.LookRotation(head.transform.position - transform.position);
		//  q.y = 0.0f;
 		// transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 1000.0f * Time.deltaTime);

		 	Vector3 lookDir = head.transform.position - transform.position;
 			lookDir.x = 0; // keep only the horizontal direction
 			transform.rotation = Quaternion.LookRotation(lookDir);
	}
}
