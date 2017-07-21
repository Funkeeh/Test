using UnityEngine;
using System.Collections;

//Checks if user is within range of the buttons
//Makes sure the user can only press 1 button at once
public class FrequencyGameBigCollider : MonoBehaviour 
{
	public static bool thereCanBeOnlyOneAlsoHere = true;

	void OnTriggerEnter()
	{

	}

	void OnTriggerExit()
	{
		thereCanBeOnlyOneAlsoHere = true;
	}
}