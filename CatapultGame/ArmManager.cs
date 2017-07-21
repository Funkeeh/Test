using UnityEngine;
using System.Collections;

public class ArmManager : MonoBehaviour 
{
	//Catapult Arm
	public GameObject 	catapultArm;
	public float	  	addMass 	= 100.0f;
	public float 		removeMass 	= 5.0f;

	//Object to throw
	public GameObject	throwThis;
	public GameObject	placement;

	private bool readyArm = false;
	private bool readyObject = false;

	void Start () 
	{
		//Nothing
	}

	void Update () 
	{
		//Lower Arm
		if (Input.GetButtonDown ("xBox_Shoot") == true) 
		{
			catapultArm.GetComponent<Rigidbody> ().mass = addMass;

			readyArm = true;
		}

		//Add Item
		if (Input.GetButtonDown ("xBox_ChooseItem") == true && readyArm == true) 
		{
			GameObject cube = GameObject.Instantiate (throwThis);
			//cube.AddComponent<Rigidbody>();
			//cube.GetComponent<Rigidbody> ().mass = 5.0f;
			cube.transform.position = placement.transform.position;

			readyObject = true;
		}

		//Shoot
		if (Input.GetButtonDown ("xBox_Shoot") == true && readyArm == true && readyObject == true) 
		{
			catapultArm.GetComponent<Rigidbody> ().mass = removeMass;

			readyArm 	= false;
			readyObject = false;
		}
	}
}
