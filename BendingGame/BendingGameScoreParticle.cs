using UnityEngine;
using System.Collections;

//Visualises the Round Score to the user via a particle system
//Chooses the correct particle system material to show user
//Activates/Deactivates the particle system
public class BendingGameScoreParticle : MonoBehaviour 
{
	public ParticleSystem	ps;
	public Material[] particleMats = new Material[21];

	void Start () 
	{
	
	}

	void Update () 
	{
		int chooseMat = (int)BendingGame.roundValue;
		//Debug.Log ("Gamescore material is: " + chooseMat);
		ps.GetComponent<ParticleSystemRenderer> ().material = particleMats [chooseMat];

		//Start Particle System if next Round Starts
		if (BendingGame.startNextRound) 
		{
			if (!ps.isPlaying) 
			{
				ps.Play ();
			}
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
