using UnityEngine;
using System.Collections;

public class LocationWhatWasPressed : MonoBehaviour 
{
	public static int lastPressed 		= 0;
	public static int secondLastPressed	= 0;
	public static int thirdLastPressed	= 0;

	public static int resetValue 			= 0;			//Stores button id value to reset if pressed again to de-activate
	public static bool maxButtonsPressed 	= false;		//Checks if max buttons are pressed, based on level
	public static int loopOnce 				= 0;			//Only loop once. Reser in LocationButtonAnimator

	void Start () 
	{
		//Nothing
	}

	void Update () 
	{

		//Level 3-----------------------------------------------------------------------------------------------//
		if (LocationManager.currentLevel == 3) 
		{
			//Checking if user has pressed more than 3 buttons
			if (lastPressed != 0 && secondLastPressed != 0 && thirdLastPressed != 0) 
			{
				maxButtonsPressed = true;
			}

			for (int i = loopOnce; i < 1; i++) 
			{
				if (maxButtonsPressed == false && lastPressed != LocationManager.LastButtonPressed && secondLastPressed != LocationManager.LastButtonPressed && thirdLastPressed != LocationManager.LastButtonPressed) 
				{
					thirdLastPressed = secondLastPressed;
					secondLastPressed = lastPressed;
					lastPressed = LocationManager.LastButtonPressed;

					resetValue = 0;
				} 
				else if (maxButtonsPressed == false && lastPressed != LocationManager.LastButtonPressed && secondLastPressed == 0) 
				{
					secondLastPressed = lastPressed;
					lastPressed = LocationManager.LastButtonPressed;

					resetValue = 0;
				} 
				else if (maxButtonsPressed == false && lastPressed == 0) 
				{
					lastPressed = LocationManager.LastButtonPressed;

					resetValue = 0;
				}
				else
				{
					if( lastPressed == LocationManager.LastButtonPressed )
					{
						lastPressed 		= secondLastPressed;
						secondLastPressed 	= thirdLastPressed;
						thirdLastPressed 	= 0;
						resetValue = LocationManager.LastButtonPressed;

						maxButtonsPressed = false;
					}
					else if( secondLastPressed == LocationManager.LastButtonPressed )
					{
						secondLastPressed 	= thirdLastPressed;
						thirdLastPressed 	= 0;
						resetValue = LocationManager.LastButtonPressed;

						maxButtonsPressed = false;
					}
					else if( thirdLastPressed == LocationManager.LastButtonPressed )
					{
						thirdLastPressed 	= 0;
						resetValue = LocationManager.LastButtonPressed;

						maxButtonsPressed = false;
					}
				}
			}
			loopOnce = 1; //Only do all this once

			Debug.Log ("Order: " + lastPressed + ", " + secondLastPressed + ", " + thirdLastPressed + "  ==> Reset Value: " + resetValue);
			Debug.Log ("Max buttons pressed " + maxButtonsPressed);
		}

		//Level 2-----------------------------------------------------------------------------------------------//
		if (LocationManager.currentLevel == 2) 
		{
			//Checking if user has pressed more than 3 buttons
			if (lastPressed != 0 && secondLastPressed != 0) 
			{
				maxButtonsPressed = true;
			}

			for (int i = loopOnce; i < 1; i++) 
			{
				if (maxButtonsPressed == false && lastPressed != LocationManager.LastButtonPressed && secondLastPressed != LocationManager.LastButtonPressed) 
				{
					secondLastPressed = lastPressed;
					lastPressed = LocationManager.LastButtonPressed;

					resetValue = 0;
				} 
				else if (maxButtonsPressed == false && lastPressed != LocationManager.LastButtonPressed && secondLastPressed == 0) 
				{
					secondLastPressed = lastPressed;
					lastPressed = LocationManager.LastButtonPressed;

					resetValue = 0;
				} 
				else if (maxButtonsPressed == false && lastPressed == 0) 
				{
					lastPressed = LocationManager.LastButtonPressed;

					resetValue = 0;
				}
				else
				{
					if( lastPressed == LocationManager.LastButtonPressed )
					{
						lastPressed 		= secondLastPressed;
						secondLastPressed 	= thirdLastPressed;
						thirdLastPressed 	= 0;
						resetValue = LocationManager.LastButtonPressed;

						maxButtonsPressed = false;
					}
					else if( secondLastPressed == LocationManager.LastButtonPressed )
					{
						secondLastPressed 	= thirdLastPressed;
						thirdLastPressed 	= 0;
						resetValue = LocationManager.LastButtonPressed;

						maxButtonsPressed = false;
					}
				}
			}
			loopOnce = 1; //Only do all this once

			Debug.Log ("Order: " + lastPressed + ", " + secondLastPressed + "  ==> Reset Value: " + resetValue);
		}

		//Level 1-----------------------------------------------------------------------------------------------//
		if (LocationManager.currentLevel == 1) 
		{
			lastPressed = LocationManager.LastButtonPressed;

			Debug.Log ("Order: " + lastPressed + ", " + secondLastPressed + ", " + thirdLastPressed + "  ==> Reset Value: " + resetValue);
			//Debug.Log ("Max buttons pressed " + maxButtonsPressed);
		}
	}
}
