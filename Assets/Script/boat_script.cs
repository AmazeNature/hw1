using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boat_script : MonoBehaviour {

	public GameObject island;
	public float rotate_speed;
	// Use this for initialization

    public GameObject global_event_handle;
    private global_event_script global_event_script;
    public bool is_selected;
    private Texture2D original_texture;
    private Texture2D targetTexture;
    // Use this for initialization
    void Start()
    {
        is_selected = false;
        original_texture = (Texture2D)gameObject.GetComponent<Renderer>().material.mainTexture;
        global_event_script = global_event_handle.GetComponent<global_event_script>();
        rotate_speed = 5.0f;



        targetTexture = new Texture2D(original_texture.width, original_texture.height);
        for (int y = 0; y < original_texture.height; y++)
        {
            for (int x = 0; x < original_texture.width; x++)
            {
                Color c = Color.red;
                targetTexture.SetPixel(x, y, c);
            }
        }
    }


    public void set_rolling_angle(float a){
        Vector3 local_rotation = this.transform.localRotation.eulerAngles;
        this.transform.localRotation = Quaternion.Euler(a/5.0f, local_rotation.y, local_rotation.z);
    }

    public void change_to_selected_state(bool input_is_selected)
    {
        is_selected = input_is_selected;
        if (is_selected)
        {
            Renderer renderer = gameObject.GetComponent<Renderer>();

            

            renderer.material.mainTexture = targetTexture;


            targetTexture.Apply();
        }
        else
        {
            global_event_script.notify_unselected(gameObject.name);
            Renderer renderer = gameObject.GetComponent<Renderer>();
            renderer.material.mainTexture = original_texture;
        }
    }
    void OnMouseDown()
    {
        is_selected = !is_selected;
        change_to_selected_state(is_selected);

        if (is_selected)
        {
            global_event_script.notify_selected(gameObject.name);
        }

    }



    // Update is called once per frame
    void Update()
    {
		if (!is_selected){
            Vector3 center = island.transform.position;
            Vector3 down_axis = -island.transform.up;
            transform.RotateAround(center, down_axis, rotate_speed * Time.deltaTime);
		}
        
    }
}
