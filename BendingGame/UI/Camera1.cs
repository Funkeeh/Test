using UnityEngine;
using System.Collections;

public class Camera1 : MonoBehaviour 
{
	// Use this for initialization
	void Start()
	{
		Debug.Log("displays connected: " + Display.displays.Length);
		// Display.displays[0] is the primary, default display and is always ON.

		Display.displays [0].Activate ();
		Display.displays [1].Activate ();
	}

	void Update ()
	{
		if (Input.GetKey ("escape")) 
		{
			Application.Quit ();
		}
	}
}