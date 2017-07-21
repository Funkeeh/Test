using UnityEngine;
using System.Collections;

public class CatapultChangeMass : MonoBehaviour 
{
	public float setMass	= 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown ("space")) 
		{
			GetComponent<Rigidbody> ().mass = setMass;
		}
	}
}
