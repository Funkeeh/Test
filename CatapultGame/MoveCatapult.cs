using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoveCatapult : MonoBehaviour 
{
	public float movespeed = 5.0f;
	public float backspeed = 3.0f;
	public float turnspeed = 50.0f;

	void Update ()
	{
		if(Input.GetButton("xBox_AddTorque"))
			transform.Translate(Vector3.forward  * movespeed * Time.deltaTime);

		if(Input.GetButton("xBox_Break"))
			transform.Translate(-Vector3.forward * backspeed * Time.deltaTime);

		if(Input.GetAxis ("Horizontal") < 0.0f)
			transform.Rotate(Vector3.up, -turnspeed * Time.deltaTime);

		if(Input.GetAxis ("Horizontal") > 0.0f)
			transform.Rotate(Vector3.up, turnspeed * Time.deltaTime);

		Debug.Log (Input.GetAxis ("Horizontal"));
	}
}

