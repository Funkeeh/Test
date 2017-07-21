//The purpose of this script is to send the collision info to the button (deeper layers)
using UnityEngine;
using System.Collections;

public class SubmitButtonMirror : MonoBehaviour 
{
	public GameObject Button, Animator;
	public static int DoStimulate, TextureId, IsSubmitted, IsSuccess;
	public Color ButtonColor, ButtonPressColor;
	bool TimerStarted=false;
	public static int WinOrLoseTimer=0;
	public static int FrequencyRealScore = 0;
	//ButtonColor is the standard color, ButtonPressColor is the activated one

	void Start () 
	{
		Button.GetComponent<Renderer> ().material.color = ButtonColor;
	}

	void Update () 
	{
		if (Manager.TimeToSense == true) 
		{
			TextureId = 3;
		}
		//Debug.Log (WinOrLoseTimer);
	}

	void OnTriggerEnter () 
	{
		Animator.GetComponent<Animation> ().Play ("PressDown");
		if (Manager.TimeToSense == false && Manager.TimeToSubmit ==true && WinOrLoseTimer==0) 
		{
			IsSubmitted 		= 1;
			StartCoroutine (Wait ());

			if (Manager.LastButtonPressed == Manager.ButtonPressId) 
			{
				Animator.GetComponent<AudioSource> ().Play ();

				TextureId = 1;
				IsSuccess = 1;
			} 
			else 
			{
				TextureId = 2;
				IsSuccess = 0;
			}
		}
	}

	void OnTriggerStay()
	{
		//Nothing
	}

	void OnTriggerExit () 
	{
		Animator.GetComponent<Animation> ().Play ("PressUp");
	}

	//Starts Coroutine which manages the new level sequence
	IEnumerator Wait ()
	{
		yield return new WaitForSeconds (2);

	}
}