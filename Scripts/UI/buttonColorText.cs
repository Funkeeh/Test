using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonColorText : MonoBehaviour 
{
	[Tooltip("True: Horizontal, False: Vertical")]
	public bool	version 	= false;

	private Text text;
	private int adjustValue	= 0;

	void Start () 
	{
		text = GetComponent<Text> ();
	}

	void Update () 
	{
		if (version == true) 
		{
			adjustValue = buttonColorManager.forButtonColorHori - 5;
			text.text = adjustValue + ".0";
		}
		else
		{
			adjustValue = buttonColorManager.forButtonColorVert - 5;
			text.text = adjustValue + ".0";
		}
	}
}