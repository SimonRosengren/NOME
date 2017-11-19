using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBrick : MonoBehaviour {

    public GameObject Item;
    GameObject Robot;
	// Use this for initialization
	void Start () {
        Robot = GameObject.FindGameObjectWithTag("Vaccum");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerExit(Collider other)
    {
        if (other == Item)
        {
            Debug.Log("check");
            Robot.GetComponent<RobotBehaviour>().wet = true;
        }
    }
}
