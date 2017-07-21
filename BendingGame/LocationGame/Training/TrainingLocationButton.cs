using UnityEngine;
using System.Collections;

public class TrainingLocationButton : MonoBehaviour 
{
	public int id;
	public GameObject number;

	private Color ButtonGrayColor		= new Color ( 0.5f, 0.5f, 0.5f, 1 );
	private Color ButtonBrightColor 	= new Color ( 0.9f, 0.9f, 0.9f, 0.9f );
	private Color ButtonPressColor		= new Color ( 1, 1, 0.15f, 1 );

	private Color nrInitial; 
	private Color nrGrayColor			= new Color ( 0.5f, 0.5f, 0.5f, 1.0f );

	void Start () 
	{
		//nrInitial = number.GetComponent<Renderer> ().material.color;
	}

	void Update () 
	{

		if ( TrainingLocationManager.visualiseButtons == true && TrainingLocationManager.lastStimuValue == id || TrainingLocationManager.visualiseButtons == true && TrainingLocationManager.secondStimValue == id) 
		{
			GetComponent<Renderer> ().material.color = LocationTextures.ManageTexture ( 2 ); //id
			number.GetComponent<Renderer> ().material.color = ButtonBrightColor;

			Debug.Log ("here");
		}
		else if ( TrainingManager.visualiseButtons == true ) 
		{
			GetComponent<Renderer> ().material.color 		= ButtonBrightColor;
			number.GetComponent<Renderer> ().material.color = ButtonBrightColor;
		}
		else
		{
			GetComponent<Renderer> ().material.color 		= ButtonGrayColor;
			number.GetComponent<Renderer> ().material.color = nrGrayColor;
		}
	}
}
