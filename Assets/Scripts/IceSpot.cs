using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpot : MonoBehaviour {
    public PhysicMaterial mat;
    public bool melting = false;
    public SphereCollider sc;
    public Projector pr;
	// Use this for initialization
	void Start () {
        mat = (PhysicMaterial)Resources.Load("PhysicMaterial/NoFriction");
		
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (melting)
        {
            Melt();
        }
		
	}
    void OnTriggerStay(Collider other)
    {
        if (other.tag == ("Player") || other.tag == ("grabable") && other.material.name != "NoFriction")
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
        sc.radius -= Time.deltaTime * 0.5f;
        pr.orthographicSize -= Time.deltaTime * 0.5f;
        if (sc.radius <= 0)
        {
            Destroy(gameObject);
        }
        
    }
}
