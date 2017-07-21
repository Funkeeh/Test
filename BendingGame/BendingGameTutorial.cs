using UnityEngine;
using System.Collections;

public class BendingGameTutorial : MonoBehaviour
{
	private int tutorialCounter = 0;

	//Cylinder Game Objects
	public GameObject goLeft;			public GameObject goMid;		public GameObject goRight;
	//Game Buttons
	public GameObject goButtons;

	//Tutorial Level Screens
	public GameObject tut01;			public GameObject tut02;		public GameObject tut03;	
	public GameObject tut04;			public GameObject tut05;

	//Game Level Screen
	public GameObject gameScreen;



	void Start () 
	{
		tut01.SetActive (true);
		goLeft.SetActive (false);
		goMid.SetActive (false);
		goRight.SetActive (false);
		goButtons.SetActive (false);
		gameScreen.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		//GRAB THE NUNCHUCK

		//01: CHECK IF USER GRABS NUNCHUCK FOR FIRST TIME
		if (BendingGameCylinderHandler.grabCylinderWorld == true && tutorialCounter == 0) 
		{
			//RELEASE THE NUNCHUCK
			tut01.SetActive (false);
			tut02.SetActive (true);
			tutorialCounter = 1;
		}

		//02: CHECK IF USER RELEASE NUNCHUCK FOR FIRST TIME
		else if (BendingGameCylinderHandler.grabCylinderWorld == false && tutorialCounter == 1) 
		{
			//GRAB THE NUNCHUCK AGAIN
			tut01.SetActive (true);
			tut02.SetActive (false);
			tutorialCounter = 2;
		}

		//03: CHECK IF USER GRABS THE NUNCHUCK AGAIN
		else if (BendingGameCylinderHandler.grabCylinderWorld == true && tutorialCounter == 2) 
		{
			//PLACE NUNCHUCK INSIDE GOAL CYLINDER
			goLeft.SetActive (true);
			goMid.SetActive (true);
			goRight.SetActive (true);
			tut01.SetActive (false);
			tut03.SetActive (true);
			tutorialCounter = 3;
		}

		//04: CHECK IF USER PUTS THE NUNCHUCK INSIDE THE GOAL
		else if (BendingGameTransparency.enableRend == true && tutorialCounter == 3) 
		{
			goButtons.SetActive (true);
			tut03.SetActive (false);
			tut04.SetActive (true);
			tutorialCounter = 4;
		}

		//05: CHECK IF USER PRESSES BUTTON
		else if (BendingGameTransparency.enableRend == true && tutorialCounter == 4) 
		{
			//GOOD JOB!
			tut04.SetActive (false);
			tut05.SetActive (true);
			tutorialCounter = 5;
		}
	}
}
