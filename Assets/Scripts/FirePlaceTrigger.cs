using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePlaceTrigger : MonoBehaviour {

    bool brickRemoved = false;
    bool sprinklerOn = false;

    [SerializeField]GameObject sprinkler;

    [SerializeField]Transform sprinklerTransform;

    [SerializeField]Transform smokeTransform;

    [SerializeField]RobotBehaviour robot;
    [SerializeField]FlammableObject fire;

    [SerializeField]ParticleSystem smoke;


	void Start () {

	}

	void Update () {
        ActivateSprinkler();
	}

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "grabable")
        {
            brickRemoved = true;
        }
    }

    public void ActivateSprinkler()
    {
        if (fire.onFire && brickRemoved && !sprinklerOn)
        {
            Instantiate(sprinkler, sprinklerTransform);
            Instantiate(smoke, smokeTransform);
            sprinklerOn = true;
            Invoke("DeactivateRobot", 2);

        }
    }

    public void DeactivateRobot()
    {
        robot.wet = true;
    }
}
