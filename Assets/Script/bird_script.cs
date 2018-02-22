using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bird_script : MonoBehaviour
{

    public float bird_rotate_speed = 20.0f;
    public GameObject boat;
    private boat_script a_boat_script;

    public bool is_selected;



    private Texture2D original_texture;
    private Color original_color;

    public GameObject global_event_handle;
    private global_event_script global_event_script;


    // Use this for initialization
    void Start()
    {

        is_selected = false;
        a_boat_script = (boat_script)boat.GetComponent(typeof(boat_script));
        global_event_script = global_event_handle.GetComponent<global_event_script>();

        original_color = this.transform.Find("Seagull").gameObject.GetComponent<Renderer>().material.color;

    }

    public void set_rolling_angle(float a)
    {
        Vector3 local_rotation = this.transform.localRotation.eulerAngles;
        this.transform.localRotation = Quaternion.Euler(local_rotation.x, local_rotation.y, a / 5.0f);
    }

    public void update_rotate_speed(float speed)
    {
        bird_rotate_speed = speed;
    }


    public void change_to_selected_state(bool is_selected)
    {
        this.is_selected = is_selected;
        Renderer renderer = this.transform.Find("Seagull").gameObject.GetComponent<Renderer>();
        // Component[] Renderers = gameObject.GetComponentsInChildren<Renderer>();
        // Renderer renderer = (Renderer)Renderers[1];
        if (is_selected)
        {
            renderer.material.color = Color.red;
        }
        else
        {
            global_event_script.notify_unselected(gameObject.name);
            renderer.material.color = original_color;
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
        if ((!a_boat_script.is_selected) && (!is_selected))
        {

            // Vector3 location_diff = this.transform.right * 0.0f + transform.forward * 0.15f;

            Vector3 location_diff = this.transform.right * 0.0f + transform.forward * 0.05f;

            Vector3 world_bird_center = this.transform.position + location_diff;

           

            transform.RotateAround(world_bird_center, this.transform.up, 300.0f * Time.deltaTime);

            Debug.DrawLine(world_bird_center, world_bird_center + this.transform.up * 10000);

            Vector3 center = boat.transform.position;
            Vector3 down_axis = new Vector3(0, -1, 0);
            transform.RotateAround(center, down_axis, bird_rotate_speed * Time.deltaTime);

        }
    }
}
