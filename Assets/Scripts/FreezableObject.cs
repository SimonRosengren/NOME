using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Water;

public class FreezableObject : MonoBehaviour {

    public BoxCollider bc;
    public MeshRenderer mesh;
    public Material waterMat;
    public Material freezeMat;
    public float timer = 0;
    bool frozen = false;
    float delay = 2;
    

	// Use this for initialization
	void Start ()
    {
        mesh = GetComponent<MeshRenderer>();     
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (frozen && timer < 1f)
        {
            timer += Time.deltaTime / delay;
            if (timer > 1f)
            {
                timer = 1f;
                mesh.material = freezeMat;
            }              
            mesh.material.Lerp(waterMat, freezeMat, timer);
        }
	}

    void Freeze()
    {
        if (!frozen)
        {
            transform.parent.GetComponent<GerstnerDisplace>().enabled = false;
            bc.isTrigger = false;
            gameObject.GetComponent<BoxCollider>().material = (PhysicMaterial)(Resources.Load("PhysicMaterial/NoFriction"));
            frozen = true;
        }
        
    }

    void SetFire()
    {
        bc.isTrigger = true;
        mesh.material = waterMat;
    }
}
