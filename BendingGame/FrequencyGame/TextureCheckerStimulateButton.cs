using UnityEngine;
using System.Collections;

public class TextureCheckerStimulateButton : MonoBehaviour {
	public Texture[] textures;
	void Start () {
		GetComponent<Renderer>().material.mainTexture = textures[0];
	}

	void Update () {
		if (Manager.TimeToSense==true) {GetComponent<Renderer>().material.mainTexture = textures[0];}
		if (Manager.TimeToSense==false) {GetComponent<Renderer>().material.mainTexture = textures[1];}
	}
}
