using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBehaviour : MonoBehaviour {

    public bool wet;
    public GameObject gravCircle;
    FollowPath pathFollower;
    Animator animator;
    public InstantiatePickUp pickup;
    public ParticleSystem smoke;
    bool dead = false;

	// Use this for initialization
	void Start () {
        wet = false;
        animator = GetComponent<Animator>();
        pathFollower = GetComponent<FollowPath>();
	}
	
	// Update is called once per frame
	void Update () {
        if (wet && !dead)
        {
            GetComponent<FollowPath>().functional = false;
            Smoke();
            animator.enabled = false;
            pathFollower.functional = false;
            Destroy(gravCircle);
            Invoke("SpitOut", 5);
            dead = true;
        }	
	}

    void Smoke()
    {
        Instantiate(smoke, transform);
    }

    void SpitOut()
    {
        pickup.DropPickUp();
    }

}
