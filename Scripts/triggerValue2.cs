using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerValue2 : MonoBehaviour
{

    SteamVR_TrackedObject trackedObj;
    SteamVR_Controller.Device device;

    public static float trigger2;
    public static bool pressingTrigger2 = false;

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void FixedUpdate()
    {
        //Get input from the tracked object (which this script is attached to)
        device = SteamVR_Controller.Input((int)trackedObj.index);

        //Get pressed value from "Trigger" button (between 0 and 1)
        Vector2 value = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
        trigger2 = value.x;
        //Debug.Log ("Trigger touched. Value of axis is " + trigger);

        if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            pressingTrigger2 = true;
        }
        else
        {
            pressingTrigger2 = false;
        }
    }
}