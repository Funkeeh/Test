using UnityEngine;
using System.Collections;

public class LocationTextureCheckerStimulateButton : MonoBehaviour {
	public Texture[] textures;
	void Start () {
		GetComponent<Renderer>().material.mainTexture = textures[0];
	}

	void Update () {
		if (LocationManager.TimeToSense==true) {GetComponent<Renderer>().material.mainTexture = textures[0];}
		if (LocationManager.TimeToSense==false) {GetComponent<Renderer>().material.mainTexture = textures[1];}
	}
}
