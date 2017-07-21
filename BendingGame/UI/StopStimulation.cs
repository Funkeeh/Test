using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//Stop Stimulation: Should also be applied to a button in the UI
public class StopStimulation : MonoBehaviour 
{
	//Reference to button to access its components
	private Button theButton;

	//this get the Transitions of the Button as its pressed
	private ColorBlock theColor;

	void Start ()
	{
		theButton 	= GetComponent<Button>();
		theColor 	= GetComponent<Button>().colors;
	}

	void Update ()
	{
		if (Client.startstimulation == true) 
		{
			theColor.normalColor 		= Color.green;
			theColor.highlightedColor 	= Color.white;

			theButton.colors = theColor;
		}
		if (Client.startstimulation == false) 
		{
			theColor.normalColor 		= Color.grey;
			theColor.highlightedColor 	= Color.grey;

			theButton.colors = theColor;
		}
	}

	public void StopStim () 
	{
		if (Client.startstimulation == true) 
		{
			Client.startstimulation = false;
		}
	}
}
