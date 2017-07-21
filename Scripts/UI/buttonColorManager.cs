using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonColorManager : MonoBehaviour 
{
	//FOR UI
	public static int	forButtonColor			= 55;				//Set active button in swingPedal pos in UI
	public static int	forButtonColorVert 		= 0;
	public static int	forButtonColorHori	 	= 0;

	public Color		alignedColor;
	public Color		pressedColor;
	public static Color	setAlignedClr;
	public static Color setPressedClr;

	void Awake () 
	{
		setAlignedClr = alignedColor;
		setPressedClr = pressedColor;
	}

	void Update () 
	{
		forButtonColorVert = int.Parse (forButtonColor.ToString () [0].ToString ());
		forButtonColorHori = int.Parse (forButtonColor.ToString () [1].ToString ());
	}
}