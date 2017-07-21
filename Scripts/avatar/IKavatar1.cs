using UnityEngine;
using System;
using System.Collections;

[RequireComponent (typeof(Animator))] 
public class IKavatar1 : MonoBehaviour
{
	protected Animator animator;
	public bool ikActive = false;

	public static bool activeLegLeftRight = true;                               //Used to handle which leg input to use (only left version in use right now)
	public static bool calibrationProcess = false;                              //Activate IKavatar after calibration

	//Head movement/rotations -----//
	public Transform headMarker     = null;                                     //[CameraRig] > Camera (head) > BehindEyes (GO)
	public Transform headBone       = null;                                     //Avatar head transform
    public Vector3 adjustCameraPos = new Vector3(0.0f, 0.0f, 0.0f);             //Wanted CameraRig pos          (not in use?)
    public Vector3 adjustCameraRot = new Vector3(0.0f, 0.0f, 0.0f);             //wanted CameraRig rot          (not in use?)

    //Upper body movement/rotations -----//
    public Transform chestBone      = null;                                     //Avatar chestLower transform
	public Transform rootMarker     = null;                                     //[CameraRig] : The avatar hip position based on hmd
	public Transform rootBone       = null;                                     //Avatar Hip : Move entire avatar

    public Transform spine;                                                     //Avatar Hip > AbdomenUpper

    private Vector3 rootMarkerAnchor;                                           //Vector from the anchor point to the origin of rootInitial in local coordinates of rootMarker  (Not in use?)
	private Vector3 rootOffsetToInitial;                                        //Translational difference between rootMarker and rootInitial                                   (Not in use?)

	//Adjust Upper Body and Arms
	private Vector3 neckRot = new Vector3 (-200.0f, -50.0f, 0.0f);              //Rotate neck preset (hide avatar head)
	//public Vector3 spineRot = new Vector3 (30.0f, 0.0f, 0.0f);                //Spine Adjustment
	public Vector3 hipRot   = new Vector3 (-25.0f, 0.0f, 0.0f);                 //Hip Adjust
	public Vector3 leftUpperArm = new Vector3 (20.0f, -15.0f, 80.0f);           //Upper Arm Adjust (pull back)
	public Vector3 leftLowerArm = new Vector3 (0.0f, 0.0f, 0.0f);               // -
	public Vector3 rightUpperArm = new Vector3 (20.0f, 15.0f, -80.0f);          //Upper Arm Adjust (pull back)
	public Vector3 rightLowerArm = new Vector3 (0.0f, 0.0f, 0.0f);              // -

    //Leg movement/rotations -----//
    public Transform llMarker   = null;                                         //[CameraRig] > Controller (right) > leg
	public Transform llBone     = null;                                         //Avatar Hip > Pelvis > lThigh
	public Transform rlBone     = null;                                         //Avatar Hip > Pelvis > rThigh

    //Foot movement/rotations -----//
    public Transform lfMarker   = null;                                         //[CameraRig] > Controller (left) > foot
	public Transform lfBone     = null;                                         //Avatar Hip > Pelvis > lThigh > lShin > lFoot
	public Transform rfBone     = null;                                         //Avatar Hip > Pelvis > rThigh > rShin > rFoot
	private Vector3 lfMarkerAnchor;

    //Gyrocopter variables
    public GameObject chopper;                                                  //Gyrocopter (GO)
    public Transform helicopter;                                                //Gyrocopter (GO)
    public GameObject seatPos;                                                  //Seating position on helicopter 
    public Transform swingPos;                                                  //Gyrocopter > swingingPoint : Position for swinging and cycling motions
	private Vector3 initialSwingPos = Vector3.zero;
	public float cyclingoffset  = -0.16f;                                       //z-axis offset for mirred leg in cycling mode
    public float cyclingoffsetZ = -0.14f;                                       //y-axis Offset for mirrored leg in cycling mode

    //MIRRORING -----//
    private Quaternion reflectedRotation;                                       //Mirrored Rotation Values for mirrored Avatar Leg
	public Vector3 calibSwingPos = Vector3.zero;

	//FOOT ORIENTATION VARIABLES
	private Vector3 newPosition = Vector3.zero;
	private Vector3 oldPosition = Vector3.zero;
	public static int currentCyclePos   = 0;                                    //New cycle offset position
	private static int currentStdPos    = 0;                                    //New std offset
	public static int setCurrentPos     = 0;                                    //Set foot status

    //OTHER VARS
	private bool addForceOnceCycling    = true;                                 //Add force once (Cycling)
	private float cyclingForceTimer     = 0.0f;                                 //Not in use
	private bool addForceOnceOthers     = true;                                 //Add force once (std and swing)
	private float otherForceTimer       = 0.0f;                                 //Not in use
	private float oldPositionFoot       = 0.0f;                                 //Foot pos in previous frame
	private float newPositionFoot       = 0.0f;                                 //Foot pos current frame
	private Vector3 footPos = Vector3.zero;
	public Transform cycleLeftFoot;                                             //New offset on left foot (For Cycle orientation

    void Start ()
	{
		animator = GetComponent<Animator> ();

		initialSwingPos = swingPos.position;

		adjustCameraPos = new Vector3 (1.14f, -0.972f, -0.54f);        //Set preset position (based on htv vive calibration)
		adjustCameraRot = new Vector3 (0.0f, 180.0f, 0.0f);            //Set preset rotation
	}

	private void Update ()
	{
        if (Input.GetKeyDown(KeyCode.F1)) //Calibrate by pressing "F1" 
        {
            setRootPosRot();    //calibrate function
        }

        //Position Avatar to Chair
        rootBone.transform.position = seatPos.transform.position;

        //STANDARD VERSION & SWINGING VERSION: Checks for a full swing
        if (swapLegControls.legMirrorVersions == 0 || swapLegControls.legMirrorVersions == 1)
        {
            float Zdistance = swingPos.position.z - cycleLeftFoot.position.z;       //Foot orientation in regards to the swing pos (positive val = foot in front, negative val = foot behind)
            newPositionFoot = lfBone.position.z - chopper.transform.position.z;     //The change in forward motion of helicopter
			float difference = newPositionFoot - oldPositionFoot;                   //Change in position of foot from prevoius frame
			oldPositionFoot = newPositionFoot;                                      //Update old frame
			float threshold = 0.1f;                                                 //least amount of distance from swingPosOrigin (when a swing is accepted)

			if (Zdistance < threshold && difference < 0.0f && currentStdPos == 0)           
            {
				currentStdPos = 1;
				cakeLight.activeCakeLight = 2;
				addForceOnceOthers = true;                  //Reset
				otherForceTimer = 0.0f;                     //Reset
                gyroCopterManager.enoughVelocity = false;   //reset
            }
            else if (Zdistance > threshold && difference > 0.0f && currentStdPos == 1)
            {
				currentStdPos = 2;
				cakeLight.activeCakeLight = 4;
			}
            else if (Zdistance < -threshold && currentStdPos == 2)
            {
				currentStdPos = 0;
				cakeLight.activeCakeLight = 5;
			}

            //When to add force ----------------------//
            if (cakeLight.activeCakeLight == 5)
            {
				otherForceTimer = otherForceTimer + Time.deltaTime;

                if (addForceOnceOthers == true)
                {
                    gyroCopterManager.enoughVelocity = true;    //Add force 1 frame
                    addForceOnceOthers = false;
                }
                else
                {
                    gyroCopterManager.enoughVelocity = false;
                }
                if (otherForceTimer > 0.1f)                     //leave if-statement
                {
					cakeLight.activeCakeLight = 0;
					addForceOnceOthers = false;
				}
			}
        }

		//CYCLE VERSION: Checks for a full swing of the pedals (full round)
		if (swapLegControls.legMirrorVersions == 2)
        {
			Vector3 cyclePos = new Vector3 (0.0f, swingPos.position.y, swingPos.position.z);        //Center of the cycle Gear, ignoring x axis
			footPos = new Vector3 (0.0f, cycleLeftFoot.position.y, cycleLeftFoot.position.z);       //Foot position, ignoring x axis
			newPosition = footPos - cyclePos;                                                       //Vector pointing from cycle position to foot position
			float angle = Vector3.Angle (oldPosition, newPosition);                                 //Change in angle from previous to current frame
			float currentAngle = Vector3.Angle (swingPos.up, newPosition);                          //Angle from vector3.up to current position
			Vector3 cross = Vector3.Cross (swingPos.up, newPosition);                               //If angle is positive or negative (to check if in front or behind of cycle position)
			oldPosition = newPosition;                                                              //Update old frame

			if (cross.x < 0.0f && currentAngle > 90.0f && currentCyclePos == 0)                                     //Check if foot went down in front of cyle (first 90 degrees)
            {     
				currentCyclePos = 1;
				cakeLight.activeCakeLight = 1;
				addForceOnceCycling = true;                 //reset
				cyclingForceTimer = 0.0f;                   //reset
				gyroCopterManager.enoughVelocity = false;   //reset
			}
            else if (cross.x < 0.0f && currentAngle > 135.0f && currentCyclePos == 1)                               //Check is foot went down in front of cyle (135 degrees)
            {   
				currentCyclePos = 2;
				cakeLight.activeCakeLight = 2;
			}
            else if (cross.x > 0.0f && currentAngle > 80.0f && currentAngle < 90.0f && currentCyclePos == 2)        //Check if foot goes back up behind cycle
            {
				currentCyclePos = 3;
				cakeLight.activeCakeLight = 3;
			}
            else if (cross.x > 0.0f && currentAngle < 35.0f && currentCyclePos == 3)                                //Check if foot goes back up behind cycle
            {      
				currentCyclePos = 0;
				cakeLight.activeCakeLight = 5;
			}

            //When to add force ----------------------//
			if (cakeLight.activeCakeLight == 5)
            {
                cyclingForceTimer = cyclingForceTimer + Time.deltaTime;

                if (addForceOnceCycling == true)
                {
                    gyroCopterManager.enoughVelocity = true;        //Add force 1 frame
                    addForceOnceCycling = false;
                }
                else
                {
                    gyroCopterManager.enoughVelocity = false;
                }
                if (cyclingForceTimer > 0.1f)                       //leave if-statement
                {
                    cakeLight.activeCakeLight = 0;
				}
			}
        }
    }

	//CALIBRATION FUNCTION -FOR UI
	public void setRootPosRot ()        //Only used for activating the avatar at the moment...
	{
		if (null != rootMarker && null != rootBone)
        {
			//Nothing
		}
		if (null != llMarker)
        {
            //Nothing
		}

		//Only follow HTC Vive Controllers after a calibration
		calibrationProcess = true;
	}

	//Animating the Avatar
	void OnAnimatorIK ()
	{
		if (animator)
        {	
			if (!ikActive)
            {
				animator.SetIKPositionWeight (AvatarIKGoal.LeftFoot, 0);
				animator.SetIKRotationWeight (AvatarIKGoal.LeftFoot, 0); 
			}
            else
            {	
				//INITIAL POS / ROTS OF AVATAR
				if (null != rootMarker && null != rootBone)
                {
					animator.SetBoneLocalRotation (HumanBodyBones.Neck, Quaternion.Euler (neckRot));                        //HEAD
					animator.SetBoneLocalRotation (HumanBodyBones.Hips, Quaternion.Euler (hipRot));                         //HIPS
					//animator.SetBoneLocalRotation(HumanBodyBones.Spine, Quaternion.Euler(spineRot));
					animator.SetBoneLocalRotation (HumanBodyBones.LeftUpperArm, Quaternion.Euler (leftUpperArm));           //LEFT ARM
					animator.SetBoneLocalRotation (HumanBodyBones.LeftLowerArm, Quaternion.Euler (leftLowerArm));
					animator.SetBoneLocalRotation (HumanBodyBones.RightUpperArm, Quaternion.Euler (rightUpperArm));         //RIGHT ARM
					animator.SetBoneLocalRotation (HumanBodyBones.RightLowerArm, Quaternion.Euler (rightLowerArm));
				}

				//UPPER BODY MOVEMENT (HEAD)
				if (null != headBone && calibrationProcess == true)
                {
					Vector3 lookDir = headMarker.position - chestBone.position;                                             //Upper body wanted y-direction
					Vector3 forward1 = Vector3.Cross (lookDir, helicopter.transform.right);                                 //Create forward z-direction by cross product of y and x directions

					//Apply parent rotation (inverse) * new look rotation, using the forward and look directions
					animator.SetBoneLocalRotation (HumanBodyBones.Spine, Quaternion.Inverse (spine.parent.rotation) * Quaternion.LookRotation (forward1, lookDir));
				}

				//LEG ORIENTATION MANAGER (KNEE)
				if (null != llMarker && null != llBone && calibrationProcess == true) //Left Leg is the controller ----------------------------//
                {
					if (activeLegLeftRight == true)
                    {
						animator.SetIKHintPositionWeight (AvatarIKHint.LeftKnee, 1.0f);
						animator.SetIKHintPosition (AvatarIKHint.LeftKnee, swapControllers.activeLeg.TransformPoint (new Vector3 (0.0f, 0.05f, 0.0f)));

						animator.SetIKHintPositionWeight (AvatarIKHint.RightKnee, 1.0f);

						//Mirror Position to other foot
						Vector3 mirroredPos = new Vector3 (-swapControllers.activeLeg.TransformPoint (new Vector3 (0.0f, 0.05f, 0.0f)).x, swapControllers.activeLeg.TransformPoint (new Vector3 (0.0f, 0.05f, 0.0f)).y, swapControllers.activeLeg.TransformPoint (new Vector3 (0.0f, 0.05f, 0.0f)).z); 
						animator.SetIKHintPosition (AvatarIKHint.RightKnee, mirroredPos);
						//animator.SetIKHintPosition(AvatarIKHint.RightKnee, calibMirrorPlane.mirrorPosition - Vector3.Reflect((calibMirrorPlane.mirrorPosition - swapControllers.activeLeg.TransformPoint(new Vector3(0.0f, 0.05f, 0.0f))), calibMirrorPlane.mirrorNormal));
					}

                    else //Right Leg is the controller  --------------------------//
                    {
						animator.SetIKHintPositionWeight (AvatarIKHint.RightKnee, 1.0f);
						animator.SetIKHintPosition (AvatarIKHint.RightKnee, swapControllers.activeLeg.TransformPoint (new Vector3 (0.0f, 0.05f, 0.0f)));

						animator.SetIKHintPositionWeight (AvatarIKHint.LeftKnee, 1.0f);
						//Mirror Position to other foot
						animator.SetIKHintPosition (AvatarIKHint.RightKnee, calibMirrorPlane.mirrorPosition - Vector3.Reflect ((calibMirrorPlane.mirrorPosition - swapControllers.activeLeg.TransformPoint (new Vector3 (0.0f, 0.05f, 0.0f))), calibMirrorPlane.mirrorNormal));
					}
				}

				//FOOT ORIENTATION MANAGER (FOOT)
				if (null != lfMarker && null != lfBone && calibrationProcess == true)
                {
					animator.SetIKPositionWeight (AvatarIKGoal.LeftFoot, 1);         //Instansiate Left Foot
					animator.SetIKRotationWeight (AvatarIKGoal.LeftFoot, 1);
					animator.SetIKPositionWeight (AvatarIKGoal.RightFoot, 1);        //Instansiate Right Foot
					animator.SetIKRotationWeight (AvatarIKGoal.RightFoot, 1);

					if (activeLegLeftRight == true)
                    {         //Left Foot is the controller
						//Set position and rotation of foot based on "active" foot Controller
						animator.SetIKPosition (AvatarIKGoal.LeftFoot, swapControllers.activeFoot.TransformPoint (lfMarkerAnchor));
						animator.SetIKRotation (AvatarIKGoal.LeftFoot, swapControllers.activeFoot.rotation);

                        float zDistOffset = swapControllers.activeFoot.TransformPoint(lfMarkerAnchor).z - swingPos.position.z;                                           //Distance offset for z-direction
                        float zDistOffsetCycling = swapControllers.activeFoot.TransformPoint (lfMarkerAnchor).z - swingPos.position.z + cyclingoffsetZ;                  //Distance offset for z-direction
						float yDistOffset = swapControllers.activeFoot.TransformPoint (lfMarkerAnchor).y - swingPos.position.y + cyclingoffset;                          //Distance offset for y-direction

                        //Standard Mirror Position to other foot
                        if (swapLegControls.legMirrorVersions == 0)
                        {
							Vector3 mirroredPos = new Vector3 (-swapControllers.activeFoot.TransformPoint (lfMarkerAnchor).x, swapControllers.activeFoot.TransformPoint (lfMarkerAnchor).y, swapControllers.activeFoot.TransformPoint (lfMarkerAnchor).z); 
							animator.SetIKPosition (AvatarIKGoal.RightFoot, mirroredPos);

							//Mirror Rotation to other foot 
							reflectedRotation.SetLookRotation (Vector3.Reflect (swapControllers.activeFoot.transform.forward, calibMirrorPlane.mirrorNormal), Vector3.Reflect (swapControllers.activeFoot.transform.up, calibMirrorPlane.mirrorNormal));
							animator.SetIKRotation (AvatarIKGoal.RightFoot, reflectedRotation);
						}

						//Swinging Mirror Position to other foot
						if (swapLegControls.legMirrorVersions == 1)
                        {
							float newZvalue = swingPos.position.z - zDistOffset;
							Vector3 mirroredPos = new Vector3 (-swapControllers.activeFoot.TransformPoint (lfMarkerAnchor).x, swapControllers.activeFoot.TransformPoint (lfMarkerAnchor).y, newZvalue); 
							animator.SetIKPosition (AvatarIKGoal.RightFoot, mirroredPos);

							//Mirror Rotation to other foot 
							reflectedRotation.SetLookRotation (Vector3.Reflect (swapControllers.activeFoot.transform.forward, calibMirrorPlane.mirrorNormal), Vector3.Reflect (swapControllers.activeFoot.transform.up, calibMirrorPlane.mirrorNormal));
							Vector3 newRot = reflectedRotation.eulerAngles;
                            //animator.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.Euler(-newRot.x, newRot.y, newRot.z));          //Old version for reference
                            animator.SetIKRotation (AvatarIKGoal.RightFoot, Quaternion.Euler (-newRot.x + 25.0f, 180.0f, 0.0f));        //Inverse x rotation plus 25 degree offset to make the foot look more realistic. Y direction is always forward and Z is zero preset
						}
                                                
						//Cycling Version: Mirror Foot Position to other foot
						if (swapLegControls.legMirrorVersions == 2)
                        {
							float newZvalue = swingPos.position.z - zDistOffsetCycling;
							float newYvalue = swingPos.position.y - yDistOffset;

                            Vector3 mirroredPos = new Vector3 (-swapControllers.activeFoot.TransformPoint (lfMarkerAnchor).x, newYvalue, newZvalue); 
                            animator.SetIKPosition (AvatarIKGoal.RightFoot, mirroredPos);

							//Mirror Rotation to other foot 
							reflectedRotation.SetLookRotation (Vector3.Reflect (swapControllers.activeFoot.transform.forward, calibMirrorPlane.mirrorNormal), Vector3.Reflect (swapControllers.activeFoot.transform.up, calibMirrorPlane.mirrorNormal));
							Vector3 newRot = reflectedRotation.eulerAngles;
							animator.SetIKRotation (AvatarIKGoal.RightFoot, Quaternion.Euler (-newRot.x, newRot.y, newRot.z));
						}       
						//animator.SetIKPosition(AvatarIKGoal.RightFoot, calibMirrorPlane.mirrorPosition - Vector3.Reflect((calibMirrorPlane.mirrorPosition - swapControllers.activeFoot.position), calibMirrorPlane.mirrorNormal));

					}
                    else //Right Foot is the controller (NOT WORKING ATM)
                    {  
						//Set position and rotation of foot based on "active" right foot Controller
						animator.SetIKPosition (AvatarIKGoal.RightFoot, swapControllers.activeFootControlRightFoot.TransformPoint (lfMarkerAnchor));
						animator.SetIKRotation (AvatarIKGoal.RightFoot, swapControllers.activeFootControlRightFoot.rotation);

						//Mirror Position to other foot
						animator.SetIKPosition (AvatarIKGoal.RightFoot, calibMirrorPlane.mirrorPosition - Vector3.Reflect ((calibMirrorPlane.mirrorPosition - swapControllers.activeFoot.position), calibMirrorPlane.mirrorNormal));

						//Mirror Rotation to other foot  
						reflectedRotation.SetLookRotation (Vector3.Reflect (swapControllers.activeFoot.transform.forward, calibMirrorPlane.mirrorNormal), Vector3.Reflect (swapControllers.activeFoot.transform.up, calibMirrorPlane.mirrorNormal));
						animator.SetIKRotation (AvatarIKGoal.RightFoot, reflectedRotation);
					}
				}
			}
		}
	}

	public void SetSwingPos ()
	{
		Vector3 newSwingPos = new Vector3 (swingPos.position.x, swingPos.position.y, swapControllers.activeFoot.TransformPoint (lfMarkerAnchor).z);
		swingPos.position = newSwingPos;
	}

	public void ResetSwingPos ()
	{
		Vector3 newSwingPos = new Vector3 (initialSwingPos.x, swingPos.position.y, initialSwingPos.z);
		swingPos.position = newSwingPos;
	}

	public void SetCyclePos ()
	{
		Vector3 newSwingPos = new Vector3 (swingPos.position.x, swapControllers.activeFoot.TransformPoint (lfMarkerAnchor).y, swapControllers.activeFoot.TransformPoint (lfMarkerAnchor).z);
		swingPos.position = newSwingPos;
	}

	public void ResetCyclePos ()
	{
		Vector3 newSwingPos = new Vector3 (initialSwingPos.x, initialSwingPos.y, initialSwingPos.z);
		swingPos.position = newSwingPos;
	}
}