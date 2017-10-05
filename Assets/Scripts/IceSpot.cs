using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpot : MonoBehaviour {
    public PhysicMaterial mat;
    public bool melting = false;
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
    void SetFire()
    {
        melting = true;
    }
    void Melt()
    {
        gameObject.transform.localScale -= Vector3.one * Time.deltaTime * 1;
        if (gameObject.transform.localScale.x < 0.1f)
        {
            Destroy(gameObject);
        }
    }
}
