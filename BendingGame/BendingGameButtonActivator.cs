using UnityEngine;
using System.Collections;

public class BendingGameButtonActivator : MonoBehaviour 
{
	public GameObject	animation;
	public GameObject 	animationMirror;
	public GameObject	button;
	public GameObject 	buttonMirror;

	public static int TextureIdBG = 0;

	public 	AudioSource completed;
	public 	AudioSource	error;
		
	public static bool 	pressButton 			= false;		//Tells bendingGame script if user is pressing button (triggerEnter = true, triggerExit = false)
	public static bool 	thereCanBeOnlyOnePress 	= true;			//Make sure the user does not press the buttons multiple times by accident


	void OnTriggerEnter () 
	{
		//animation.GetComponent<Animation> ().Play ("PressDown");

		if(BendingGame.gameOver == false && thereCanBeOnlyOnePress == true)
		{
			pressButton 			= true;		
			thereCanBeOnlyOnePress 	= false;
		}
	}

	void OnTriggerExit ()
	{
		//animation.GetComponent<Animation> ().Play ("PressUp");
		if (BendingGame.gameOver == false) 
		{
			TextureIdBG = 0;
			pressButton = false;
		}
	}
}