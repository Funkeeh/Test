using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonColor : MonoBehaviour 
{
	public int myNumber = 55;
	private Button mybutton;
	private ColorBlock cb;
	//private Color myColor 	= Color.yellow;
	//private Color myColor2 	= Color.red;

	//FOR CYCLE POS OFFSET POSITIONING
	public 	Transform cyclePos;
	private Transform initial;
	//private float	vertical 	= 0.0f;
	//private float horizontal 	= 0.0f;

	void Start () 
	{
		mybutton = GetComponent<Button> ();
		cb = mybutton.colors;

		initial = cyclePos.GetComponent<Transform> ();
	}

	void Update () 
	{
		int first 	= int.Parse (myNumber.ToString () [0].ToString ());
		int second 	= int.Parse (myNumber.ToString () [1].ToString ());

		if (myNumber == buttonColorManager.forButtonColor) 
		{
			ColorBlock newClr 	= cb;
			newClr.normalColor 	= buttonColorManager.setPressedClr;
			mybutton.colors 	= newClr;
		}
		else if (first == buttonColorManager.forButtonColorVert || second == buttonColorManager.forButtonColorHori) 
		{
			ColorBlock newClr 	= cb;
			newClr.normalColor 	= buttonColorManager.setAlignedClr;
			mybutton.colors 	= newClr;

		}
		else
		{
			mybutton.colors = cb;
		}
	}

	public void setWorldNumberToMine ()
	{
		buttonColorManager.forButtonColor = myNumber;
	}
}