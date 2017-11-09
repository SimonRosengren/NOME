using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpot : MonoBehaviour {

    public PhysicMaterial mat;
    public bool melting = false;
    public SphereCollider sc;
    public Projector pr;

	void Start ()
    {
        mat = (PhysicMaterial)Resources.Load("PhysicMaterial/NoFriction");		
	}
	
	void Update () 
    {
        if (melting)
        {
            Melt();
        }		
	}

    void OnTriggerStay(Collider other)
    {
        if (other.material.name != "NoFriction (Instance)")
        {
            if (other.tag == ("Player") || other.tag == ("CleaningRobot") || other.tag == ("Grabable"))
            {
                other.material = mat;
            }
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" || other.tag == ("CleaningRobot") || other.tag == ("Grabable"))
        {
            other.material = null; //Can we make it go back to original?
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
