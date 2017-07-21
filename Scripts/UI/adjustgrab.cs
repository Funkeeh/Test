using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class adjustgrab : MonoBehaviour
{
    
    public Slider slider;
    string s;
    float f;

	void Start () {
        s = GetComponent<Text>().text;
	}
	
	void Update () {
        f = slider.value;
        s = Convert.ToString(f);
        GetComponent<Text>().text = s;
	}
}
