using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class global_event_script : MonoBehaviour {

	public GameObject boat;
    public GameObject fish;
    public GameObject bird;

    public GameObject tower;

    public GameObject bottle1;
    public GameObject bottle2;

	public GameObject boat_panel;
	public GameObject fish_panel;
    public GameObject bird_panel;

    public GameObject tower_panel;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void set_boat_speed(float speed){
        boat_script a_boat_script = (boat_script) boat.GetComponent(typeof(boat_script));
        a_boat_script.rotate_speed = speed;
	}

    public void set_fish_speed(float speed)
    {
        fish_script a_fish_script = (fish_script) fish.GetComponent(typeof(fish_script));
        a_fish_script.fish_rotate_speed = speed;
    }

	public void notify_selected(string name){
		Debug.Log("handler received selected:" + name);

		string except_name = name;
        unselected_all(except_name);
		if (name == "boat"){
            boat_panel.SetActive(true);
		}
		if (name == "fish"){
            fish_panel.SetActive(true);
		}
        if (name == "bird"){
            bird_panel.SetActive(true);
        }

        if (name == "tower"){
            tower_panel.SetActive(true);
        }
	}

    public void notify_unselected(string name)
    {
        Debug.Log("handler received unselected:" + name);
		if (name == "boat"){
            boat_panel.SetActive(false);
		}
        if (name == "fish")
        {
            fish_panel.SetActive(false);
        }
        if (name == "bird")
        {
            bird_panel.SetActive(false);
        }

        if (name == "tower"){
            tower_panel.SetActive(false);
        }
    }

	private void unselected_all(string except_name){
        PanelManager.hide_all_panels();

		if (except_name != "fish"){
            ((fish_script)fish.GetComponent(typeof(fish_script))).change_to_selected_state(false);
		}

        if (except_name != "boat")
        {
            ((boat_script)boat.GetComponent(typeof(boat_script))).change_to_selected_state(false);
		}

        if (except_name != "bird")
        {
            ((bird_script)bird.GetComponent(typeof(bird_script))).change_to_selected_state(false);
        }

        if (except_name != "tower")
        {
            ((lighthouse_script)tower.GetComponent(typeof(lighthouse_script))).change_to_selected_state(false);
        }

        if (except_name != "bottle 1")
        {
            if (bottle1 != null){
                ((bottle_script)bottle1.GetComponent(typeof(bottle_script))).change_to_selected_state(false);
            }
            
        }

        if (except_name != "bottle 2")
        {
            if (bottle2 != null){
                ((bottle_script)bottle2.GetComponent(typeof(bottle_script))).change_to_selected_state(false);
            }
        }
	}
}
