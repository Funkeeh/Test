using UnityEngine;
using System.Collections;

public class BendingGameTextureChecker : MonoBehaviour 
{
	public Material[] textures;

	void Start () 
	{
		GetComponent<Renderer>().material = textures[0];
	}

	void Update () 
	{
		if (BendingGameButtonActivator.TextureIdBG == 0 ) {GetComponent<Renderer>().material = textures[0];}
		if (BendingGameButtonActivator.TextureIdBG == 1 ) {GetComponent<Renderer>().material = textures[1];}
		if (BendingGameButtonActivator.TextureIdBG == 2 ) {GetComponent<Renderer>().material = textures[2];}
		if (BendingGame.gameOver == true ) 				  {GetComponent<Renderer>().material = textures[3];}
	}
}
