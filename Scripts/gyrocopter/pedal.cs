using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pedal : MonoBehaviour
{
	private LineRenderer line;
	public	Transform start;
	public	Transform end;

	void Start () 
	{
		line = GetComponent<LineRenderer> ();
	}

	void Update () 
	{
		line.SetPosition (0, start.position);
		line.SetPosition (1, end.position);
	}
}