using UnityEngine;
using System.Collections;

public class LocationTextureCheckerHands : MonoBehaviour 
{
	//public Texture[] textures = new Texture[8];


	public Texture blank;
	public Texture white;
	public Texture red;
	public Texture green;
	public Texture yellow;

	//public int id;

	void Start () 
	{
		GetComponent<Renderer> ().material.mainTexture 	= blank;
		//GetComponent<Renderer> ().material.color 		= Color.yellow;
	}

	void Update () 
	{
		//Hand Color Manager---------------------------------------------------------------------//
		//Set Hands to White if they need to be used for stim input
		if (LocationManager.TimeToSense == true) 
		{
			GetComponent<Renderer> ().material.mainTexture = yellow;
		} 
		else if (LocationManager.SelectedColorId == 0) 
		{
			GetComponent<Renderer> ().material.mainTexture = blank;
		} 
		else if (LocationSubmitButton.IsSubmitted == 1) 
		{
			if (LocationManager.SelectedColorId == 1) 
			{
				//If the input is correct
				GetComponent<Renderer> ().material.mainTexture = green;
			}
			if (LocationManager.SelectedColorId == 2) 
			{
				//If the input is wrong
				GetComponent<Renderer> ().material.mainTexture = red;
			}
		}
	}
}

