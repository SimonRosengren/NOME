using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBehaviour : MonoBehaviour {

    public bool wet;
    public GameObject gravCircle;

	// Use this for initialization
	void Start () {
        wet = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (wet)
        {
            GetComponent<FollowPath>().functional = false;
            Smoke();
            Destroy(gravCircle);
            SpitOut();
        }	
	}

    void Smoke()
    {

    }

    void SpitOut()
    {

    }

}
