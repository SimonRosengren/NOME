using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableWithRobot : MonoBehaviour {

    [SerializeField] GameObject robot;

    ChasingRobot robotHandler;

	void Start () {
        robotHandler = robot.GetComponent<ChasingRobot>();
	}
	

	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            robotHandler.SetToActive();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            robotHandler.Deactivate();
        }
        if (other.tag == "grabable")
        {
            robotHandler.isDead = true;
        }
    }
}
