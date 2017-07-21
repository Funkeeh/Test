using UnityEngine;
using System.Collections;

public class TrainingLocationHands : MonoBehaviour 
{
	//Ones
	public Material index;	
	public Material middle;
	public Material ring;
	public Material pinky;
	public Material thumb;
	public Material palm;

	//Twos
	public Material[] twoFingers = new Material[15];

	//Others
	public Material none;
	public Material timeToSense;
	public Material currentlyPressing;

	void Update () 
	{
		if (TrainingLocationManager.sendStimulation == true) 
		{
			if (TrainingLocationManager.currentlyStim == true) 
			{
				GetComponent<Renderer> ().material = currentlyPressing;
			} 
			else 
			{
				GetComponent<Renderer> ().material = timeToSense;
			}
		} 
		else if (TrainingLocationManager.visualiseButtons == true) 
		{
			//Level 02
			if (TrainingLocationManager.level == 2) 
			{
				//Two Textures
				if( TrainingLocationManager.lastStimuValue == 1 && TrainingLocationManager.secondStimValue == 2 || TrainingLocationManager.lastStimuValue == 2 && TrainingLocationManager.secondStimValue == 1 )
				{
					GetComponent<Renderer> ().material = twoFingers [0];
				}
				else if( TrainingLocationManager.lastStimuValue == 1 && TrainingLocationManager.secondStimValue == 3 || TrainingLocationManager.lastStimuValue == 3 && TrainingLocationManager.secondStimValue == 1 )
				{
					GetComponent<Renderer> ().material = twoFingers [1];
				}
				else if( TrainingLocationManager.lastStimuValue == 1 && TrainingLocationManager.secondStimValue == 4 || TrainingLocationManager.lastStimuValue == 4 && TrainingLocationManager.secondStimValue == 1 )
				{
					GetComponent<Renderer> ().material = twoFingers [2];
				}
				else if( TrainingLocationManager.lastStimuValue == 1 && TrainingLocationManager.secondStimValue == 5 || TrainingLocationManager.lastStimuValue == 5 && TrainingLocationManager.secondStimValue == 1 )
				{
					GetComponent<Renderer> ().material = twoFingers [3];
				}
				else if( TrainingLocationManager.lastStimuValue == 1 && TrainingLocationManager.secondStimValue == 6 || TrainingLocationManager.lastStimuValue == 6 && TrainingLocationManager.secondStimValue == 1 )
				{
					GetComponent<Renderer> ().material = twoFingers [4];
				}
				else if( TrainingLocationManager.lastStimuValue == 2 && TrainingLocationManager.secondStimValue == 3 || TrainingLocationManager.lastStimuValue == 3 && TrainingLocationManager.secondStimValue == 2 )
				{
					GetComponent<Renderer> ().material = twoFingers [5];
				}
				else if( TrainingLocationManager.lastStimuValue == 2 && TrainingLocationManager.secondStimValue == 4 || TrainingLocationManager.lastStimuValue == 4 && TrainingLocationManager.secondStimValue == 2 )
				{
					GetComponent<Renderer> ().material = twoFingers [6];
				}
				else if( TrainingLocationManager.lastStimuValue == 2 && TrainingLocationManager.secondStimValue == 5 || TrainingLocationManager.lastStimuValue == 5 && TrainingLocationManager.secondStimValue == 2 )
				{
					GetComponent<Renderer> ().material = twoFingers [7];
				}
				else if( TrainingLocationManager.lastStimuValue == 2 && TrainingLocationManager.secondStimValue == 6 || TrainingLocationManager.lastStimuValue == 6 && TrainingLocationManager.secondStimValue == 2 )
				{
					GetComponent<Renderer> ().material = twoFingers [8];
				}
				else if( TrainingLocationManager.lastStimuValue == 3 && TrainingLocationManager.secondStimValue == 4 || TrainingLocationManager.lastStimuValue == 4 && TrainingLocationManager.secondStimValue == 3 )
				{
					GetComponent<Renderer> ().material = twoFingers [9];
				}
				else if( TrainingLocationManager.lastStimuValue == 3 && TrainingLocationManager.secondStimValue == 5 || TrainingLocationManager.lastStimuValue == 5 && TrainingLocationManager.secondStimValue == 3 )
				{
					GetComponent<Renderer> ().material = twoFingers [10];
				}
				else if( TrainingLocationManager.lastStimuValue == 3 && TrainingLocationManager.secondStimValue == 6 || TrainingLocationManager.lastStimuValue == 6 && TrainingLocationManager.secondStimValue == 3 )
				{
					GetComponent<Renderer> ().material = twoFingers [11];
				}
				else if( TrainingLocationManager.lastStimuValue == 4 && TrainingLocationManager.secondStimValue == 5 || TrainingLocationManager.lastStimuValue == 5 && TrainingLocationManager.secondStimValue == 4 )
				{
					GetComponent<Renderer> ().material = twoFingers [12];
				}
				else if( TrainingLocationManager.lastStimuValue == 4 && TrainingLocationManager.secondStimValue == 6 || TrainingLocationManager.lastStimuValue == 6 && TrainingLocationManager.secondStimValue == 4 )
				{
					GetComponent<Renderer> ().material = twoFingers [13];
				}
				else if( TrainingLocationManager.lastStimuValue == 5 && TrainingLocationManager.secondStimValue == 6 || TrainingLocationManager.lastStimuValue == 6 && TrainingLocationManager.secondStimValue == 5 )
				{
					GetComponent<Renderer> ().material = twoFingers [14];
				}
				else
				{
					if ( TrainingLocationManager.lastStimuValue == 1 || TrainingLocationManager.secondStimValue == 1 ) 
					{
						GetComponent<Renderer> ().material = thumb;
					} 
					else if ( TrainingLocationManager.lastStimuValue == 2 || TrainingLocationManager.secondStimValue == 2 ) 
					{
						GetComponent<Renderer> ().material = index;
					} 
					else if ( TrainingLocationManager.lastStimuValue == 3 || TrainingLocationManager.secondStimValue == 3 ) 
					{
						GetComponent<Renderer> ().material = middle;					
					} 
					else if ( TrainingLocationManager.lastStimuValue == 4 || TrainingLocationManager.secondStimValue == 4 ) 
					{
						GetComponent<Renderer> ().material = ring;
					} 
					else if ( TrainingLocationManager.lastStimuValue == 5 || TrainingLocationManager.secondStimValue == 5 ) 
					{
						GetComponent<Renderer> ().material = pinky;
					} 
					else if ( TrainingLocationManager.lastStimuValue == 6 || TrainingLocationManager.secondStimValue == 6 ) 
					{
						GetComponent<Renderer> ().material = palm;
					} 
					else 
					{
						GetComponent<Renderer> ().material = none;
					}
				}
			}
			//Level 01
			if (TrainingLocationManager.level == 1) 
			{
				if ( TrainingLocationManager.lastStimuValue == 0 ) 
				{
					GetComponent<Renderer> ().material = none;
				} 
				else if ( TrainingLocationManager.lastStimuValue == 1 ) 
				{
					GetComponent<Renderer> ().material = thumb;
				} 
				else if ( TrainingLocationManager.lastStimuValue == 2 ) 
				{
					GetComponent<Renderer> ().material = index;
				} 
				else if ( TrainingLocationManager.lastStimuValue == 3 ) 
				{
					GetComponent<Renderer> ().material = middle;
				} 
				else if ( TrainingLocationManager.lastStimuValue == 4 ) 
				{
					GetComponent<Renderer> ().material = ring;
				} 
				else if ( TrainingLocationManager.lastStimuValue == 5 ) 
				{
					GetComponent<Renderer> ().material = pinky;
				} 
				else if ( TrainingLocationManager.lastStimuValue == 6 ) 
				{
					GetComponent<Renderer> ().material = palm;
				} 
				else 
				{
					GetComponent<Renderer> ().material = none;
				}
			}
		}
		else
		{
			GetComponent<Renderer> ().material = none;
		}
	
	}
}
