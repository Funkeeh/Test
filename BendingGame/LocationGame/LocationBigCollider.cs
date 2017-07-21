using UnityEngine;
using System.Collections;

//Checks if user is within range of the buttons
//Makes sure the user can only press 1 button at once
public class LocationBigCollider : MonoBehaviour 
{
	public static bool thereCanBeOnlyLocation = true;

	void OnTriggerEnter()
	{

	}

	void OnTriggerExit()
	{
		thereCanBeOnlyLocation = true;
	}
}