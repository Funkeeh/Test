using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startPoint : MonoBehaviour 
{
	public static bool hitStartPoint = false;
	private Collider col;

	void Start()
	{
		//col = GetComponent<Collider> ();
	}

	void OnTriggerEnter()
	{
		hitStartPoint 	= true;
		//col.enabled = false;
	}
}
