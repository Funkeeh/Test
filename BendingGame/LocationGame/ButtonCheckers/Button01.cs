using UnityEngine;
using System.Collections;

public class Button01 : MonoBehaviour 
{
	public Collider[] 	buttons 		= new Collider[6];
	public int[] currentValue 	= new int[6];
	public static int 	TotalValue 		= 0;

	public static bool[] alreadyPressed = {false, false, false, false, false, false};

	void Start () 
	{
	
	}

	void Update () 
	{
		//Debug.Log (LocationBigCollider.thereCanBeOnlyLocation);
	}

	void OnTriggerEnter (Collider other)
	{
		int i = 0;
		for (int j = i; j <= 5; j++) 
		{
			if (other == buttons [i]) 
			{
				if (alreadyPressed [i] == false) 
				{
					currentValue[i] = 1;
					alreadyPressed [i] = true;
				}
				else if (alreadyPressed [i] == true) 
				{
					currentValue[i] = 0;
					alreadyPressed [i] = false;
				}
			}
			i++;
		}

	}
}
