using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeGround : MonoBehaviour {
    public GameObject ice;
    private float timer;
	// Use this for initialization
	void Start () 
    {
        //ice = GameObject.Find("IceSpot");
        timer = 1;
	}
	
	// Update is called once per frame
	void Update () {
        if (timer >= 0)
        {
            timer -= Time.deltaTime;   
        }


        if (Input.GetButton("Fire2") && timer <= 0 || Input.GetKey(KeyCode.V) && timer <= 0)
        {           
            Collider[] col = Physics.OverlapSphere(gameObject.transform.position, 1);
            bool frozen = false;
            int i = 0;
            while (i < col.Length)
            {
                if (col[i].tag == "Freezable")
                {
                    col[i].SendMessageUpwards("Freeze");
                    frozen = true;
                }
                i++;
            }
            if (!frozen)
            {
                if (gameObject.GetComponent<CapsuleCollider>().material.name != "NoFriction (Instance)")
                {
                    Instantiate(ice, new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z), Quaternion.Euler(new Vector3(90, 0, 0)));
                    timer = 1;    
                }               
            }
            
        }
		
	}
}
