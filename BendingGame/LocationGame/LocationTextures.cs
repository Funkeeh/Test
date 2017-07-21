using UnityEngine;
using System.Collections;

//Use in other scripts to attach correct texture color
public class LocationTextures : MonoBehaviour 
{
	public static Color[] colors 		= new Color[6];
	public static Color[] colorsPressed = new Color[6];

	//Color Scheme Setup
	public Color c01;
	public Color c02;
	public Color c03;
	public Color c04;
	public Color c05;
	public Color c06;

	//Color Pressed Scheme Setup
	public Color p01;
	public Color p02;
	public Color p03;
	public Color p04;
	public Color p05;
	public Color p06;

	void Awake () 
	{
		colors [0] = c01;
		colors [1] = c02;
		colors [2] = c03;
		colors [3] = c04;
		colors [4] = c05;
		colors [5] = c06;

		colorsPressed [0] = p01;
		colorsPressed [1] = p02;
		colorsPressed [2] = p03;
		colorsPressed [3] = p04;
		colorsPressed [4] = p05;
		colorsPressed [5] = p06;
	}
		
	public static Color ManageTexture (int nr)
	{
		int getClr = nr - 1;
		return colors [getClr];
	}

	public static Color ManageTexturePressing (int nr)
	{
		int getClr = nr - 1;
		return colorsPressed [getClr];
	}
}
