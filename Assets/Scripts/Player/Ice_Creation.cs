using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice_Creation : MonoBehaviour {

    public GameObject ice;
	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            CreateIce();
        }
	}

    void CreateIce()
    {
        Instantiate(ice);
    }

    void FindContactPointGround()
    {

    }
}
