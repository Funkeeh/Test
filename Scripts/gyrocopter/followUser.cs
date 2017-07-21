using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followUser : MonoBehaviour 
{
	public 	Transform 	user;
	private Transform 	pos; 
	private Vector3 	getPos;
	private float		yPos;

	void Start () 
	{
		pos = GetComponent<Transform> ();
		yPos = pos.position.y;
	}

	void Update () 
	{
		getPos = new Vector3 ( user.position.x, yPos, user.position.z );
		pos.position = getPos;
	}
}
