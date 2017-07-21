using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ColorActivity : MonoBehaviour {
	//Reference to button to access its components
	private Button theButton;

	//this get the Transitions of the Button as its pressed
	private ColorBlock theColor;

	// Use this for initialization
	void Start () {
		theButton = GetComponent<Button>();
		theColor = GetComponent<Button>().colors;

	}


	/// <summary>
	/// Example
	/// just add this to your Button component On Click()
	/// </summary>
	public void ButtonTransitionColors()
	{

		theColor.normalColor = Color.green;
		theColor.highlightedColor = Color.green;

		theButton.colors = theColor;
		print("Cliked");
	}

	public void ButtonReset()
	{
		theColor.normalColor = Color.white;
		theColor.highlightedColor = Color.gray;	

		theButton.colors = theColor;
	}
}