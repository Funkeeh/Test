using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameStatus : MonoBehaviour
{
    public static int activeGame = 0;
    //0 = avalab / no game active, 1 = shape game, 2 = catapult game, 3 = anti-symmetrical game

    public GameObject avalab;
    public GameObject shapeGame; 
    public GameObject catapultGame;
    public GameObject catapult;
    //public GameObject antisymmetric

    public GameObject maleA;
    public GameObject FemaleA;

    //Avatar foot colliders
    public GameObject shapeGameCol;
    public GameObject catapultGameCol;

    //Couch GO
    public GameObject couch;
    public GameObject couchback;
    public GameObject couchLie;
    public GameObject couchbackLie;

    private string[] gameNames = { "No Game", "Shape Game", "Catapult Game", "Anti-Symmetrical Game", "Default - No Game" };
    public void Start()
    {
        //Only avalab active
        //avalab.SetActive(true);
        //catapultGame.SetActive(false);
        //shapeGame.GetComponent<manager>().enabled = false;

        //shapeGameCol.SetActive(false);
        //catapultGameCol.SetActive(false);
    }


    public void setGameStatus(int rnd)
    {
        //int setStatus = System.Int32.Parse(rnd);
        activeGame = rnd;

        Debug.Log("Active Game is " + gameNames[activeGame]);

        switch ( activeGame )
        {
            case 0:
                // avalab.SetActive(true);
                // catapultGame.SetActive(false);
                // shapeGame.GetComponent<manager>().enabled = false;
                // maleA.GetComponent<MainColliderPS>().enabled = false;
                // FemaleA.GetComponent<MainColliderPS>().enabled = false;

                // shapeGameCol.SetActive(false);
                // catapultGameCol.SetActive(false);
                break;
            case 1:
                // avalab.SetActive(false);
                // catapultGame.SetActive(true);
                // catapult.SetActive(false);
                // shapeGame.GetComponent<manager>().enabled = true;
                // maleA.GetComponent<MainColliderPS>().enabled = true;
                // FemaleA.GetComponent<MainColliderPS>().enabled = true;

                // shapeGameCol.SetActive(true);
                // catapultGameCol.SetActive(false);

                // if (IKavatar.sittingVSlieDown == true)
                // {
                //     couch.SetActive(true);
                //     couchback.SetActive(true);
                //     couchLie.SetActive(false);
                //     couchbackLie.SetActive(false);
                // }
                // else
                // {
                //     couch.SetActive(false);
                //     couchback.SetActive(false);
                //     couchLie.SetActive(true);
                //     couchbackLie.SetActive(true);
                // }
                break;
            case 2:
                // avalab.SetActive(false);
                // catapultGame.SetActive(true);
                // catapult.SetActive(true);
                // shapeGame.GetComponent<manager>().enabled = false;
                // maleA.GetComponent<MainColliderPS>().enabled = false;
                // FemaleA.GetComponent<MainColliderPS>().enabled = false;

                // shapeGameCol.SetActive(false);
                // catapultGameCol.SetActive(true);

                // if (IKavatar.sittingVSlieDown == true)
                // {
                //     catapult.transform.localPosition = new Vector3( 0.0f, -0.2f, 1.4f );
                //     couch.SetActive(true);
                //     couchback.SetActive(true);
                //     couchLie.SetActive(false);
                //     couchbackLie.SetActive(false);
                // }
                // else
                // {
                //     catapult.transform.localPosition = new Vector3( 0.0f, -0.2f, 1.75f);
                //     couch.SetActive(false);
                //     couchback.SetActive(false);
                //     couchLie.SetActive(true);
                //     couchbackLie.SetActive(true);
                // }

                break;
            case 3:
                // avalab.SetActive(false);
                // catapultGame.SetActive(false);
                // shapeGame.GetComponent<manager>().enabled = false;
                // maleA.GetComponent<MainColliderPS>().enabled = false;
                // FemaleA.GetComponent<MainColliderPS>().enabled = false;

                // shapeGameCol.SetActive(false);
                // catapultGameCol.SetActive(false);
                break;
            default:
                // avalab.SetActive(true);
                // catapultGame.SetActive(false);
                // shapeGame.GetComponent<manager>().enabled = false;
                // maleA.GetComponent<MainColliderPS>().enabled = false;
                // FemaleA.GetComponent<MainColliderPS>().enabled = false;

                // shapeGameCol.SetActive(false);
                // catapultGameCol.SetActive(false);
                break;
        }
    }
}