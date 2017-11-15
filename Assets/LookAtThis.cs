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
    private void OnTriggerEnter(Collider other)
    {
        Player.GetComponent<HeadLookController>().target = lookat.position;
    }
}
