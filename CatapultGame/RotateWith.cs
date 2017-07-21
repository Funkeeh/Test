using UnityEngine;
using System.Collections;

public class RotateWith : MonoBehaviour 
{
	public GameObject go;
	private Quaternion rotate;

	// Use this for initialization
	void Start () 
	{
		//Nothing
	}

	// Update is called once per frame
	void FixedUpdate () 
	{
		rotate = go.transform.rotation;
		transform.rotation = new Quaternion (rotate.y, 0, 0, 1);
	}
}
