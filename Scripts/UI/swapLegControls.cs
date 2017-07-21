using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swapLegControls : MonoBehaviour
{
    public static bool maleFemaleAvatar = true;     //True = Male Avatar; False = Female Avatar
    public static int  legMirrorVersions = 0;       //0 = Standard Mirror, 1 = Swinging, 2 = Cycling

    public GameObject avatarMale;
    public GameObject avatarFemale;

    public void swapLegsandFeet()
    {
        if (IKavatar1.activeLegLeftRight == true)
        {
            IKavatar1.activeLegLeftRight = false;

            Debug.Log("RIGHT LEG: ACTIVE");
        }
        else
        {
            IKavatar1.activeLegLeftRight = true;
            Debug.Log("LEFT LEG: ACTIVE");
        }
    }
    public void swapAvatars()
    {
        if (maleFemaleAvatar == true)
        {
            maleFemaleAvatar = false;
            Debug.Log("FEMALE AVATAR: ACTIVE");

            avatarMale.SetActive(false);
            avatarFemale.SetActive(true);
        }
        else
        {
            maleFemaleAvatar = true;
            Debug.Log("MALE AVATAR: ACTIVE");

            avatarMale.SetActive(true);
            avatarFemale.SetActive(false);
        }
    }

    public void setMirrorVersion (int value)
    {
        legMirrorVersions = value;
        if (value == 2) //If cycling version is active, change forcelimit value (for gyrocoptermanager)
        {
            gyroCopterManager.velocityLimit = 0.8f;
        }
        else
        {
            gyroCopterManager.velocityLimit = 1.5f;
        }
    }
}