using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class adjustgrab : MonoBehaviour {
    
    public Slider slider;
    string s;
    float f;

	// Use this for initialization
	void Start () {
        s = GetComponent<Text>().text;
	}
	
	// Update is called once per frame
	void Update () {
        f = slider.value;
        s = Convert.ToString(f);
        GetComponent<Text>().text = s;
	}
}
