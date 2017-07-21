using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addForceAtPos : MonoBehaviour 
{
	public GameObject chopper;
    private Rigidbody rb;
    public float force = 2000.0f;

    private void Start()
    {
        rb = chopper.GetComponent<Rigidbody>();
    }

	void OnTriggerEnter()
	{
        if (rb.velocity.y > -4.0f)
        {
            Debug.Log("CRASHED OMG!");
            chopper.GetComponent<Rigidbody>().AddForce(transform.up * force);

        }
        else
        {
            Debug.Log("SMALL CRASH NOT SO BAD");
            chopper.GetComponent<Rigidbody>().AddForce(transform.up * 2000.0f);
        }

	}
}