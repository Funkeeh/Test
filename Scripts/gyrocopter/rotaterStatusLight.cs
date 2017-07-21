using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Manage status light in front panel
public class rotaterStatusLight : MonoBehaviour 
{
	public GameObject motorVersion;

	public Texture standard;
	public Texture swinging;
	public Texture cycle;

	void Update () 
	{
		if (startPoint.hitStartPoint == false) 
		{
			GetComponent<Light> ().enabled = false;
		} 
		else 
		{
			GetComponent<Light> ().enabled = true;
		}

		//Mirror versions
		if (swapLegControls.legMirrorVersions == 0)				//Standard Mirror
		{
			motorVersion.GetComponent<Renderer> ().material.SetTexture ("_MainTex", standard);
		}
		else if (swapLegControls.legMirrorVersions == 1)		//"Swinging" Version
		{
			motorVersion.GetComponent<Renderer> ().material.SetTexture ("_MainTex", swinging);
		}
		else 													//"Cycling" Version
		{
			motorVersion.GetComponent<Renderer> ().material.SetTexture ("_MainTex", cycle);
		}
	}
}