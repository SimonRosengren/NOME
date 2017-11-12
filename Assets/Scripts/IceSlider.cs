using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSlider : MonoBehaviour {

    private BoxCollider bc;

	// Use this for initialization
	void Start ()
    {
        bc = gameObject.GetComponent<BoxCollider>();		
	}
	
	// Update is called once per frame
	void Update ()
    {        		
	}

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Freezable")
        {
            bc.isTrigger = false;
        }
        else if (!bc.isTrigger)
        {
            bc.isTrigger = true;
        }
    }
}
