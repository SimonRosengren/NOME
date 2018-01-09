using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtThis : MonoBehaviour {
    public Transform lookat;
    GameObject Player;
	// Use this for initialization
	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == Player)
        {
            
            if (Player.GetComponent<HeadLookController>().enabled == false)
            {
                Player.GetComponent<HeadLookController>().enabled = true;
            }
            
            Player.GetComponent<HeadLookController>().target = lookat.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Player)
        {
            if (Player.GetComponent<HeadLookController>().enabled == true)
            {
                Player.GetComponent<HeadLookController>().enabled = false;
            }
        }
    }
}
