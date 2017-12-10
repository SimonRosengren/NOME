using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezableObject : MonoBehaviour {

    public BoxCollider bc;
    public MeshRenderer mesh;
    public Material waterMat;
    public Material freezeMat;

	// Use this for initialization
	void Start ()
    {		
	}
	
	// Update is called once per frame
	void Update () 
    {		
	}

    void Freeze()
    {
        bc.isTrigger = false;
        mesh.material = freezeMat;
        gameObject.GetComponent<BoxCollider>().material = (PhysicMaterial)(Resources.Load("PhysicMaterial/NoFriction"));        
    }

    void SetFire()
    {
        bc.isTrigger = true;
        mesh.material = waterMat;
    }
}
