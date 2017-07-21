using UnityEngine;
using System.Collections;

public class LocationNumberChecker : MonoBehaviour 
{

	private Color ButtonInitialColor;
	public  Color ButtonGrayColor			= new Color (0.5f, 0.5f, 0.5f, 1);
	public 	int   colorID	= 0;			//ID 1 = Stimulator, ID2 = Input Buttons, ID3 = Submitbutton

	void Start () 
	{
		ButtonInitialColor = GetComponent <Renderer> ().material.color;
	}

	void Update () 
	{
		if (SubmitButton.gameOver == false) 
		{
			switch (colorID) 
			{
			case 1:	//ID 1 = Stimulator
				if (LocationManager.TimeToSense == false) 
				{
					GetComponent <Renderer> ().material.color = ButtonGrayColor;
				} 
				else 
				{
					GetComponent <Renderer> ().material.color = ButtonInitialColor;
				}
				break;
			case 2: //ID 2 = Input Buttons
				if (LocationManager.TimeToSense == true) 
				{
					GetComponent <Renderer> ().material.color = ButtonGrayColor;
				} else 
				{
					GetComponent <Renderer> ().material.color = ButtonInitialColor;
				}
				break;
			case 3: //ID 3 = Submitbutton
				if (LocationManager.TimeToSense == true) 
				{
					GetComponent <Renderer> ().material.color = ButtonGrayColor;
				} else 
				{
					GetComponent <Renderer> ().material.color = ButtonInitialColor;
				}
				break;
			}
		} 
		else 
		{
			GetComponent <Renderer> ().material.color = ButtonGrayColor;
		}

	}
}
