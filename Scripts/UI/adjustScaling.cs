using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class adjustScaling : MonoBehaviour
{
    public GameObject avatarMale;
    public GameObject avatarFemale;
    private GameObject avatar;
    //private float adjustment = 0.0f;

    public void AdjustScale(float number)
    {
        int useInCase = (int)number;

        switch (useInCase)
        {
            case 10:
                avatarMale.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                avatarFemale.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                break;
            case 9:
                avatarMale.transform.localScale = new Vector3(1.18f, 1.18f, 1.18f);
                avatarFemale.transform.localScale = new Vector3(1.18f, 1.18f, 1.18f);
                break;
            case 8:
                avatarMale.transform.localScale = new Vector3(1.16f, 1.16f, 1.16f);
                avatarFemale.transform.localScale = new Vector3(1.16f, 1.16f, 1.16f);
                break;
            case 7:
                avatarMale.transform.localScale = new Vector3(1.14f, 1.14f, 1.14f);
                avatarFemale.transform.localScale = new Vector3(1.14f, 1.14f, 1.14f);
                break;
            case 6:
                avatarMale.transform.localScale = new Vector3(1.12f, 1.12f, 1.12f);
                avatarFemale.transform.localScale = new Vector3(1.12f, 1.12f, 1.12f);
                break;
            case 5:
                avatarMale.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
                avatarFemale.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
                break;
            case 4:
                avatarMale.transform.localScale = new Vector3(1.08f, 1.08f, 1.08f);
                avatarFemale.transform.localScale = new Vector3(1.08f, 1.08f, 1.08f);
                break;
            case 3:
                avatarMale.transform.localScale = new Vector3(1.06f, 1.06f, 1.06f);
                avatarFemale.transform.localScale = new Vector3(1.06f, 1.06f, 1.06f);
                break;
            case 2:
                avatarMale.transform.localScale = new Vector3(1.04f, 1.04f, 1.04f);
                avatarFemale.transform.localScale = new Vector3(1.04f, 1.04f, 1.04f);
                break;
            case 1:
                avatarMale.transform.localScale = new Vector3(1.02f, 1.02f, 1.02f);
                avatarFemale.transform.localScale = new Vector3(1.02f, 1.02f, 1.02f);
                break;
            case 0:
                avatarMale.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                avatarFemale.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                break;
            case -1:
                avatarMale.transform.localScale = new Vector3(0.98f, 0.98f, 0.98f);
                avatarFemale.transform.localScale = new Vector3(0.98f, 0.98f, 0.98f);
                break;
            case -2:
                avatarMale.transform.localScale = new Vector3(0.96f, 0.96f, 0.96f);
                avatarFemale.transform.localScale = new Vector3(0.96f, 0.96f, 0.96f);
                break;
            case -3:
                avatarMale.transform.localScale = new Vector3(0.94f, 0.94f, 0.94f);
                avatarFemale.transform.localScale = new Vector3(0.94f, 0.94f, 0.94f);
                break;
            case -4:
                avatarMale.transform.localScale = new Vector3(0.92f, 0.92f, 0.92f);
                avatarFemale.transform.localScale = new Vector3(0.92f, 0.92f, 0.92f);
                break;
            case -5:
                avatarMale.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
                avatarFemale.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
                break;
            case -6:
                avatarMale.transform.localScale = new Vector3(0.88f, 0.88f, 0.88f);
                avatarFemale.transform.localScale = new Vector3(0.88f, 0.88f, 0.88f);
                break;
            case -7:
                avatarMale.transform.localScale = new Vector3(0.86f, 0.86f, 0.86f);
                avatarFemale.transform.localScale = new Vector3(0.86f, 0.86f, 0.86f);
                break;
            case -8:
                avatarMale.transform.localScale = new Vector3(0.84f, 0.84f, 0.84f);
                avatarFemale.transform.localScale = new Vector3(0.84f, 0.84f, 0.84f);
                break;
            case -9:
                avatarMale.transform.localScale = new Vector3(0.82f, 0.82f, 0.82f);
                avatarFemale.transform.localScale = new Vector3(0.82f, 0.82f, 0.82f);
                break;
            case -10:
                avatarMale.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                avatarFemale.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                break;
        }
    }
}
