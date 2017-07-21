using UnityEngine;
using System.Collections;

public class TrainingButtonAnimator : MonoBehaviour 
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
		nrInitial = number.GetComponent<Renderer> ().material.color;
	}

	void Update () 
	{
		if ( TrainingManager.lastStimulatedValue == id && TrainingManager.visualiseButtons == true ) 
		{
			GetComponent<Renderer> ().material.color 		= ButtonPressColor;
			number.GetComponent<Renderer> ().material.color = nrInitial;
		}
		else if ( TrainingManager.visualiseButtons == true ) 
		{
			GetComponent<Renderer> ().material.color 		= ButtonBrightColor;
			number.GetComponent<Renderer> ().material.color = nrInitial;
		}
		else
		{
			GetComponent<Renderer> ().material.color 		= ButtonGrayColor;
			number.GetComponent<Renderer> ().material.color = nrGrayColor;
		}
	}
}
