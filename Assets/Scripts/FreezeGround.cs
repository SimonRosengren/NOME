using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeGround : MonoBehaviour {
    public GameObject ice;
    private float timer;
    private float chargeTimer;
    public CapsuleCollider capC;
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
        if (Input.GetButtonDown("Fire2"))
        {
            chargeTimer += Time.deltaTime;
        }
        //if (Input.GetButtonUp("Fire2") && chargeTimer > 2)
        if (Input.GetKey(KeyCode.B) && timer <= 0)
        {
            timer = 1;
            Collider[] colls = Physics.OverlapSphere(gameObject.transform.position, 0.5f);
            int i = 0;
            while (i < colls.Length)
            {
                if (colls[i].tag == "Freezable")
                {    
                    colls[i].SendMessageUpwards("Freeze");
                }
                i++;
            }
            
        }

        if (Input.GetButton("Fire2") || Input.GetKey(KeyCode.V) 
            && chargeTimer < 2 && timer <= 0 && capC.material.name != "NoFriction (Instance)")
        {
            Instantiate(ice, new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z), Quaternion.Euler(new Vector3(90, 0, 0)));
            timer = 1;
        }
		
	}
}
