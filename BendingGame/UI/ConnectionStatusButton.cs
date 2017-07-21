using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ConnectionStatusButton : MonoBehaviour 
{
	public Text ConnectionStatus;
	public static bool clickedOnce = false;

	private Button button;
	//this get the Transitions of the Button as its pressed
	private ColorBlock theColor;
	private double timer = 0.0;

	void Start ()
	{
		button 		= GetComponent<Button>();
		theColor 	= GetComponent<Button>().colors;
	}

	void Update () 
	{
		if (Client.serverStatus == true) 
		{
			theColor.normalColor 		= Color.green;
			theColor.highlightedColor 	= Color.green;
			button.colors = theColor;

			ConnectionStatus.text = "Connected!";
		}
		else
		{
			theColor.normalColor = Color.white;
			button.colors = theColor;

			ConnectionStatus.text = "Connect!";
		}
	}
}
