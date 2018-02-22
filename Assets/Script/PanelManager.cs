using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour {

	// Use this for initialization

	public GameObject global_canvas;
	private List<string> element_panel_list = new List<string>(){"boat", "fish","bird"};
	private static List<GameObject> element_panels;

	private void init_panels(){
        element_panels = new List<GameObject>();
        foreach (string panel_name in element_panel_list)
		{
			string real_panel_name = panel_name + "_panel";
            GameObject currentPanel = global_canvas.transform.Find(real_panel_name).gameObject;
			element_panels.Add(currentPanel);
		}
	}
	public static void hide_all_panels(){
		foreach (GameObject obj in element_panels)
		{
			obj.SetActive(false);
		}
	}
	void Start () {
        init_panels();
        hide_all_panels();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
