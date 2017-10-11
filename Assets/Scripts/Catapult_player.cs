using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapult_player : MonoBehaviour {

    float timeleft = 3;
    public float cforce = 7;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    private void OnTriggerStay(Collider other)
    {
        Rigidbody catapultObject = other.gameObject.GetComponent<Rigidbody>();
        timeleft -= Time.deltaTime;
        Debug.Log(timeleft);
        if (timeleft < 0)
        {
            Debug.Log("launch");
            catapultObject.AddForce(transform.up * cforce);
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        timeleft = 3;
    }


}
