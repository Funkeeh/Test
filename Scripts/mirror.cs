using UnityEngine;
using System.Collections;

public class mirror : MonoBehaviour 
{
	public GameObject 	original;
	public GameObject 	reflection;
	private Quaternion  reflectedRotation;

	void Start () 
	{
		//Nothing
    }

	void Update () 
	{
		//Reflect Position
		reflection.transform.position = calibMirrorPlane.mirrorPosition - Vector3.Reflect ((calibMirrorPlane.mirrorPosition - original.transform.position), calibMirrorPlane.mirrorNormal);

		//Reflect Rotation     
		reflectedRotation.SetLookRotation ( Vector3.Reflect( original.transform.forward, calibMirrorPlane.mirrorNormal ), -Vector3.Reflect( original.transform.up, calibMirrorPlane.mirrorNormal ));
		reflection.transform.rotation = reflectedRotation;
	}
}