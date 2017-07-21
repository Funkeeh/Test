using UnityEngine;
using System.Collections;

//Checks if user is within range of the buttons
public class BendingGameButtonsCollider : MonoBehaviour 
{

	void OnTriggerEnter()
	{

	}

	void OnTriggerExit()
	{
		BendingGameButtonActivator.thereCanBeOnlyOnePress = true;
	}
}