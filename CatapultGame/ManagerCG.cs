using UnityEngine;
using System.Collections;

public class ManagerCG : MonoBehaviour 
{
	public GameObject start;
	public GameObject mangolYes;
	public GameObject mangolNo;
	public GameObject TrebuchYes;
	public GameObject TrebuchNo;

	public GameObject mangolet;
	public GameObject trebuchet;

	private bool chooseGame = true;
	private bool backToMenu = true;

	// Use this for initialization
	void Start () 
	{
		start.SetActive (true);
		mangolNo.SetActive (false);
		TrebuchYes.SetActive (false);
		mangolYes.SetActive (true);
		TrebuchNo.SetActive (true);

		mangolet.SetActive (false);
		trebuchet.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButton ("xBox_Start")) 
		{
			backToMenu = true;

			start.SetActive (true);
			mangolYes.SetActive (true);
			TrebuchNo.SetActive (true);
			mangolNo.SetActive (false);
			TrebuchYes.SetActive (false);

			mangolet.SetActive (false);
			trebuchet.SetActive (false);
		}

		if (backToMenu == true) 
		{
			if (Input.GetAxis ("Horizontal") < 0.0f) 
			{
				chooseGame = true;
			}

			if (Input.GetAxis ("Horizontal") > 0.0f) 
			{
				chooseGame = false;
			}

			if (chooseGame == true) 
			{
				mangolNo.SetActive (false);
				mangolYes.SetActive (true);
				TrebuchNo.SetActive (true);
				TrebuchYes.SetActive (false);
			}

			if (chooseGame == false) 
			{
				mangolNo.SetActive (true);
				mangolYes.SetActive (false);
				TrebuchNo.SetActive (false);
				TrebuchYes.SetActive (true);
			}

			if (Input.GetButton ("xBox_AddTorque")) 
			{
				start.SetActive (false);
				mangolNo.SetActive (false);
				mangolYes.SetActive (false);
				TrebuchNo.SetActive (false);
				TrebuchYes.SetActive (false);

				if (chooseGame == true) 
				{
					mangolet.SetActive (true);
					trebuchet.SetActive (false);
				}

				if (chooseGame == false) 
				{
					mangolet.SetActive (false);
					trebuchet.SetActive (true);
				}

				backToMenu = false;
			}
		}
	}
}
