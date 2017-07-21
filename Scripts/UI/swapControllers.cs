using UnityEngine;
using System.Collections;

public class swapControllers : MonoBehaviour
{
    private bool swap = false;

    public Transform footLeft           = null;
    public Transform legLeft            = null;
    public Transform footRight          = null;
    public Transform legRight           = null;

    public Transform footMLeft          = null;
    public Transform footMRight         = null;

    public static Transform activeFoot  = null;
    public static Transform activeLeg   = null;
    public static Transform activeFootControlRightFoot  = null;

    void Start()
    {
        activeFoot  = footLeft;
        activeFootControlRightFoot = footMLeft;
        activeLeg   = legRight;
    }


    void Update()
    {                                                                                                                                                                                                                                                                                                                                                                                                                                                        
        if (Input.GetKeyDown(KeyCode.F5))
        {
            swapControllerPositions();
        }
    }

    public void swapControllerPositions()
    {
        if (swap == true)
        {
            activeFoot  = footLeft;
            activeFootControlRightFoot = footMLeft;
            activeLeg   = legRight;
        }
        else
        {
            activeFoot = footRight;
            activeFootControlRightFoot = footMRight;
            activeLeg = legLeft;
        }

        //Swap
        if (swap == true)
        {
            swap = false;
        }
        else
        {
            swap = true;
        }
    }
}