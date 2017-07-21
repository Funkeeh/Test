using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveAvatar : MonoBehaviour
{
    public Transform seatingPos;
    private float move = 0.0f;

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            move = move - 0.01f;
            seatingPos.position = new Vector3(move, seatingPos.position.y, seatingPos.position.z); 
            //Debug.Log ("Moving Left");
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            move = move + 0.01f;
            seatingPos.position = new Vector3(move, seatingPos.position.y, seatingPos.position.z);
            //Debug.Log("Moving Right");
        }
    }

    //FOR UI
    public void MoveSeatingPos(float dist)
    {
       //move = dist * -1.0f;
        seatingPos.position = new Vector3(dist * -1.0f, seatingPos.position.y, seatingPos.position.z);
    }
}
