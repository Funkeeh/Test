using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class UIsave : MonoBehaviour {

	public Text[] buttons = new Text[20];
	public static Text[] buttonscopy = new Text[20];

	public static void GetNames()
	{

		String[] hold = (String[])UiController.Savefiles.ToArray(typeof(String));
        //string[] saves = SaveFiles.saveFileNames

		for (int i = 0; i <= buttonscopy.Length; i++)
		{
			buttonscopy[i].text = hold[i];
            
            //buttons[i].text = buttonscopy[i].text;
		}
        
	}

	void Start () {
		//GetNames();
	}
	

	void Update () {
        //GetNames();
        //buttonscopy = buttons;
        for (int i = 0; i <= buttonscopy.Length; i++)
        {
            buttons[i].text = buttonscopy[i].text;
        }


	}
}
