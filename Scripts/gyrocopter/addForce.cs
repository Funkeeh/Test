using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addForce : MonoBehaviour 
{
	private float basicThrust 	= 775.0f;		//800
	private float pedalThrust 	= 100.0f;

    //FORCE ADJUSTMENTS
    [Tooltip("When a high updwards velocity")]
    public float forceUpHighVel     = 0.0f;
    [Tooltip("When going upwards in steady velocity")]
    public float forceUpMediumVel   = 200.0f;
    [Tooltip("When going upwards slowly")]
    public float forceUpLowVel      = 250.0f;
    [Tooltip("When almost 0 upgoing velocity (but still going up)")]
    public float forceUpNoVel       = 445.0f;
    [Tooltip("When a high downwards velocity")]
    public float forceDownHighVel   = 4250.0f;
    [Tooltip("When going down in steady velocity")]
    public float forceDownMediumVel = 1500.0f;
    [Tooltip("When going down in low-med velocity")]
    public float forceDownLowMedVel = 750.0f;
    [Tooltip("When going down slowly")]
    public float forceDownLowVel    = 500.0f;
    [Tooltip("When almost 0 downgoing velocity (but still going down)")]
    public float forceDownNoVel     = 455.0f;

    private Transform pos;
	private Rigidbody rb;

	public static float velocity 		= 0.0f;		//Forward position (not velocity)
	public static float velocityY		= 0.0f;
	public static float velocityZ		= 0.0f;
	public static float altitude		= 0.0f;

	public Transform goalArea;					//Endpoint > Helipad > GoalArea
	public static int backRotaterSpeed = 0;		//Used in rotateBack script


	void Start () 
	{
		rb 	= GetComponent<Rigidbody> ();
		pos = GetComponent<Transform> ();
	}

    void FixedUpdate()
    {
        rb.freezeRotation = true;                                           //Removes rotations

        //FORCE Y-DIRECTIONS (up/down) ----------------------------------------------------------------------------- //
        rb.AddForce(transform.up * basicThrust);                            //Base Force (so helicopter does not just drop)
        altitude = pos.position.y + 10.0f;                                  //current altitude of helicpter (for altitude meter)

        //FORCE MANAGER -------------------------------------------------------------------------------------------- //
        if (Input.GetKeyDown("space") && gyroCopterManager.gameStart == true)                                                                   //debug
        {
            rb.AddForce(transform.up * (basicThrust + pedalThrust));
        }
        if (gyroCopterManager.enoughVelocity == true && gyroCopterManager.gameStart == true)         
        {
            rb.AddForce(transform.up * (basicThrust + pedalThrust)); 
        }

        //Maximum Downwards Velocity Handler
        if (rb.velocity.y < -8.0f)                  //If helicopter moves down, apply some upwards force
        {
            rb.AddForce(transform.up * 500.0f);
        }

        //Borders (bottom and top)                  //Stop from flying beneath or above these values
        if (pos.position.y < -8.0f)
        {
            rb.AddForce(transform.up * 1500.0f);
        }
        if (pos.position.y > 260.0f)
        {
            rb.AddForce(transform.up * -500.0f);
        }

        //PEDAL FORCE INPUT  --------------------------------------------------------------------------------------------- //
        if (rb.velocity.y > 0)                      //Upgoing ------------------------ //
        {
            if (rb.velocity.y > 7.0f)          //helicopter upward velocity too high = no force up
            {
                pedalThrust = forceUpHighVel;
            }
            else if (rb.velocity.y > 1.6f)      //copter goes up medium pace
            {
                pedalThrust = (forceUpMediumVel + gyroCopterManager.appliedForce);    // base (750) + force for active game version
            }
            else if (rb.velocity.y > 0.6f)      //copter goes up slow
            {
                pedalThrust = (forceUpLowVel + gyroCopterManager.appliedForce);    // base (750) + force for active game version
            }
            else                                //helicopter stands still (almost)
            {
                pedalThrust = (forceUpNoVel + gyroCopterManager.appliedForce);    // base (800) + force for active game version
            }
        }
        else if (rb.velocity.y < 0)                 //Downgoing ----------------------//
        {
            if (rb.velocity.y < -4.0f)
            {
                pedalThrust = (forceDownHighVel + gyroCopterManager.appliedForce);   // base (2250) + force for active game version
            }
            else if (rb.velocity.y < -2.0f)
            {
                pedalThrust = (forceDownMediumVel + gyroCopterManager.appliedForce);   // base (2250) + force for active game version
            }
            else if (rb.velocity.y < -1.2f)
            {
                pedalThrust = (forceDownLowMedVel + gyroCopterManager.appliedForce);   //1200
            }
            else if (rb.velocity.y < -0.6f)
            {
                pedalThrust = (forceDownLowVel + gyroCopterManager.appliedForce);   //1000
            }
            else
            {
                pedalThrust = (forceDownNoVel + gyroCopterManager.appliedForce);   //2500
            }
        }
        else
        {
            pedalThrust = (100.0f + gyroCopterManager.appliedForce);   //2500
        }

		//FORCE Z-DIRECTIONS (Forward) ------------------------------------------------------------------------------------- //
		velocity = pos.position.z;
		velocityZ = rb.velocity.z;
		//FORCE Y-DIRECTION (up/down)
		velocityY = pos.position.y;

		//Moving forward Sequence
		if (startPoint.hitStartPoint == true)
		{
			if (pos.position.z < -500000.0f)	//Reduce Velocity if above value (not in use; always within this value)
			{
				backRotaterSpeed = 1;

				if (rb.velocity.z < -5.0f)
				{
					rb.AddForce (transform.forward * 100.0f);
					Debug.Log ("Reducing speed");
				}
				else if (pos.position.z <= goalArea.position.z)
				{
					rb.velocity = Vector3.zero;
					Debug.Log ("Finished!");

					startPoint.hitStartPoint = false;
				}
				else
				{
					//Do Nothing
				}

			}
			if (rb.velocity.z < -20.0f) 	//At Maximum velocity 
			{
				backRotaterSpeed = 1;
				//Debug.Log ("Max.Speed");
			}
			else 								//Increase Velocity
			{
				if (rb.velocity.z < -5.0f)
				{
					rb.AddForce (transform.forward * -50.0f);
					backRotaterSpeed = 1;
				}
				else if (rb.velocity.z < -3.0f)
				{
					rb.AddForce (transform.forward * -75.0f);
					backRotaterSpeed = 2;
				}
				else
				{
					rb.AddForce (transform.forward * -100.0f);
					backRotaterSpeed = 3;
				}
			}
		}
		else
		{
			backRotaterSpeed = 0;
		}
	}
}