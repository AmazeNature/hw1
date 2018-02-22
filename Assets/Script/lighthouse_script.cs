using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lighthouse_script : MonoBehaviour {


    public float light_rotate_speed = 300.0f;

    public bool is_selected;

    private Texture2D original_texture;


    public GameObject global_event_handle;
    private global_event_script global_event_script;

	public GameObject spotlight;

    private Texture2D targetTexture;

    private Shader shader;
    private Material tower_material;

    private Color original_color;
    private Color target_color;

    void Start()
    {

        is_selected = false;
        global_event_script = global_event_handle.GetComponent<global_event_script>();
        

        tower_material = this.gameObject.GetComponent<Renderer>().material;
        shader = tower_material.shader;


        original_color = tower_material.GetColor("_Color");
        target_color = Color.red;
        


    }

    public void update_light_rotate_speed(float speed)
    {
        light_rotate_speed = speed;
    }

    public void change_to_selected_state(bool is_selected)
    {
        this.is_selected = is_selected;
        if (is_selected)
        {
            tower_material.SetColor("_Color", target_color);
        }
        else
        {
            global_event_script.notify_unselected(gameObject.name);
            tower_material.SetColor("_Color", original_color);
        }
    }

    void OnMouseDown()
    {
        Debug.Log("light house click");
        is_selected = !is_selected;
        change_to_selected_state(is_selected);

        if (is_selected)
        {
            global_event_script.notify_selected(gameObject.name);
        }

    }
	
	// Update is called once per frame
	void Update () {
		if (!is_selected){
            // update light
            spotlight.transform.Rotate(0, -Time.deltaTime * light_rotate_speed, 0, Space.World);
		}
	}
}
