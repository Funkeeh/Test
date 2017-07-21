using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pedalCycling : MonoBehaviour 
{
	public GameObject target;

	void Start () 
	{
		
	}

	void Update () 
	{
		transform.position = target.transform.position;
	}
}
