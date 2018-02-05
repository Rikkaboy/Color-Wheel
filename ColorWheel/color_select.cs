// Color Selection Script
// Created by Sam Moore
// Last Updated: 2/02/2018

// Selects object in front of player, then opens Color Wheel GUI for color selection
// Controls: Left click to select color and N to cancel
// Triggered from Radial Menu (CapsLock)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class color_select : Crosshair {
    public Texture2D colorPicker;
    public GameObject Sample;
    public Rect colorPanel;
    public Camera mainCamera;


    //public bool SelectMode;
    public bool SelectMode;
    public bool ColorMode;
    public GameObject SelectedObj;
    public Color col;
    private Vector2 pickpos;

    private MouseLook LookerX;
    private MouseLook LookerY;

    // Use this for initialization
    void Start () {
        SelectMode = false;
        ColorMode = false;
        SelectedObj = null;
        gameObject.SetActive(false);
        LookerX = GameObject.FindGameObjectWithTag("ADAM").GetComponent<MouseLook>();
        LookerY = mainCamera.GetComponent<MouseLook>();
        //center color panel on screen, above inventory hotbar
        colorPanel.x = Screen.width/2 - colorPanel.width/2;
        colorPanel.y = Screen.height/2 - colorPanel.height/2 - Screen.height/40; //Screen.height/40 is arbitrarily chosen to raise the panel a bit
        float y = Sample.GetComponent<RectTransform>().anchoredPosition.y + Screen.height/40;
		Sample.GetComponent<RectTransform>().anchoredPosition = new Vector2(Sample.GetComponent<RectTransform>().anchoredPosition.x,y);

    }
	
	// Update is called once per frame
	void Update () {
        // Raycast for object
        if(SelectMode == true)
        {
            RaycastHit hit;
            Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            if(Physics.Raycast(ray, out hit))
            {
                SelectedObj = hit.transform.gameObject;
                Debug.Log("Object Selected: " + SelectedObj);
                SelectMode = false;
                ColorMode = true;
                Sample.SetActive(true);
                col = SelectedObj.GetComponent<Renderer>().material.color;
                Sample.GetComponent<Image>().color = col;

                //Frees Cursor
                Crosshair.SetCursorEnabled();
                LookerX.enabled = false;
                LookerY.enabled = false;

            }
            else
            {
                Debug.Log("No object found");

                SelectMode = false;
                ColorMode = false;
                SelectedObj = null;
                Sample.SetActive(false);
                gameObject.SetActive(false);
            }
    }

        //Color is selected
        if(Input.GetMouseButtonDown(0) && ColorMode)
        {
            SelectedObj.GetComponent<Renderer>().material.color = col;
            SelectMode = false;
            ColorMode = false;
            SelectedObj = null;
            Sample.SetActive(false);
            gameObject.SetActive(false);

            // Returns to Crosshair
            Crosshair.SetCrosshairEnabled();
            LookerX.enabled = true;
            LookerY.enabled = true;
        }

        //Color select canceled
        if(ColorMode && Input.GetKeyDown(KeyCode.N))
        {
            SelectMode = false;
            ColorMode = false;
            SelectedObj = null;
            Sample.SetActive(false);
            gameObject.SetActive(false);

            //Returns to Crosshair
            Crosshair.SetCrosshairEnabled();
            LookerX.enabled = true;
            LookerY.enabled = true;
        }
    }
    
    void OnGUI()
    {
        // color wheel is only active if ColorMode = true
        if (ColorMode)
        {
            GUI.backgroundColor = Color.clear;
            GUI.DrawTexture(colorPanel, colorPicker);
            Vector2 pickpos = Event.current.mousePosition;
            float xx = (pickpos.x - colorPanel.x) / colorPanel.width;
            float yy = (pickpos.y - colorPanel.y) / colorPanel.height;
            yy = 1.0f - yy;
            col = colorPicker.GetPixelBilinear(xx, yy);
                
            if (col.a != 0)
            {
                Sample.GetComponent<Image>().color = col;
            }
        }
    }

    public void EnableSelectionMode()
    {
        gameObject.SetActive(true);
        SelectMode = true;
    }
}
