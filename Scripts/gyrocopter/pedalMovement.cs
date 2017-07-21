using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pedalMovement : MonoBehaviour 
{
	public GameObject target;
	public GameObject foot;

	public Vector3 offset = new Vector3 (0.0f, 0.0f, 0.0f);

	void Start () 
	{
		//Nothing
	}

	void Update () 
	{
		transform.position = target.transform.position;

		Vector3 footRot = foot.transform.rotation.eulerAngles;
		Vector3 newRot	= footRot + offset;
		Quaternion rotate = Quaternion.Euler (newRot); 

		transform.rotation = rotate;
	}
}