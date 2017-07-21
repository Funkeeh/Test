using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour 
{
	static public int stage=1, ButtonPressId=0, LastButtonPressed=0, Score=0, Round=1;
	public GameObject StimulationIndicator;
	public static bool Success=false, SendStimulation=false, TimeToSense=true, TimeToSubmit=false;
	//TimeToSense defines whether the game is in the sensing or input (apply stimulation) stage

	void Start () 
	{
		StimulationIndicator.GetComponent<Renderer> ().material.color = new Color (0.5f, 0.5f, 0.5f, 1);
		SendStimulation = false;
		int maxRange = SubmitButton.amountOfButtons + 1;
		ButtonPressId = Random.Range (1, maxRange);
	}
	
	void Update () 
	{
		if (SendStimulation == true) 
		{
			StimulateIndicator ();
		}
		else 
		{
			DoNotStimulateIndicator ();
		}
	}

	void StimulateIndicator()
	{
		StimulationIndicator.GetComponent<Renderer> ().material.color = new Color (1, 0, 0, 1);
	}

	void DoNotStimulateIndicator()
	{
		StimulationIndicator.GetComponent<Renderer> ().material.color = new Color (0.5f, 0.5f, 0.5f, 1);
	}
}