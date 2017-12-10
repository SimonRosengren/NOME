using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileScript : MonoBehaviour {

    public float maxForce = 1;
    Rigidbody rb;
    public Vector3 target;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Awake()
    {
        Destroy(gameObject, 10);
        //Vector3 dir = Vector3.Normalize(target - transform.position);
        //rb.AddForce(dir * Random.RandomRange(10, maxForce), ForceMode.VelocityChange);
    }
	
	// Update is called once per frame
	void Update ()
    {        
    }

}
