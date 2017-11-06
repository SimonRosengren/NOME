using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openshelf : MonoBehaviour {
    bool open;
    Animator shelf;
    
	// Use this for initialization
	void Start () {
        open = false;
        shelf = GameObject.Find("Kitchen_Shelf").GetComponent<Animator>();
        
	}
	
	// Update is called once per frame
	

    private void OnTriggerEnter(Collider other)
    {
        if(other.name== "LedgeGrabBox" && open==false)
        {
            
            shelf.Play("Shelf Opening");
            Debug.Log("Open");
        }
    }

    

}
