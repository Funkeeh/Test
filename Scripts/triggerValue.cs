using UnityEngine;
using System.Collections;

//Attach to controller you want to check 
[RequireComponent(typeof(SteamVR_TrackedObject))] //Checks if controller is active /is able to track a controller
public class triggerValue : MonoBehaviour 
{
	SteamVR_TrackedObject trackedObj;
	SteamVR_Controller.Device device;

	public static float trigger;
	public static bool 	pressingTrigger     = false;

    //Values from both triggers (triggerValue and triggerValue2)
    public static bool  oneTriggerPressed   = false;    //Checks for both controllers  
    public static float bothTriggers;

    void Awake () 
	{
		trackedObj = GetComponent<SteamVR_TrackedObject>();
	}

	void FixedUpdate () 
	{
		//Get input from the tracked object (which this script is attached to)
		device = SteamVR_Controller.Input( (int)trackedObj.index );

        //Debug.Log(bothTriggers);

		//Get pressed value from "Trigger" button (between 0 and 1)
		Vector2 value 	= device.GetAxis ( Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger );
		trigger 		= value.x;
		//Debug.Log ("Trigger touched. Value of axis is " + trigger);

		if (device.GetTouch (SteamVR_Controller.ButtonMask.Trigger)) 
		{
			pressingTrigger = true;
		} 
		else 
		{
			pressingTrigger = false;
		}

        //handles both Trigger inputs
        if (pressingTrigger == true || triggerValue2.pressingTrigger2 == true)
        {
            oneTriggerPressed = true;
        }
        else
        {
            oneTriggerPressed = false;
        }

        //Assign largest of each trigger value
        if (trigger > triggerValue2.trigger2)
        {
            bothTriggers = trigger;
        }
        else if (trigger <= triggerValue2.trigger2)
        {
            bothTriggers = triggerValue2.trigger2;
        }
        else
        {
            bothTriggers = 0.0f;
        }

        //Debug.Log("Trigger Status: " + oneTriggerPressed + ", " + bothTriggers);
    }
}
