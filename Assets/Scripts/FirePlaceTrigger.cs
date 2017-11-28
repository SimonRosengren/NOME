using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePlaceTrigger : MonoBehaviour {

    ParticleSystem sprinkler;
    Transform sprinklerTransform;

    [SerializeField]RobotBehaviour robot;


	void Start () {

	}

	void Update () {
		
	}

    public void ActivateSprinkler()
    {
        Instantiate(sprinkler, sprinklerTransform);
        Invoke("DeactivateRobot", 2);
    }

    public void DeactivateRobot()
    {
        robot.wet = true;
    }
}
