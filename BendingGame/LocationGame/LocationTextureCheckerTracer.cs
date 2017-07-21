using UnityEngine;
using System.Collections;

public class LocationTextureCheckerTracer : MonoBehaviour 
{
	public int id = 0;
	Color ButtonGrayColor = new Color(0.5f, 0.5f, 0.5f, 1);

	void Update () 
	{
		if (LocationManager.gameOver == false) 
		{
			//LEVEL 3 MODE
			if (LocationManager.currentLevel == 3) 
			{
				if (LocationManager.TimeToSense == true) 
				{
					GetComponent<Renderer> ().material.color = ButtonGrayColor;
				}
				else if (LocationManager.SelectedColorId == 0) 
				{
					if (id == LocationWhatWasPressed.lastPressed) 
					{
						GetComponent<Renderer> ().material.color = LocationTextures.ManageTexture (LocationWhatWasPressed.lastPressed);
					}
					else if (id == LocationWhatWasPressed.secondLastPressed) 
					{
						GetComponent<Renderer> ().material.color = LocationTextures.ManageTexture (LocationWhatWasPressed.secondLastPressed);
					}
					else if (id == LocationWhatWasPressed.thirdLastPressed) 
					{
						GetComponent<Renderer> ().material.color = LocationTextures.ManageTexture (LocationWhatWasPressed.thirdLastPressed);
					}
					else
					{
						GetComponent<Renderer> ().material.color = Color.white;
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
			}

			//LEVEL 2 MODE
			if (LocationManager.currentLevel == 2) 
			{
				if (LocationManager.TimeToSense == true) 
				{
					GetComponent<Renderer> ().material.color = ButtonGrayColor;
				}
				else if (LocationManager.SelectedColorId == 0) 
				{
					if (id == LocationWhatWasPressed.lastPressed) 
					{
						GetComponent<Renderer> ().material.color = LocationTextures.ManageTexture ( 2 ); 	//Lastpressed id if old color should go back
					}
					else if (id == LocationWhatWasPressed.secondLastPressed) 
					{
						GetComponent<Renderer> ().material.color = LocationTextures.ManageTexture ( 2 );	//SeconLastpressed id if old color should go back
					}
					else
					{
						GetComponent<Renderer> ().material.color = Color.white;
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
			}

			//LEVEL 1 MODE
			if (LocationManager.currentLevel == 1) 
			{
				if (LocationManager.TimeToSense == true) 
				{
					GetComponent<Renderer> ().material.color = ButtonGrayColor;
				}
				else if (LocationManager.SelectedColorId == 0) 
				{
					if (id == LocationWhatWasPressed.lastPressed) 
					{
						GetComponent<Renderer> ().material.color = LocationTextures.ManageTexture ( 2 ); //id
					}
					else
					{
						GetComponent<Renderer> ().material.color = Color.white;
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
			}
		}
		else
		{
			GetComponent<Renderer> ().material.color = ButtonGrayColor;
		}
	}
}
