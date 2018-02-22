using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bird_slider_script : MonoBehaviour {

	public bird_script bird_script;
    public Slider bird_speed_slider;

    // Use this for initialization
	void Start () {
        bird_speed_slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
	}
	

	public void ValueChangeCheck() {
		Debug.Log(bird_speed_slider.value);
        bird_script.bird_rotate_speed = bird_speed_slider.value;
	}
	


	// Update is called once per frame
	void Update () {
		
	}
}
