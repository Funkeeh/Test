using UnityEngine;
using System.Collections;

public class LocationTextureChecker : MonoBehaviour {
	public Texture[] textures;
	void Start () {
		GetComponent<Renderer>().material.mainTexture = textures[0];
	}

	void Update () {
		if (LocationSubmitButton.TextureId==0) {GetComponent<Renderer>().material.mainTexture = textures[0];}
		if (LocationSubmitButton.TextureId==1) {GetComponent<Renderer>().material.mainTexture = textures[1];}
		if (LocationSubmitButton.TextureId==2) {GetComponent<Renderer>().material.mainTexture = textures[2];}
		if (LocationSubmitButton.TextureId==3) {GetComponent<Renderer>().material.mainTexture = textures[3];}
	}
}
