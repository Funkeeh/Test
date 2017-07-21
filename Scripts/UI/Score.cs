using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour 
{

	public Text Score1;
	private int hold;

	void Start () 
	{
		
	}

	void Update () 
	{
		hold = gyroCopterManager.ScoreTotal;
		Score1.text = hold.ToString();
		//Score1.text = gyroCopterManager.ScoreTotal.ToString;
	}
}
