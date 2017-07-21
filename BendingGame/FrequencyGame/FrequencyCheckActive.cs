using UnityEngine;
using System.Collections;

public class FrequencyCheckActive : MonoBehaviour 
{
	public GameObject game;
	public GameObject training;
	public GameObject ui;

	public void ActivateOnlyNeeded () 
	{
		if (game.activeSelf == true) 
		{
			//Do Nothing
		}
		else if (training.activeSelf == true) 
		{
			ui.SetActive (false);
		}
		else
		{
			game.SetActive (true);
			ui.SetActive (true);
		}
	}
}
