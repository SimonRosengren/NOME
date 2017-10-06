using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpot : MonoBehaviour {
    public PhysicMaterial mat;
	// Use this for initialization
	void Start () {
        mat = (PhysicMaterial)Resources.Load("PhysicMaterial/NoFriction");
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerStay(Collider other)
    {
        if (other.tag == ("Player") && other.material.name != "NoFriction")
        {
            other.material = mat;
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.material = null;
        }
    }
}
