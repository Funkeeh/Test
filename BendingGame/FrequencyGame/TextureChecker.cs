using UnityEngine;
using System.Collections;

public class TextureChecker : MonoBehaviour {
	public Texture[] textures;
	void Start () {
		GetComponent<Renderer>().material.mainTexture = textures[0];
	}
	
	void Update () {
		if (SubmitButton.TextureId==0) {GetComponent<Renderer>().material.mainTexture = textures[0];}
		if (SubmitButton.TextureId==1) {GetComponent<Renderer>().material.mainTexture = textures[1];}
		if (SubmitButton.TextureId==2) {GetComponent<Renderer>().material.mainTexture = textures[2];}
		if (SubmitButton.TextureId==3) {GetComponent<Renderer>().material.mainTexture = textures[3];}
	}
}
