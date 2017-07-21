using UnityEngine;
using System.Collections;

public class LocationCompleted : MonoBehaviour 
{
	public ParticleSystem	ps;
	public AudioSource		go;

	void Update () 
	{
		//Start Particle System if next Round Starts
		if (LocationManager.gameOver == true) 
		{
			if (!ps.isPlaying) 
			{
				ps.Play ();
			}

			go.Play ();
		} 
		else 
		{
			if (!ps.isPlaying) 
			{
				ps.Stop ();
			}
		}

	}
}
