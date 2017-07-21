using UnityEngine;
using System.Collections;

public class Mirror2 : MonoBehaviour 
{
	//Perception Neuron Input
	public GameObject initLeftArm;		
	public GameObject initLeftForearm;	
	public GameObject initLeftHand;
	public GameObject initLeftThumb1;	public GameObject initLeftThumb2;	public GameObject initLeftThumb3;	public GameObject initLeftThumb4;
	public GameObject initLeftIndex;	public GameObject initLeftIndex1;	public GameObject initLeftIndex2;	public GameObject initLeftIndex3;	public GameObject initLeftIndex4;
	public GameObject initLeftMiddle;	public GameObject initLeftMiddle1;	public GameObject initLeftMiddle2;	public GameObject initLeftMiddle3;	public GameObject initLeftMiddle4;
	public GameObject initLeftPinky;	public GameObject initLeftPinky1;	public GameObject initLeftPinky2;	public GameObject initLeftPinky3;	public GameObject initLeftPinky4;
	public GameObject initLeftRing;		public GameObject initLeftRing1;	public GameObject initLeftRing2;	public GameObject initLeftRing3;	public GameObject initLeftRing4;

	//Dummy - Left Arm Input
	public GameObject cubeLeftArm;
	public GameObject cubeLeftForearm;
	public GameObject cubeLeftHand;
	public GameObject cubeLeftThumb1;	public GameObject cubeLeftThumb2;	public GameObject cubeLeftThumb3;	public GameObject cubeLeftThumb4;
	public GameObject cubeLeftIndex;	public GameObject cubeLeftIndex1;	public GameObject cubeLeftIndex2;	public GameObject cubeLeftIndex3;	public GameObject cubeLeftIndex4;
	public GameObject cubeLeftMiddle;	public GameObject cubeLeftMiddle1;	public GameObject cubeLeftMiddle2;	public GameObject cubeLeftMiddle3;	public GameObject cubeLeftMiddle4;
	public GameObject cubeLeftPinky;	public GameObject cubeLeftPinky1;	public GameObject cubeLeftPinky2;	public GameObject cubeLeftPinky3;	public GameObject cubeLeftPinky4;
	public GameObject cubeLeftRing;		public GameObject cubeLeftRing1;	public GameObject cubeLeftRing2;	public GameObject cubeLeftRing3;	public GameObject cubeLeftRing4;

	//Dummy - Right Arm Input, Mirrored
	public GameObject cubeRightArm;
	public GameObject cubeRightForearm;
	public GameObject cubeRightHand;
	public GameObject cubeRightThumb1;	public GameObject cubeRightThumb2;	public GameObject cubeRightThumb3;	public GameObject cubeRightThumb4;
	public GameObject cubeRightIndex;	public GameObject cubeRightIndex1;	public GameObject cubeRightIndex2;	public GameObject cubeRightIndex3;	public GameObject cubeRightIndex4;
	public GameObject cubeRightMiddle;	public GameObject cubeRightMiddle1;	public GameObject cubeRightMiddle2;	public GameObject cubeRightMiddle3;	public GameObject cubeRightMiddle4;
	public GameObject cubeRightPinky;	public GameObject cubeRightPinky1;	public GameObject cubeRightPinky2;	public GameObject cubeRightPinky3;	public GameObject cubeRightPinky4;
	public GameObject cubeRightRing;	public GameObject cubeRightRing1;	public GameObject cubeRightRing2;	public GameObject cubeRightRing3;	public GameObject cubeRightRing4;

	//Oculus Input and initial spine positions
	public GameObject initHeadMovement;
	private float	  lastPosition;

	//Adjust Position
	public float adjust = -1.0f;

	//Dummy - Neck, Shoulder and Spine Input
	public GameObject cubeNeck;			public GameObject cubeShoulderRight;	
										public GameObject cubeShoulderLeft;
	public GameObject cubeSpine3;		public GameObject cubeSpine2;		public GameObject cubeSpine1;
	private Vector3   spine3StartPos;	private Vector3   spine2StartPos;	private Vector3   spine1StartPos;

	public GameObject	head;

	void Start ()
	{
		lastPosition = initHeadMovement.transform.position.z;

		head.transform.position = new Vector3 (0, 100, -500);
	}

	void Update () 
	{
		//Set Left Arm Rotations
		cubeLeftArm.transform.rotation 			= initLeftArm.transform.rotation;
		cubeLeftForearm.transform.rotation 		= initLeftForearm.transform.rotation;
		cubeLeftHand.transform.rotation			= initLeftHand.transform.rotation;
		cubeLeftThumb1.transform.rotation 		= initLeftThumb1.transform.rotation;
		cubeLeftThumb2.transform.rotation 		= initLeftThumb2.transform.rotation;
		cubeLeftThumb3.transform.rotation		= initLeftThumb3.transform.rotation;
		cubeLeftThumb4.transform.rotation 		= initLeftThumb4.transform.rotation;
		cubeLeftIndex.transform.rotation 		= initLeftIndex.transform.rotation;
		cubeLeftIndex1.transform.rotation		= initLeftIndex1.transform.rotation;
		cubeLeftIndex2.transform.rotation 		= initLeftIndex2.transform.rotation;
		cubeLeftIndex3.transform.rotation 		= initLeftIndex3.transform.rotation;
		cubeLeftIndex4.transform.rotation		= initLeftIndex4.transform.rotation;
		cubeLeftMiddle.transform.rotation 		= initLeftMiddle.transform.rotation;
		cubeLeftMiddle1.transform.rotation 		= initLeftMiddle1.transform.rotation;
		cubeLeftMiddle2.transform.rotation		= initLeftMiddle2.transform.rotation;
		cubeLeftMiddle3.transform.rotation 		= initLeftMiddle3.transform.rotation;
		cubeLeftMiddle4.transform.rotation 		= initLeftMiddle4.transform.rotation;
		cubeLeftPinky.transform.rotation 		= initLeftPinky.transform.rotation;
		cubeLeftPinky1.transform.rotation 		= initLeftPinky1.transform.rotation;
		cubeLeftPinky2.transform.rotation		= initLeftPinky2.transform.rotation;
		cubeLeftPinky3.transform.rotation 		= initLeftPinky3.transform.rotation;
		cubeLeftPinky4.transform.rotation 		= initLeftPinky4.transform.rotation;
		cubeLeftRing.transform.rotation 		= initLeftRing.transform.rotation;
		cubeLeftRing1.transform.rotation 		= initLeftRing1.transform.rotation;
		cubeLeftRing2.transform.rotation		= initLeftRing2.transform.rotation;
		cubeLeftRing3.transform.rotation 		= initLeftRing3.transform.rotation;
		cubeLeftRing4.transform.rotation 		= initLeftRing4.transform.rotation;

		//Set Right Arm Mirrored Rotations
		cubeRightArm.transform.localRotation 			= new Quaternion (	cubeLeftArm.transform.localRotation.x * -1.0f, 		cubeLeftArm.transform.localRotation.y, 
																			cubeLeftArm.transform.localRotation.z, 				cubeLeftArm.transform.localRotation.w * -1.0f );
		cubeRightForearm.transform.localRotation 		= new Quaternion (	cubeLeftForearm.transform.localRotation.x * -1.0f, 	cubeLeftForearm.transform.localRotation.y, 
																			cubeLeftForearm.transform.localRotation.z, 			cubeLeftForearm.transform.localRotation.w * -1.0f );
		cubeRightHand.transform.localRotation 			= new Quaternion (	cubeLeftHand.transform.localRotation.x * -1.0f, 	cubeLeftHand.transform.localRotation.y, 
																			cubeLeftHand.transform.localRotation.z, 			cubeLeftHand.transform.localRotation.w * -1.0f );
		cubeRightThumb1.transform.localRotation 		= new Quaternion (	cubeLeftThumb1.transform.localRotation.x * -1.0f, 	cubeLeftThumb1.transform.localRotation.y, 
																			cubeLeftThumb1.transform.localRotation.z, 			cubeLeftThumb1.transform.localRotation.w * -1.0f );
		cubeRightThumb2.transform.localRotation 		= new Quaternion (	cubeLeftThumb2.transform.localRotation.x * -1.0f, 	cubeLeftThumb2.transform.localRotation.y, 
																			cubeLeftThumb2.transform.localRotation.z, 			cubeLeftThumb2.transform.localRotation.w * -1.0f );
		cubeRightThumb3.transform.localRotation 		= new Quaternion (	cubeLeftThumb3.transform.localRotation.x * -1.0f, 	cubeLeftThumb3.transform.localRotation.y, 
																			cubeLeftThumb3.transform.localRotation.z, 			cubeLeftThumb3.transform.localRotation.w * -1.0f );
		cubeRightThumb4.transform.localRotation 		= new Quaternion (	cubeLeftThumb4.transform.localRotation.x * -1.0f, 	cubeLeftThumb4.transform.localRotation.y, 
																			cubeLeftThumb4.transform.localRotation.z, 			cubeLeftThumb4.transform.localRotation.w * -1.0f );
		cubeRightIndex.transform.localRotation 			= new Quaternion (	cubeLeftIndex.transform.localRotation.x * -1.0f, 	cubeLeftIndex.transform.localRotation.y, 
																			cubeLeftIndex.transform.localRotation.z, 			cubeLeftIndex.transform.localRotation.w * -1.0f );
		cubeRightIndex1.transform.localRotation 		= new Quaternion (	cubeLeftIndex1.transform.localRotation.x * -1.0f, 	cubeLeftIndex1.transform.localRotation.y, 
																			cubeLeftIndex1.transform.localRotation.z, 			cubeLeftIndex1.transform.localRotation.w * -1.0f );
		cubeRightIndex2.transform.localRotation 		= new Quaternion (	cubeLeftIndex2.transform.localRotation.x * -1.0f, 	cubeLeftIndex2.transform.localRotation.y, 
																			cubeLeftIndex2.transform.localRotation.z, 			cubeLeftIndex2.transform.localRotation.w * -1.0f );
		cubeRightIndex3.transform.localRotation 		= new Quaternion (	cubeLeftIndex3.transform.localRotation.x * -1.0f, 	cubeLeftIndex3.transform.localRotation.y, 
																			cubeLeftIndex3.transform.localRotation.z, 			cubeLeftIndex3.transform.localRotation.w * -1.0f );
		cubeRightIndex4.transform.localRotation 		= new Quaternion (	cubeLeftIndex4.transform.localRotation.x * -1.0f, 	cubeLeftIndex4.transform.localRotation.y, 
																			cubeLeftIndex4.transform.localRotation.z, 			cubeLeftIndex4.transform.localRotation.w * -1.0f );
		cubeRightMiddle.transform.localRotation 		= new Quaternion (	cubeLeftMiddle.transform.localRotation.x * -1.0f, 	cubeLeftMiddle.transform.localRotation.y, 
																			cubeLeftMiddle.transform.localRotation.z, 			cubeLeftMiddle.transform.localRotation.w * -1.0f );
		cubeRightMiddle1.transform.localRotation 		= new Quaternion (	cubeLeftMiddle1.transform.localRotation.x * -1.0f, 	cubeLeftMiddle1.transform.localRotation.y, 
																			cubeLeftMiddle1.transform.localRotation.z, 			cubeLeftMiddle1.transform.localRotation.w * -1.0f );
		cubeRightMiddle2.transform.localRotation 		= new Quaternion (	cubeLeftMiddle2.transform.localRotation.x * -1.0f, 	cubeLeftMiddle2.transform.localRotation.y, 
																			cubeLeftMiddle2.transform.localRotation.z, 			cubeLeftMiddle2.transform.localRotation.w * -1.0f );
		cubeRightMiddle3.transform.localRotation 		= new Quaternion (	cubeLeftMiddle3.transform.localRotation.x * -1.0f, 	cubeLeftMiddle3.transform.localRotation.y, 
																			cubeLeftMiddle3.transform.localRotation.z, 			cubeLeftMiddle3.transform.localRotation.w * -1.0f );
		cubeRightMiddle4.transform.localRotation 		= new Quaternion (	cubeLeftMiddle4.transform.localRotation.x * -1.0f, 	cubeLeftMiddle4.transform.localRotation.y, 
																			cubeLeftMiddle4.transform.localRotation.z, 			cubeLeftMiddle4.transform.localRotation.w * -1.0f );
		cubeRightPinky.transform.localRotation 			= new Quaternion (	cubeLeftPinky.transform.localRotation.x * -1.0f, 	cubeLeftPinky.transform.localRotation.y, 
																			cubeLeftPinky.transform.localRotation.z, 			cubeLeftPinky.transform.localRotation.w * -1.0f );
		cubeRightPinky1.transform.localRotation 		= new Quaternion (	cubeLeftPinky1.transform.localRotation.x * -1.0f, 	cubeLeftPinky1.transform.localRotation.y, 
																			cubeLeftPinky1.transform.localRotation.z, 			cubeLeftPinky1.transform.localRotation.w * -1.0f );
		cubeRightPinky2.transform.localRotation 		= new Quaternion (	cubeLeftPinky2.transform.localRotation.x * -1.0f, 	cubeLeftPinky2.transform.localRotation.y, 
																			cubeLeftPinky2.transform.localRotation.z, 			cubeLeftPinky2.transform.localRotation.w * -1.0f );
		cubeRightPinky3.transform.localRotation 		= new Quaternion (	cubeLeftPinky3.transform.localRotation.x * -1.0f, 	cubeLeftPinky3.transform.localRotation.y, 
																			cubeLeftPinky3.transform.localRotation.z, 			cubeLeftPinky3.transform.localRotation.w * -1.0f );
		cubeRightPinky4.transform.localRotation 		= new Quaternion (	cubeLeftPinky4.transform.localRotation.x * -1.0f, 	cubeLeftPinky4.transform.localRotation.y, 
																			cubeLeftPinky4.transform.localRotation.z, 			cubeLeftPinky4.transform.localRotation.w * -1.0f );
		cubeRightRing.transform.localRotation 			= new Quaternion (	cubeLeftRing.transform.localRotation.x * -1.0f, 	cubeLeftRing.transform.localRotation.y, 
																			cubeLeftRing.transform.localRotation.z, 			cubeLeftRing.transform.localRotation.w * -1.0f );
		cubeRightRing1.transform.localRotation 			= new Quaternion (	cubeLeftRing1.transform.localRotation.x * -1.0f, 	cubeLeftRing1.transform.localRotation.y, 
																			cubeLeftRing1.transform.localRotation.z, 			cubeLeftRing1.transform.localRotation.w * -1.0f );
		cubeRightRing2.transform.localRotation 			= new Quaternion (	cubeLeftRing2.transform.localRotation.x * -1.0f, 	cubeLeftRing2.transform.localRotation.y, 
																			cubeLeftRing2.transform.localRotation.z, 			cubeLeftRing2.transform.localRotation.w * -1.0f );
		cubeRightRing3.transform.localRotation 			= new Quaternion (	cubeLeftRing3.transform.localRotation.x * -1.0f, 	cubeLeftRing3.transform.localRotation.y, 
																			cubeLeftRing3.transform.localRotation.z, 			cubeLeftRing3.transform.localRotation.w * -1.0f );
		cubeRightRing4.transform.localRotation 			= new Quaternion (	cubeLeftRing4.transform.localRotation.x * -1.0f, 	cubeLeftRing4.transform.localRotation.y, 
																			cubeLeftRing4.transform.localRotation.z, 			cubeLeftRing4.transform.localRotation.w * -1.0f );

		//Set Right Arm Mirrored Positions
		//cubeRightArm.transform.position					= new Vector3 (-cubeLeftArm.transform.position.x, 		cubeLeftArm.transform.position.y, 		cubeLeftArm.transform.position.z);
		//cubeRightForearm.transform.position				= new Vector3 (-cubeLeftForearm.transform.position.x, 	cubeLeftForearm.transform.position.y, 	cubeLeftForearm.transform.position.z);
		//cubeRightHand.transform.position				= new Vector3 (-cubeLeftHand.transform.position.x, 		cubeLeftHand.transform.position.y, 		cubeLeftHand.transform.position.z);

		//Calculate the position of the Head and Spine based on Oculus camera position
		float currentPosition 	= initHeadMovement.transform.position.z + adjust;
		float velocity 			= currentPosition - lastPosition;
		lastPosition 			= currentPosition;
 
		Vector3 newHeadPos 		= new Vector3 ( 0, 0, (velocity * 0.25f) );
		cubeNeck.transform.position				+= newHeadPos;
		cubeShoulderRight.transform.position	+= newHeadPos;
		cubeShoulderLeft.transform.position		+= newHeadPos;

		Vector3 newSpine3Pos 	= new Vector3 ( 0, 0, (velocity * 0.25f) );
		Vector3 newSpine2Pos 	= new Vector3 ( 0, 0, (velocity * 0.25f) );
		Vector3 newSpine1Pos 	= new Vector3 ( 0, 0, (velocity * 0.25f) );
		cubeSpine3.transform.position			+= newSpine3Pos;
		cubeSpine2.transform.position			+= newSpine2Pos;
		cubeSpine1.transform.position			+= newSpine1Pos;
	}
}
