using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pedalManager : MonoBehaviour 
{
    public GameObject standard;
	public GameObject cycle;
	public GameObject standardFEMALE;
	public GameObject cycleFEMALE;
	public GameObject cogwheelLeft;
	public GameObject cogwheelRight;
	public GameObject lineRendLeft;
	public GameObject lineRendRight;
	public GameObject lineRendLeftFEMALE;
	public GameObject lineRendRightFEMALE;

    public GameObject cycleGearBox; //The gear in the middle
    public GameObject cycleGearBoxF; //The gear in the middle
    public GameObject femaleLeftFoot;
    public GameObject maleLeftFoot;

    public Transform cycleLineLeft, cycleLineRight, swingingPoint;

    private Vector3 newFootPos = Vector3.zero;
    private Vector3 oldFootPos = Vector3.zero;

    void Update () //DET HER ER LORT - FIX DET TIL EN FUNKTION
	{
        cycleLineLeft.position = new Vector3 (0.04f, swingingPoint.position.y, swingingPoint.position.z);
        cycleLineRight.position = new Vector3(-0.04f, swingingPoint.position.y, swingingPoint.position.z);

        switch (swapLegControls.legMirrorVersions) 
		{
		case 0:			//Standard Movement
			cogwheelLeft.SetActive 	(true);
			cogwheelRight.SetActive (true);

			if (swapLegControls.maleFemaleAvatar == true)
			{
				standard.SetActive 		(true);
				cycle.SetActive 		(false);
				lineRendLeft.SetActive 	(true);
				lineRendRight.SetActive (true);

				standardFEMALE.SetActive 		(false);
				cycleFEMALE.SetActive 			(false);
				lineRendLeftFEMALE.SetActive 	(false);
				lineRendRightFEMALE.SetActive 	(false);
			}
			else
			{
				standard.SetActive 		(false);
				cycle.SetActive 		(false);
				lineRendLeft.SetActive 	(false);
				lineRendRight.SetActive (false);

				standardFEMALE.SetActive 		(true);
				cycleFEMALE.SetActive 			(false);
				lineRendLeftFEMALE.SetActive 	(true);
				lineRendRightFEMALE.SetActive 	(true);
			}
			break;
		case 1:			//Swinging Movement
			cogwheelLeft.SetActive 	(true);
			cogwheelRight.SetActive (true);

			if (swapLegControls.maleFemaleAvatar == true)
			{
				standard.SetActive 		(false);
				cycle.SetActive 		(false);
				lineRendLeft.SetActive 	(true);
				lineRendRight.SetActive (true);

				standardFEMALE.SetActive 		(false);
				cycleFEMALE.SetActive 			(false);
				lineRendLeftFEMALE.SetActive 	(false);
				lineRendRightFEMALE.SetActive 	(false);
			}
			else
			{
				standard.SetActive 		(false);
				cycle.SetActive 		(false);
				lineRendLeft.SetActive 	(false);
				lineRendRight.SetActive (false);

				standardFEMALE.SetActive 		(false);
				cycleFEMALE.SetActive 			(false);
				lineRendLeftFEMALE.SetActive 	(true);
				lineRendRightFEMALE.SetActive 	(true);
			}
			break;
		case 2:			//Cycling Movement
			cogwheelLeft.SetActive 	(false);
			cogwheelRight.SetActive (false);

            if (swapLegControls.maleFemaleAvatar == true)
			{
                    //cycleGearBoxF.transform.LookAt(femaleLeftFoot.transform.position);
                    Vector3 relativePos = cycleGearBox.transform.position - maleLeftFoot.transform.position;        //vector looking from whatever
                    relativePos.x = 0.0f;                                                                           //reset x to 0
                    Quaternion newRot = Quaternion.LookRotation(relativePos);
                    newFootPos = maleLeftFoot.transform.position;

                    float Ydifference = oldFootPos.y - newFootPos.y;
                    float Zdifference = oldFootPos.z - newFootPos.z;

                    //cycleGearBox.transform.rotation = newRot;

                    if (IKavatar1.currentCyclePos == 0 && Ydifference > 0.0f || IKavatar1.currentCyclePos == 0 && Zdifference < 0.0f)
                    {
                        cycleGearBox.transform.rotation = newRot;
                    }
                    else if (IKavatar1.currentCyclePos == 1 && Ydifference > 0.0f || IKavatar1.currentCyclePos == 1 && Zdifference > 0.0f)
                    {
                        cycleGearBox.transform.rotation = newRot;
                    }
                    else if (IKavatar1.currentCyclePos == 2 && Ydifference < 0.0f || IKavatar1.currentCyclePos == 2 && Zdifference > 0.0f)
                    {
                        cycleGearBox.transform.rotation = newRot;
                    }
                    else if (IKavatar1.currentCyclePos == 3 && Ydifference < 0.0f || IKavatar1.currentCyclePos == 3 && Zdifference < 0.0f)
                    {
                        cycleGearBox.transform.rotation = newRot;
                    }

                    oldFootPos = newFootPos;

                    standard.SetActive 		(false);
				    cycle.SetActive 		(true);
				    lineRendLeft.SetActive 	(false);
				    lineRendRight.SetActive (false);

				    standardFEMALE.SetActive 		(false);
				    cycleFEMALE.SetActive 			(false);
				    lineRendLeftFEMALE.SetActive 	(false);
				    lineRendRightFEMALE.SetActive 	(false);
			}
			else
			{
                    //cycleGearBoxF.transform.LookAt(femaleLeftFoot.transform.position);
                    Vector3 relativePos = cycleGearBoxF.transform.position - femaleLeftFoot.transform.position;
                    relativePos.x = 0.0f;
                    Quaternion newRot = Quaternion.LookRotation (relativePos);
                    newFootPos = femaleLeftFoot.transform.position;

                    float Ydifference = oldFootPos.y - newFootPos.y;

                    if (IKavatar1.currentCyclePos == 0 && Ydifference > 0.0f || IKavatar1.currentCyclePos == 1 && Ydifference > 0.0f)
                    {
                        cycleGearBoxF.transform.rotation = newRot;
                    }
                    else if (IKavatar1.currentCyclePos == 2 && Ydifference < 0.0f || IKavatar1.currentCyclePos == 3 && Ydifference < 0.0f)
                    {
                        cycleGearBoxF.transform.rotation = newRot;
                    }

                    oldFootPos = newFootPos;

                    standard.SetActive 		(false);
				    cycle.SetActive 		(false);
				    lineRendLeft.SetActive 	(false);
				    lineRendRight.SetActive (false);

				    standardFEMALE.SetActive 		(false);
				    cycleFEMALE.SetActive 			(true);
				    lineRendLeftFEMALE.SetActive 	(false);
				    lineRendRightFEMALE.SetActive 	(false);
			}				
			break;
		default:
			    standard.SetActive 		(true);
			    cycle.SetActive 		(false);
			    cogwheelLeft.SetActive 	(true);
			    cogwheelRight.SetActive (true);
			    lineRendLeft.SetActive 	(true);
			    lineRendRight.SetActive (true);
			break;
		}	
	}
}