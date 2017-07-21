using UnityEngine;
using System.Collections;

public class BendingGameSillouette : MonoBehaviour 
{
	//For sound effects
	public AudioSource pwrUP;

	private bool whenToPlay = false;

	void Start () 
	{
		//audio = GetComponent<AudioSource>();
	}

	void Update () 
	{
		if 		( BendingGameTransparency.enableRend == true ) //If the cylinder is within the correct position
		{
			GetComponent<Renderer> ().enabled = true;

			if (whenToPlay == false) 
			{
				pwrUP.Play ();
				whenToPlay = true;
			}
		}

		else if ( BendingGameTransparency.enableRend == false ) //If the cylinder is oustide the correct position
		{
			GetComponent<Renderer> ().enabled = false;

			if (whenToPlay == true) 
			{
				whenToPlay = false;
			}
		}

		else
		{
			GetComponent<Renderer> ().enabled = false;
		}
	}
}
