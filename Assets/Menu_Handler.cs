using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Handler : MonoBehaviour {
    public Transform hints_canvas;
    public Transform hints_canvas_off;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Select"))
        {
            if (hints_canvas.gameObject.activeInHierarchy==false)
            {
                hints_canvas.gameObject.SetActive(true);
                hints_canvas_off.gameObject.SetActive(false);
            }
            else
            {
                hints_canvas.gameObject.SetActive(false);
                hints_canvas_off.gameObject.SetActive(true);
            }
        }
	}
}
