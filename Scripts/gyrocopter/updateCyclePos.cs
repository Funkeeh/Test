using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class updateCyclePos : MonoBehaviour 
{
	//public 	Transform cyclePos;
	private Vector3 initial 		= Vector3.zero;
	private float	verticalOffset 	= 0.0f;
	private float horizontalOffset 	= 0.0f;

	void Start () 
	{
		initial = GetComponent<Transform> ().localPosition;
	}

	void FixedUpdate () 
	{
		verticalOffset 		= (5 - buttonColorManager.forButtonColorVert) / 10.0f;
		horizontalOffset  	= ((5 - buttonColorManager.forButtonColorHori) * -1.0f) / 10.0f;
		//Debug.Log ("Z " + horizontal + " Y " + vertical);

		Vector3 newPosition = new Vector3 (initial.x, initial.y + verticalOffset, initial.z + horizontalOffset);
		//Debug.Log (newPosition);
		GetComponent<Transform> ().localPosition = newPosition;
	}
}