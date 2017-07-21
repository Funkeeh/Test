using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Movement : MonoBehaviour 
{
	public WheelCollider wheelFR;
	public WheelCollider wheelFL;
	public WheelCollider wheelRR;
	public WheelCollider wheelRL;

	public float motorForce 	= 100.0f;
	public float steerRadius 	= 10.0f;
	public float breakforce		= 100.0f;

	public void FixedUpdate()
	{
		float applyTorque = 0;

		if(Input.GetButton("xBox_AddTorque")) 
		{
			applyTorque = motorForce;
		}
		else
		{
			applyTorque = 0;
		}

		float applySteer 	= Input.GetAxis ("Horizontal") 	* steerRadius;

		//Apply force to all wheels (4-wheel drive, yo)
		wheelFR.motorTorque = applyTorque;
		wheelFL.motorTorque = applyTorque;
		wheelRR.motorTorque = applyTorque;
		wheelRL.motorTorque = applyTorque;

		//Apply Steering
		wheelFR.steerAngle 	= applySteer;
		wheelFL.steerAngle 	= applySteer;

		//Apply Breaks
		if(Input.GetButton("xBox_Break")) 
		{
			wheelFR.brakeTorque = breakforce;
			wheelFL.brakeTorque = breakforce;
			wheelRR.brakeTorque = breakforce;
			wheelRL.brakeTorque = breakforce;
		}
		else
		{
			wheelFR.brakeTorque = 0;
			wheelFL.brakeTorque = 0;
			wheelRR.brakeTorque = 0;
			wheelRL.brakeTorque = 0;
		}
	}
}
