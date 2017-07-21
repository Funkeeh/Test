using UnityEngine;
using System.Collections;

public class calibMirrorPlane : MonoBehaviour 
{
	public GameObject 			cameraPlane;
	public static Vector3     	mirrorNormal;
	public static Vector3     	mirrorPosition;
	public static Quaternion 	mirrorRotation;

	void Start () 
	{
		mirrorNormal = cameraPlane.transform.right;                 //Camera.transform.right
		Vector3.Normalize(mirrorNormal);                            //Normalized

		mirrorPosition = cameraPlane.transform.position;
		mirrorRotation = cameraPlane.transform.rotation;
	}
		
	void Update () 
	{
		//Set Camera Plane Mirror
		if ( Input.GetKeyDown( KeyCode.F2 ) )
		{
			mirrorNormal = cameraPlane.transform.right;             //Camera.transform.right (up?)
			Vector3.Normalize(mirrorNormal);                        //Normalized

			mirrorPosition = cameraPlane.transform.position;
			mirrorRotation = cameraPlane.transform.rotation;
		}
	}

	//FOR USE IN UI
	public void CalibrateMirrorPosition ()
	{
		mirrorNormal = cameraPlane.transform.right;             	//Camera.transform.right (up?)
		Vector3.Normalize(mirrorNormal);                       	 	//Normalized

		mirrorPosition = cameraPlane.transform.position;
		mirrorRotation = cameraPlane.transform.rotation;
	}
}
