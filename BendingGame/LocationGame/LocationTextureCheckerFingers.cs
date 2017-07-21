using UnityEngine;
using System.Collections;

public class LocationTextureCheckerFingers : MonoBehaviour 
{	
	//Ones
	public Material index;	
	public Material middle;
	public Material ring;
	public Material pinky;
	public Material thumb;
	public Material palm;

	//Twos
	public Material[] twoFingers = new Material[15];

	//Others
	public Material none;
	public Material timeToSense;
	public Material currentlyPressing;
	public Material correct;
	public Material wrong;

	void Awake () 
	{
		//Nothing
	}

	void Update () 
	{
		if (LocationManager.gameOver == true) 
		{
			GetComponent<Renderer> ().material = none;
		}
		else if (LocationManager.TimeToSense == true) 
		{
			if (LocationStimulationHands.currentlyPressing == true) 
			{
				GetComponent<Renderer> ().material = currentlyPressing;
			} 
			else 
			{
				GetComponent<Renderer> ().material = timeToSense;
			}
		}
		else if (LocationManager.SelectedColorId == 0) 
		{
			//Level 03
			if (LocationManager.currentLevel == 3) 
			{
				if (LocationWhatWasPressed.lastPressed == 1 || LocationWhatWasPressed.secondLastPressed == 1 || LocationWhatWasPressed.thirdLastPressed == 1) 
				{
					GetComponent<Renderer> ().material = thumb;
				} 
				else if (LocationWhatWasPressed.lastPressed == 2 || LocationWhatWasPressed.secondLastPressed == 2 || LocationWhatWasPressed.thirdLastPressed == 2) 
				{
					GetComponent<Renderer> ().material = index;
				} 
				else if (LocationWhatWasPressed.lastPressed == 3 || LocationWhatWasPressed.secondLastPressed == 3 || LocationWhatWasPressed.thirdLastPressed == 3) 
				{
					GetComponent<Renderer> ().material = middle;
				} 
				else if (LocationWhatWasPressed.lastPressed == 4 || LocationWhatWasPressed.secondLastPressed == 4 || LocationWhatWasPressed.thirdLastPressed == 4) 
				{
					GetComponent<Renderer> ().material = ring;
				} 
				else if (LocationWhatWasPressed.lastPressed == 5 || LocationWhatWasPressed.secondLastPressed == 5 || LocationWhatWasPressed.thirdLastPressed == 5) 
				{
					GetComponent<Renderer> ().material = pinky;
				} 
				else if (LocationWhatWasPressed.lastPressed == 6 || LocationWhatWasPressed.secondLastPressed == 6 || LocationWhatWasPressed.thirdLastPressed == 6) 
				{
					GetComponent<Renderer> ().material = palm;
				} 
				else 
				{
					GetComponent<Renderer> ().material = none;
				}
			}
			//Level 02
			if (LocationManager.currentLevel == 2) 
			{
				//Two Textures
				if(LocationWhatWasPressed.lastPressed == 1 && LocationWhatWasPressed.secondLastPressed == 2 || LocationWhatWasPressed.lastPressed == 2 && LocationWhatWasPressed.secondLastPressed == 1)
				{
					GetComponent<Renderer> ().material = twoFingers [0];
				}
				else if(LocationWhatWasPressed.lastPressed == 1 && LocationWhatWasPressed.secondLastPressed == 3 || LocationWhatWasPressed.lastPressed == 3 && LocationWhatWasPressed.secondLastPressed == 1)
				{
					GetComponent<Renderer> ().material = twoFingers [1];
				}
				else if(LocationWhatWasPressed.lastPressed == 1 && LocationWhatWasPressed.secondLastPressed == 4 || LocationWhatWasPressed.lastPressed == 4 && LocationWhatWasPressed.secondLastPressed == 1)
				{
					GetComponent<Renderer> ().material = twoFingers [2];
				}
				else if(LocationWhatWasPressed.lastPressed == 1 && LocationWhatWasPressed.secondLastPressed == 5 || LocationWhatWasPressed.lastPressed == 5 && LocationWhatWasPressed.secondLastPressed == 1)
				{
					GetComponent<Renderer> ().material = twoFingers [3];
				}
				else if(LocationWhatWasPressed.lastPressed == 1 && LocationWhatWasPressed.secondLastPressed == 6 || LocationWhatWasPressed.lastPressed == 6 && LocationWhatWasPressed.secondLastPressed == 1)
				{
					GetComponent<Renderer> ().material = twoFingers [4];
				}
				else if(LocationWhatWasPressed.lastPressed == 2 && LocationWhatWasPressed.secondLastPressed == 3 || LocationWhatWasPressed.lastPressed == 3 && LocationWhatWasPressed.secondLastPressed == 2)
				{
					GetComponent<Renderer> ().material = twoFingers [5];
				}
				else if(LocationWhatWasPressed.lastPressed == 2 && LocationWhatWasPressed.secondLastPressed == 4 || LocationWhatWasPressed.lastPressed == 4 && LocationWhatWasPressed.secondLastPressed == 2)
				{
					GetComponent<Renderer> ().material = twoFingers [6];
				}
				else if(LocationWhatWasPressed.lastPressed == 2 && LocationWhatWasPressed.secondLastPressed == 5 || LocationWhatWasPressed.lastPressed == 5 && LocationWhatWasPressed.secondLastPressed == 2)
				{
					GetComponent<Renderer> ().material = twoFingers [7];
				}
				else if(LocationWhatWasPressed.lastPressed == 2 && LocationWhatWasPressed.secondLastPressed == 6 || LocationWhatWasPressed.lastPressed == 6 && LocationWhatWasPressed.secondLastPressed == 2)
				{
					GetComponent<Renderer> ().material = twoFingers [8];
				}
				else if(LocationWhatWasPressed.lastPressed == 3 && LocationWhatWasPressed.secondLastPressed == 4 || LocationWhatWasPressed.lastPressed == 4 && LocationWhatWasPressed.secondLastPressed == 3)
				{
					GetComponent<Renderer> ().material = twoFingers [9];
				}
				else if(LocationWhatWasPressed.lastPressed == 3 && LocationWhatWasPressed.secondLastPressed == 5 || LocationWhatWasPressed.lastPressed == 5 && LocationWhatWasPressed.secondLastPressed == 3)
				{
					GetComponent<Renderer> ().material = twoFingers [10];
				}
				else if(LocationWhatWasPressed.lastPressed == 3 && LocationWhatWasPressed.secondLastPressed == 6 || LocationWhatWasPressed.lastPressed == 6 && LocationWhatWasPressed.secondLastPressed == 3)
				{
					GetComponent<Renderer> ().material = twoFingers [11];
				}
				else if(LocationWhatWasPressed.lastPressed == 4 && LocationWhatWasPressed.secondLastPressed == 5 || LocationWhatWasPressed.lastPressed == 5 && LocationWhatWasPressed.secondLastPressed == 4)
				{
					GetComponent<Renderer> ().material = twoFingers [12];
				}
				else if(LocationWhatWasPressed.lastPressed == 4 && LocationWhatWasPressed.secondLastPressed == 6 || LocationWhatWasPressed.lastPressed == 6 && LocationWhatWasPressed.secondLastPressed == 4)
				{
					GetComponent<Renderer> ().material = twoFingers [13];
				}
				else if(LocationWhatWasPressed.lastPressed == 5 && LocationWhatWasPressed.secondLastPressed == 6 || LocationWhatWasPressed.lastPressed == 6 && LocationWhatWasPressed.secondLastPressed == 5)
				{
					GetComponent<Renderer> ().material = twoFingers [14];
				}
				else
				{
					if (LocationWhatWasPressed.lastPressed == 1 || LocationWhatWasPressed.secondLastPressed == 1) 
					{
						GetComponent<Renderer> ().material = thumb;
					} 
					else if (LocationWhatWasPressed.lastPressed == 2 || LocationWhatWasPressed.secondLastPressed == 2) 
					{
						GetComponent<Renderer> ().material = index;
					} 
					else if (LocationWhatWasPressed.lastPressed == 3 || LocationWhatWasPressed.secondLastPressed == 3) 
					{
						GetComponent<Renderer> ().material = middle;					
					} 
					else if (LocationWhatWasPressed.lastPressed == 4 || LocationWhatWasPressed.secondLastPressed == 4) 
					{
						GetComponent<Renderer> ().material = ring;
					} 
					else if (LocationWhatWasPressed.lastPressed == 5 || LocationWhatWasPressed.secondLastPressed == 5) 
					{
						GetComponent<Renderer> ().material = pinky;
					} 
					else if (LocationWhatWasPressed.lastPressed == 6 || LocationWhatWasPressed.secondLastPressed == 6) 
					{
						GetComponent<Renderer> ().material = palm;
					} 
					else 
					{
						GetComponent<Renderer> ().material = none;
					}
				}
			}
			//Level 01
			if (LocationManager.currentLevel == 1) 
			{
				//Debug.Log ("im here level 1 and last button pressed is " + LocationManager.LastButtonPressed);
				if (LocationManager.LastButtonPressed == 0) 
				{
					GetComponent<Renderer> ().material = none;
				} 
				else if (LocationManager.LastButtonPressed == 1) 
				{
					GetComponent<Renderer> ().material = thumb;
				} 
				else if (LocationManager.LastButtonPressed == 2) 
				{
					GetComponent<Renderer> ().material = index;
				} 
				else if (LocationManager.LastButtonPressed == 3) 
				{
					GetComponent<Renderer> ().material = middle;
				} 
				else if (LocationManager.LastButtonPressed == 4) 
				{
					GetComponent<Renderer> ().material = ring;
				} 
				else if (LocationManager.LastButtonPressed == 5) 
				{
					GetComponent<Renderer> ().material = pinky;
				} 
				else if (LocationManager.LastButtonPressed == 6) 
				{
					GetComponent<Renderer> ().material = palm;
				} 
				else 
				{
					GetComponent<Renderer> ().material = none;
				}
			}
		}

		else if (LocationManager.SelectedColorId == 1) 
		{
			GetComponent<Renderer> ().material.color = Color.green;
		}
		else if (LocationManager.SelectedColorId == 2) 
		{
			GetComponent<Renderer> ().material.color = Color.red;
		}
		else if (LocationSubmitButton.IsSubmitted == 1)
		{
			if (LocationManager.SelectedColorId == 1) 
			{
				//If the input is correct
				GetComponent<Renderer> ().material = correct;
			}
			if (LocationManager.SelectedColorId == 2) 
			{
				//If the input is wrong
				GetComponent<Renderer> ().material = wrong;
			}
		}
	}
}
