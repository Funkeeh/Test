using UnityEngine;
using System.Collections;

public class LegRotation : MonoBehaviour {
    public GameObject controller;
    public GameObject calibrationPose;
    public GameObject leg;

    private Vector3 boneWorld;
    private Vector3 boneCalibration;
    private Vector3 boneProjectedXZ;
    private float yaw;
    private float pitch;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        boneWorld = controller.transform.TransformDirection(Vector3.forward);
        boneCalibration = calibrationPose.transform.InverseTransformDirection(boneWorld);
        boneProjectedXZ = (new Vector3(boneCalibration.x, 0.0f, boneCalibration.z)).normalized;

        if (Vector3.Angle(Vector3.right, boneProjectedXZ) < 90.0f)
        {
            yaw = Vector3.Angle(Vector3.forward, boneProjectedXZ);
        }
        else {
            yaw = -Vector3.Angle(Vector3.forward, boneProjectedXZ);
        }
        if (Vector3.Angle(Vector3.up, boneCalibration) < 90.0f)
        {
            pitch = Vector3.Angle(boneCalibration, boneProjectedXZ);
        }
        else
        {
            pitch = -Vector3.Angle(boneCalibration, boneProjectedXZ);
        }

        leg.transform.rotation = calibrationPose.transform.rotation * Quaternion.Euler(-pitch, yaw, 0.0f);
    }
}
