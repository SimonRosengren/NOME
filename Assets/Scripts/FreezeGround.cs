﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeGround : MonoBehaviour {
    public GameObject ice;
    public float timer;
	// Use this for initialization
	void Start () 
    {
        ice = GameObject.Find("IceSpot");
        timer = 1;
	}
	
	// Update is called once per frame
	void Update () {
        if (timer >= 0)
        {
            timer -= Time.deltaTime;   
        }
        

        if (Input.GetButton("Fire2") || Input.GetKey(KeyCode.V) && timer <= 0)
        {
            Instantiate(ice, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.Euler(new Vector3(90, 0, 0)));
            timer = 1;
        }
		
	}
}
