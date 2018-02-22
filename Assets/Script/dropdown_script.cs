using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dropdown_script : MonoBehaviour {

    // Use this for initialization
    public Dropdown dropdown;
	public Camera scene_cam;
    public Camera lighthouse_cam;
    public Camera boat_cam;

	void Start () {
		
	}
    public void change_camera()
    { 
        int cam_index = dropdown.value;

		Debug.Log("cam change to index:" + cam_index);

        scene_cam.enabled = false;
        lighthouse_cam.enabled = false;
        boat_cam.enabled = false;

		if (cam_index == 0){
			Debug.Log("change cam to scene");
			scene_cam.enabled = true;
		}
        if (cam_index == 1)
        {
            Debug.Log("change cam to lighthouse");
            lighthouse_cam.enabled = true;
        }
        if (cam_index == 2)
        {
            Debug.Log("change cam to boat");
            boat_cam.enabled = true;
        }

    }

	// Update is called once per frame
	void Update () {
		
	}
}
