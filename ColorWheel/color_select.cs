using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class color_select : MonoBehaviour {
    public Texture2D colorPicker;
    public GameObject Sample;
    public Rect colorPanel;

    public Color col;
    Vector2 pickpos;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       
    }
    //void OnMouseDown()
    //{
       // float aaa = pickpos.x - transform.position.x;
        //float bbb = pickpos.y - transform.position.y;
        //int height = (int) GetComponent<Collider2D>().bounds.size.y;
        //int width = (int)GetComponent<Collider2D>().bounds.size.x;

        //int aaa2 = (int)(aaa * (wheel.width / (width + 0.0f)));
        //int bbb2 = (int)((height - bbb) * (wheel.height / (height + 0.0f)));

        //col = wheel.GetPixel(aaa2, bbb2);
        //Sample.GetComponent<Renderer>().material.color = col;
        
    //}
    
    void OnGUI()
    {
        GUI.DrawTexture(colorPanel, colorPicker);
        
        if (GUI.RepeatButton(colorPanel, ""))
        {
            Vector2 pickpos = Event.current.mousePosition;
            int aaa = (int)(pickpos.x - colorPanel.x);
            int bbb = (int)(pickpos.y - colorPanel.y);
            col = colorPicker.GetPixel(aaa, 700-bbb);
            Debug.Log(col.ToString());
            if(col.a == 1)
            {
                Sample.GetComponent<Renderer>().material.color = col;
            }
            


        }
    } 
}
