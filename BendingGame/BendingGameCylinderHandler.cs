using UnityEngine;
using System.Collections;
using Neuron;

//Bending Game Cylinder Handler
//Checks input from the NeuronSkeleton positions and handles the bending and positioning of the Cylinders
//Cylinder Game Objects (place in inspector): m_cylinder (left cylinder), m_cylinderMirrored (right cylinder), m_cylinderMid (middle cylinder) and m_cylinderHandle (left cylinder joint)
//NeuronSkeleton inputs: Left hand, Index finger: priximal, middle and distan, Pinky finger: Proximal and m_HandleLeft (joint for cylinder)
public class BendingGameCylinderHandler : MonoBehaviour
{
	public static bool 	grabCylinderWorld	= false;
	private bool 		grabCylinder		= false;

	//Checks if the cylinder (Nunchuck) has been grabbed and is now released. 
	public static bool 	relasingCylinder = false;		

	//Grab/release variables
	private float minIndexBend  		= 0.64f;
	private float minIndexToWrist 		= 1.61f;
	private float minDistToCylinder		= 1.00f;

	//Static values for use only when bending game is open;
	public static float minBendIndexSetting;
	public static float minIndextoHandSetting;
	public static float minDistancetoCylSetting;
	public static float minBendIndexInput;
	public static float minIndextoHandInput;
	public static float minDistancetoCylInput;

	//Cylinder GameObjects / Handlers -------------------------------------------------------------------------------------------------------------------------------//
	public GameObject 	m_cylinder;							//GO: BG_CylinderLeft
	public GameObject	m_cylinderHandle;					//GO: BG_CylinderLeft > Joint
	public GameObject	m_cylinderMirrored;					//GO: BG_CylinderRight
	public GameObject  	m_cylinderMid;						//GO: BG_CylinderMid

	private Vector3 	initiatCylinderPos;													//Initial position and rotation of Nunchuck
	private Quaternion 	initialCylinderRot;

	private Quaternion 	m_cylinderRotAdjust 	= Quaternion.Euler(90, 0, 0);				//Adjust the initial rotation of the cylinders

	//Avatar Objects
	public GameObject	dummyLeft;
	public GameObject	dummyRight;

	//Cylinder Skeleton   / Left Dummy ---------------------------------------------------------------------------------------------------------------------------------//
	public GameObject  	b_leftHand;							//Left Hand of OurDummyAvatar: Robot_References> Hips> Spine> LeftHand	//Only pos used
	public GameObject	b_leftIndexProximal; 				//> LeftHand> Leftindex1												//Only pos used
	public GameObject 	b_leftIndexMiddle;					//> LeftHand> Leftindex2
	public GameObject	b_leftIndexDistal;					//> LeftHand> Leftindex3												//Only pos used
	public GameObject 	b_leftPinkyProximal;				//> LeftHand> Leftpinky1												//Only pos used
	public GameObject 	m_HandleLeft;						//Left Hand of Avatar: Robot_References> Hips> Spine> LeftHand> Handle	//both

	//Cylinder Skeleton   / Right Dummy --------------------------------------------------------------------------------------------------------------------------------//
	public GameObject  	b_rightHand;						//Left Hand of OurDummyAvatarR: Robot_References> Hips> Spine> LeftHand
	public GameObject	b_rightIndexProximal; 				//> LeftHand> Leftindex1
	public GameObject	b_rightIndexDistal;					//> LeftHand> Leftindex3
	public GameObject 	b_rightPinkyProximal;				//> LeftHand> Leftpinky1
	public GameObject 	m_HandleRight;						//Left Hand of Avatar: Robot_References> Hips> Spine> LeftHand> Handle

	void Start()
	{
		initiatCylinderPos = m_cylinder.transform.position;
		initialCylinderRot = m_cylinder.transform.rotation;
	}

	void Update()
	{
		//Debug.Log ("Dummy Left Version is active: " + dummyLeft.activeSelf + "; Dummy Right Version is active " + dummyRight.activeSelf);

		Vector3 palmposition;
		Vector3 cylinderPosition;
		float distIndexDistalToProxy;
		float distIndexToHand;
		float distPalmToCylinder;

		if (dummyLeft.activeSelf == true) 
		{
			//Position between Index and Pinky Proximal
			palmposition 			= ((b_leftIndexProximal.transform.position - b_leftPinkyProximal.transform.position) * 0.5f) + b_leftPinkyProximal.transform.position;
			cylinderPosition 		= m_cylinder.transform.position;

			//Distances
			distIndexDistalToProxy 	= (b_leftIndexDistal.transform.position - b_leftIndexProximal.transform.position).magnitude;
			distIndexToHand 		= (b_leftHand.transform.position - b_leftIndexDistal.transform.position).magnitude;
			distPalmToCylinder		= (palmposition - cylinderPosition).magnitude;
		} 
		else 
		{
			//Position between Index and Pinky Proximal
			palmposition 			= ((b_rightIndexProximal.transform.position - b_rightPinkyProximal.transform.position) * 0.5f) + b_rightPinkyProximal.transform.position;
			cylinderPosition 		= m_cylinder.transform.position;

			//Distances
			distIndexDistalToProxy 	= (b_rightIndexDistal.transform.position - b_rightIndexProximal.transform.position).magnitude;
			distIndexToHand 		= (b_rightHand.transform.position - b_rightIndexDistal.transform.position).magnitude;
			distPalmToCylinder		= (palmposition - cylinderPosition).magnitude;
		}
			
		//Check when to grab the Cylinder
		if (minIndexBend > distIndexDistalToProxy && minIndexToWrist > distIndexToHand && minDistToCylinder > distPalmToCylinder) 
		{
			grabCylinder 		= true;
			relasingCylinder 	= false;
		}
		else if (minIndexToWrist < distIndexToHand) 
		{
			if (grabCylinder == true) 
			{
				relasingCylinder = true;
			}

			grabCylinder 		= false;
		}

		//Move Cylinder handles
		if (grabCylinder == true) 
		{
			Vector3 positioning;

			if (dummyLeft.activeSelf == true)
			{
				positioning 					= m_HandleLeft.transform.position;								//Get position of handle
			}
			else
			{
				positioning 					= m_HandleRight.transform.position;								//Get position of handle
			}

			//Vector3 positioning 					= m_HandleLeft.transform.position;								//Get position of handle
			Vector3 positioningM					= new Vector3 (-positioning.x, positioning.y, positioning.z);	//Get mirrored position

			m_cylinder.transform.position 			= positioning;

			if (dummyLeft.activeSelf == true)
			{
				m_cylinder.transform.rotation 			= m_HandleLeft.transform.rotation * m_cylinderRotAdjust;		//Rotate Cylinder based on Handle rotation * Initial Adjustment
			}
			else
			{
				m_cylinder.transform.rotation 			= m_HandleRight.transform.rotation * m_cylinderRotAdjust;		//Rotate Cylinder based on Handle rotation * Initial Adjustment
			}

			//m_cylinder.transform.rotation 			= m_HandleLeft.transform.rotation * m_cylinderRotAdjust;		//Rotate Cylinder based on Handle rotation * Initial Adjustment
			m_cylinderMirrored.transform.position 	= positioningM;

			//Mirror the rotation of the real cylinder on to the mirrored
			m_cylinderMirrored.transform.localRotation = new Quaternion (	m_cylinder.transform.localRotation.x * -1.0f, 
																			m_cylinder.transform.localRotation.y, 
																			m_cylinder.transform.localRotation.z, 
																			m_cylinder.transform.localRotation.w * -1.0f );
			MidCylinder ();	//Handle middle cylinder
		}

		//Reset the position of the Nunchuck
		if (BendingGame.startNextRound == true) 
		{
			ResetNunchuk ();
		}

		//Remove Nunchuck if game is done
		if (BendingGame.gameOver == true) 
		{
			m_cylinder.transform.position 			= new Vector3 (0.0f, -10.0f, 0.0f);
			m_cylinderMid.transform.position 		= new Vector3 (0.0f, -10.0f, 0.0f);
			m_cylinderMirrored.transform.position 	= new Vector3 (0.0f, -10.0f, 0.0f);
		}

		grabCylinderWorld = grabCylinder;

		//Send settings To Debug
		minBendIndexSetting 		= minIndexBend;
		minIndextoHandSetting 		= minIndexToWrist;
		minDistancetoCylSetting 	= minDistToCylinder;
		minBendIndexInput			= distIndexDistalToProxy;
		minIndextoHandInput			= distIndexToHand;
		minDistancetoCylInput		= distPalmToCylinder;

	}

	//Mid cylinder Manager
	void MidCylinder()
	{
		//Places the Mid cylinder between left and right cylinder
		Vector3 localToWorld 	= m_cylinderHandle.transform.TransformPoint ( 0, 0, 0 );			
		Vector3 midCylPos 		= new Vector3( 0, localToWorld.y, localToWorld.z );
		m_cylinderMid.transform.position 	= midCylPos;

		//Sets the mid cylinder x-value equal to the x-position of the right cylinder
		float dist 				= localToWorld.x;
		Vector3 localScale 		= m_cylinderMid.transform.localScale;
		localScale.y 			= dist;
		m_cylinderMid.transform.localScale 	= localScale;
	}

	//Use to reset the Nunchuck (For UI)
	public void ResetNunchuk ()
	{
		m_cylinder.transform.position = initiatCylinderPos;
		m_cylinder.transform.rotation = initialCylinderRot;

		Vector3 positioning 					= m_cylinder.transform.position;								//Get position of handle
		Vector3 positioningM					= new Vector3 (-positioning.x, positioning.y, positioning.z);	//Get mirrored position

		m_cylinderMirrored.transform.position 	= positioningM;

		//Mirror the rotation of the real cylinder on to the mirrored
		m_cylinderMirrored.transform.localRotation = new Quaternion (	m_cylinder.transform.localRotation.x * -1.0f, 
			m_cylinder.transform.localRotation.y, 
			m_cylinder.transform.localRotation.z, 
			m_cylinder.transform.localRotation.w * -1.0f );

		MidCylinder ();	//Handle middle cylinder
	}
		

	//Adjusts the values for when the user is able to grab the Nunchuck. Used in UI.
	public void AdjustHandVariabels (float number)
	{
		int useInCase = (int)number;

		//Number input: -5 lowest, -4, -3, -2, -1, 0 default, 1, 2, 3, 4, 5 highest
		switch (useInCase) 
		{
		case -5: //Lowest values
			minIndexBend 		= 0.58f;
			minIndexToWrist 	= 1.54f;
			minDistToCylinder 	= 0.90f;
			break;
		case -4:
			minIndexBend 		= 0.59f;
			minIndexToWrist 	= 1.55f;
			minDistToCylinder 	= 0.90f;
			break;
		case -3:
			minIndexBend 		= 0.60f;
			minIndexToWrist 	= 1.56f;
			minDistToCylinder 	= 0.95f;
			break;
		case -2:
			minIndexBend 		= 0.61f;
			minIndexToWrist 	= 1.57f;
			minDistToCylinder 	= 0.95f;
			break;
		case -1:
			minIndexBend 		= 0.62f;
			minIndexToWrist 	= 1.58f;
			minDistToCylinder 	= 1.00f;
			break;
		case 0:  //Same as Default values
			minIndexBend 		= 0.63f;
			minIndexToWrist 	= 1.60f;
			minDistToCylinder 	= 1.10f;
			break;
		case 1:
			minIndexBend 		= 0.64f;
			minIndexToWrist 	= 1.62f;
			minDistToCylinder 	= 1.20f;
			break;
		case 2:
			minIndexBend 		= 0.66f;
			minIndexToWrist 	= 1.64f;
			minDistToCylinder 	= 1.25f;
			break;
		case 3:
			minIndexBend 		= 0.67f;
			minIndexToWrist 	= 1.66f;
			minDistToCylinder 	= 1.25f;
			break;
		case 4:
			minIndexBend 		= 0.68f;
			minIndexToWrist 	= 1.68f;
			minDistToCylinder 	= 1.25f;
			break;
		case 5: //Highest values
			minIndexBend 		= 0.70f;
			minIndexToWrist 	= 1.70f;
			minDistToCylinder 	= 1.30f;
			break;
		default:
			minIndexBend 		= 0.63f;
			minIndexToWrist 	= 1.60f;
			minDistToCylinder 	= 1.00f;
			break;
		}
	}
}