using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cakeLight : MonoBehaviour 
{
	public static int activeCakeLight = 0;
	public Material[] materials;
	public Light light;
	public AudioSource click;
	private bool playOnce = true;

	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.Q)) 
		{
			if (activeCakeLight == 5) 
			{
				activeCakeLight = 0;
			} 
			else 
			{
				activeCakeLight = activeCakeLight + 1;
			}
		}

		//Ignite light Source
		if (activeCakeLight == 5) 
		{
            if (light.intensity < 8.0f)
            {
                light.intensity = light.intensity + (Time.deltaTime * 50.0f);

                if (playOnce == true)
                {
                    if (!click.isPlaying)
                    {
                        click.Play();
                    }
                    playOnce = false;
                }
            }
        } 
		else 
		{
			light.intensity = 0.0f;
			playOnce = true;
		}

		GetComponent<Renderer> ().material = materials [activeCakeLight];
	}
}