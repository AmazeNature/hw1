using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lighthouse_cam_script : MonoBehaviour {


    public GameObject boat;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // transform.position = boat_diff + boat.transform.position;
        transform.LookAt(boat.transform);
    }
}
