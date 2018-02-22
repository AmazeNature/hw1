using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectable_special_script : MonoBehaviour {


    private bool is_selected;
    private Texture2D original_texture;
    // Use this for initialization
    void Start()
    {
        is_selected = false;
        original_texture = (Texture2D)gameObject.GetComponent<Renderer>().material.mainTexture;
    }
    void OnMouseDown()
    {
        is_selected = !is_selected;
        Debug.Log("change to: " + is_selected);
        if (is_selected)
        {
            Renderer renderer = gameObject.GetComponentInChildren<Renderer>();
            Texture2D targetTexture = new Texture2D(original_texture.width, original_texture.height);
            renderer.material.mainTexture = targetTexture;


            for (int y = 0; y < original_texture.height; y++)
            {
                for (int x = 0; x < original_texture.width; x++)
                {
                    Color c = Color.red;
                    targetTexture.SetPixel(x, y, c);
                }
            }
            targetTexture.Apply();
        }
        else
        {
            Renderer renderer = gameObject.GetComponentInChildren<Renderer>();
            renderer.material.mainTexture = original_texture;
        }
    }



    // Update is called once per frame
    void Update()
    {

    }
}
