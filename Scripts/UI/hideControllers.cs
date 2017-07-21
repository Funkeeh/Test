using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class hideControllers : MonoBehaviour 
{
	private bool hidden = false;
	//private int once = 0;

	public GameObject controllerVive01;
    public GameObject controllerVive02;

    void Start () 
	{
        //Nothing
	}

	void Update ()
	{
		if (Input.GetKeyDown ( KeyCode.F3 )) 
		{
			SetControllerVisible (controllerVive01, hidden);
            SetControllerVisible (controllerVive02, hidden);

            if (hidden) 
			{
				hidden = false;
			} 
			else 
			{
				hidden = true;
			}
		}
	}

	//Src:  answers.unity3d.com/questions/1177557/hiding-steamvr-vive-controller-models.html
	void SetControllerVisible(GameObject controller, bool visible)
	{
		foreach (SteamVR_RenderModel model in controller.GetComponentsInChildren<SteamVR_RenderModel>())
		{
			foreach (var child in model.GetComponentsInChildren<MeshRenderer>())
				child.enabled = visible;
		}
	}

    //FOR UI
    public void toggleControllers( )
    {
        SetControllerVisible(controllerVive01, hidden);
        SetControllerVisible(controllerVive02, hidden);

        if (hidden)
        {
            hidden = false;
        }
        else
        {
            hidden = true;
        }
    }
}
