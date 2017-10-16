using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitationalPull : MonoBehaviour {

    public GameObject target;
    public Rigidbody rbTarget;
    public float pullForce;
    public bool PullOn=true;
	// Use this for initialization
	void Start () {
        target = GameObject.FindGameObjectWithTag("Player");
        rbTarget = target.gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        
        if (other.gameObject == target && PullOn)
        {
            
                target.transform.position = Vector3.MoveTowards(target.transform.position, transform.position, pullForce * Time.deltaTime);
            
        }

        
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    PullOn = true;
    //}
}
