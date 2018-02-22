using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fish_script : MonoBehaviour {

    public float fish_rotate_speed = 300.0f;
	private boat_script a_boat_script;
	public bool is_selected;
    public GameObject boat;

    private Texture2D original_texture;
    private Texture2D targetTexture;


    public GameObject global_event_handle;
    private global_event_script global_event_script;

    private float init_time;


	// Use this for initialization
	void Start () {

        is_selected = false;
        a_boat_script = (boat_script)boat.GetComponent(typeof(boat_script));
        global_event_script = global_event_handle.GetComponent<global_event_script>();

        original_texture = (Texture2D)gameObject.GetComponent<Renderer>().material.mainTexture;

        init_time = Time.time;



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
	
	

    public void change_to_selected_state(bool is_selected){

        this.is_selected = is_selected;

        if (is_selected){
            Renderer renderer = gameObject.GetComponent<Renderer>();

            

            renderer.material.mainTexture = targetTexture;

            

            targetTexture.Apply();
        }else{
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
        if ((!a_boat_script.is_selected) && (!is_selected))
        {
            Vector3 center = boat.transform.position;
            Vector3 sailing_direction_orthogonal = boat.transform.position;
            Vector3 sailing_direction = new Vector3(sailing_direction_orthogonal.z, 0, -sailing_direction_orthogonal.x);
            transform.RotateAround(center, sailing_direction, fish_rotate_speed * Time.deltaTime);

            // random vary
            Vector3 local_p_before = transform.localPosition;
            Vector3 p = transform.localPosition;
            Vector3 local_center = new Vector3(0,0,0);
            p = p + 10.0f * (transform.localPosition - local_center).normalized * Mathf.Sin(Time.time - init_time);
            Vector3 local_p_after = p;
            local_p_after.x = local_p_before.x;
            transform.localPosition = local_p_after;
        }
    }

}
