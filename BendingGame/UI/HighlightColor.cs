using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HighlightColor : MonoBehaviour {

	// Use this for initialization
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

		theColor.normalColor = new Color(0f,0f,0.5f,0.5f);
		theColor.highlightedColor = new Color(0f,0f,0.5f,0.5f);

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