using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileScript : MonoBehaviour {

    public float force = 1;
    Rigidbody rb;
	// Use this for initialization
	void Start () {

	}
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * force, ForceMode.VelocityChange);
    }
	
	// Update is called once per frame
	void Update () {
        
    }
}
