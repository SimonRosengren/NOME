using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitationalPull : MonoBehaviour {

    public GameObject target;
    public Rigidbody rbTarget;
    public float pullForce;
    public bool pullOn = true;
    private bool resetPull;
    private float timer;

	// Use this for initialization
	void Start ()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        rbTarget = target.gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (resetPull)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                pullOn = true;
                resetPull = false;
            }
        }		
	}

    private void OnTriggerStay(Collider other)
    {        
        if (other.gameObject == target && pullOn)
        {            
                target.transform.position = Vector3.MoveTowards(target.transform.position, transform.position, pullForce * Time.deltaTime);            
        }        
    }

    private void OnTriggerExit(Collider other)
    {
        resetPull = true;
        timer = 3;
    }
}
