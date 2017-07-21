using UnityEngine;
using System.Collections;

//Managing the Goal Area
public class BendingGameObjectHandler : MonoBehaviour 
{
	//GameObjects / Handlers ----------------------------------------------------------------------------------------------------------------------------------------------//
	public GameObject	m_objectRight;														//Right Object
	public GameObject	m_objectHandle;	
	public GameObject  	m_objectMid;														//Middle Object


	//private Quaternion 	m_cylinderRotAdjust 	= Quaternion.Euler(90, 0, 0);				//Adjust the initial rotation of the object

	void Start () 
	{
		//Nothing
	}

	void Update () 
	{
		Vector3 positioning 					= transform.position;
		Vector3 mirroredPosition				= new Vector3 (-positioning.x, positioning.y, positioning.z);	//Get mirrored position

		m_objectRight.transform.position 		= mirroredPosition;

		Quaternion rotation 					= transform.rotation;

		m_objectRight.transform.localRotation = new Quaternion (	rotation.x * -1.0f, 
																	rotation.y, 
																	rotation.z, 
																	rotation.w * -1.0f );

		//Manage Mid Object
		Vector3 localToWorld 	= m_objectHandle.transform.TransformPoint ( 0, 0, 0 );			
		Vector3 midCylPos 		= new Vector3( 0, localToWorld.y, localToWorld.z );
		m_objectMid.transform.position 	= midCylPos;

		//Sets the mid cylinder x-value equal to the x-position of the right cylinder
		float dist 				= localToWorld.x;
		Vector3 localScale 		= m_objectMid.transform.localScale;
		localScale.y 			= dist;
		m_objectMid.transform.localScale 	= localScale;	

		if (BendingGame.gameOver == true) 
		{
			
		}
	}
}
