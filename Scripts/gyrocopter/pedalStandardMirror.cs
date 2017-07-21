using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pedalStandardMirror : MonoBehaviour 
{
	[Tooltip("bottomPos01 (front), 01.5 (middle) or 02 (back) in left Pedal GO")]
	public GameObject pedalLeft;
	[Tooltip("bottomPos01 (front), 01.5 (middle) or 02 (back) in right Pedal GO")]
	public GameObject pedalRight;

	void Update () 
	{
		//Set Scale of Middle Axis
		float dist = Vector3.Distance (pedalLeft.transform.position, pedalRight.transform.position); 
		Vector3 newSize =  new Vector3(transform.localScale.x, dist / 2.0f, transform.localScale.z);
		transform.localScale = newSize;

		//Set Position 
		float xPos = pedalLeft.transform.position.x - (dist / 2.0f);
		transform.position = new Vector3 (xPos, pedalLeft.transform.position.y, pedalLeft.transform.position.z);
	}
}