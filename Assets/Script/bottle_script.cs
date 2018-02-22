using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bottle_script : MonoBehaviour {

	// Use this for initialization
	public float min_y = 0.016f;
	public float max_y = 0.101f;
	public float floating_speed;

    public GameObject explosionPrefab;

	private float floating_gap;


    private Color initialColor;
    private Color selectedColor;

	Texture2D original_texture;
    Texture2D targetTexture;

    public GameObject global_event_handle;
    private global_event_script global_event_script;

	private bool is_selected;
	void Start () {
        floating_gap = (max_y - min_y) / 2;
        is_selected = false;

        original_texture = (Texture2D)gameObject.GetComponent<Renderer>().material.mainTexture;
        global_event_script = global_event_handle.GetComponent<global_event_script>();


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

	void toggle_select_state(){
		is_selected = !is_selected;
		if (is_selected){
            Renderer renderer = gameObject.GetComponent<Renderer>();
            renderer.material.mainTexture = targetTexture;
            targetTexture.Apply();
			Debug.Log("change to selected.");
		}else{

            is_log_init_position = false;

            Renderer renderer = gameObject.GetComponent<Renderer>();
            renderer.material.mainTexture = original_texture;
            targetTexture.Apply();
            Debug.Log("change to unselected.");
		}
	}

    private Vector3 screenPoint;
    private Vector3 offset;
	
	private bool is_log_init_position = false;
	private Vector3 init_position;


	public void change_to_selected_state(bool target_selected){
		if (is_selected != target_selected){
            toggle_select_state();
		}
	}
    void OnMouseDown()
    {
        toggle_select_state();
        Debug.Log("change to: " + is_selected);
        // if (is_selected)
        // {
        //     // Debug.Log("selected name: " + gameObject.name);
        //     global_event_script.notify_selected(gameObject.name);
        //     Renderer renderer = gameObject.GetComponent<Renderer>();
            
        //     renderer.material.mainTexture = targetTexture;

        //     targetTexture.Apply();
        // }
        // else
        // {
        //     global_event_script.notify_unselected(gameObject.name);
        //     Renderer renderer = gameObject.GetComponent<Renderer>();
        //     renderer.material.mainTexture = original_texture;
        // }
    }



	void OnMouseUp()
	{
        if (is_selected){
            toggle_select_state();
            Debug.Log("change to: " + is_selected);
        }
        // if (is_selected)
        // {
        //     // Debug.Log("selected name: " + gameObject.name);
        //     global_event_script.notify_selected(gameObject.name);
        //     Renderer renderer = gameObject.GetComponent<Renderer>();

        //     renderer.material.mainTexture = targetTexture;

        //     targetTexture.Apply();
        // }
        // else
        // {
        //     global_event_script.notify_unselected(gameObject.name);
        //     Renderer renderer = gameObject.GetComponent<Renderer>();
        //     renderer.material.mainTexture = original_texture;
        // }
	}

    void OnTriggerEnter(Collider collider)
    {
        print("bottle: " + "Detected collision between " + gameObject.name + " and " + collider.name);

        if (collider.name == "boat"){
            GameObject explosion = (GameObject)Instantiate(explosionPrefab, transform.position, explosionPrefab.transform.rotation);
            Destroy(explosion, explosion.GetComponent<ParticleSystem>().main.startLifetimeMultiplier);

            Destroy(this.gameObject);
        }
        
    }

    void OnTriggerStay(Collider collider)
    {
        // print("bottle: " + gameObject.name + " and " + collider.name + " are still colliding");
    }

    void OnTriggerExit(Collider collider)
    {
        print("bottle: " + gameObject.name + " and " + collider.name + " are no longer colliding");
    }


	void OnMouseDrag()
	{
		if (is_selected){

			if (!is_log_init_position){
                is_log_init_position = true;
                init_position = transform.position;
			}

            Plane plane = new Plane(Vector3.up, transform.position);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float distance;
            if (plane.Raycast(ray, out distance))
            {
                transform.position = ray.GetPoint(distance);

				if (Mathf.Sqrt(transform.position.x * transform.position.x + transform.position.z * transform.position.z)<=3.5){
					// collide wit island
					transform.position = init_position;
                    toggle_select_state();
				}else{
                    if ( Mathf.Abs(transform.position.x) > 25.0f || Mathf.Abs(transform.position.z) > 25.0f)
                    {
                        // collide wit island
                        transform.position = init_position;
                        toggle_select_state();
                    }
                }
            }
		}
        
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 p = transform.position;
		p.y = min_y + floating_gap * Mathf.Sin(Time.time * floating_speed);
		transform.position = p;
	}
}
