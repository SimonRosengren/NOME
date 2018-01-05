using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launchzone : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider other)
    {
        GameObject vaccum = GameObject.Find("VacuumRobot/VacuumRobot_grp/hoseRootNode_joint/hoseNodeFront_joint1/hoseNodeFront_joint2/hoseNodeFront_joint3/hoseNode_joint4/headPipe_ctrl/headPipe_geo/headPiece_ctrl/headPiece_geo/GravitationalCircle/Stuck&LaunchPoint");
        VacuumHoldLaunch vScript = vaccum.GetComponent<VacuumHoldLaunch>();
        vScript.canFire = true;

        //Debug.Log("yes");
        //if (other.tag == "Vaccum")
        //{
        //    other.SendMessage("Activate");
        //}
    }

    void OnTriggerExit(Collider other)
    {
        GameObject vaccum = GameObject.Find("VacuumRobot/VacuumRobot_grp/hoseRootNode_joint/hoseNodeFront_joint1/hoseNodeFront_joint2/hoseNodeFront_joint3/hoseNode_joint4/headPipe_ctrl/headPipe_geo/headPiece_ctrl/headPiece_geo/GravitationalCircle/Stuck&LaunchPoint");
        VacuumHoldLaunch vScript = vaccum.GetComponent<VacuumHoldLaunch>();
        vScript.canFire = false;
        
        //if (other.tag == "Vaccum")
        //{
        //    other.SendMessage("Deactivate");
        //}
    }

}
