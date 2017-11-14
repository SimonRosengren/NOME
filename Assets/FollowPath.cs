using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour {
    public float movementSpeed;
	// Use this for initialization
	void Start () {
        Path();
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void Path()
    {
        iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("RobotPath"), "speed",movementSpeed, "orientToPath", true, "delay", 0,"oncomplete","Path", "easetype", iTween.EaseType.easeInOutQuad));

    }
}
