using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RobotBehaviour : MonoBehaviour {

    public bool wet;
    public GameObject gravCircle;
    public GameObject launchZone;
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
            WriteToLog("Robots wet-statement has been called");
            GetComponent<FollowPath>().functional = false;
            Smoke();
            animator.enabled = false;
            pathFollower.functional = false;
            Destroy(gravCircle);
            Destroy(launchZone);
            Destroy(GameObject.Find("key_geo"));
            Invoke("SpitOut", 5);
            dead = true;
            WriteToLog("Robots isDead = " + dead);
        }	
	}

    void Smoke()
    {
        Instantiate(smoke, transform);
    }

    void SpitOut()
    {
        WriteToLog("SpitOut() has been invoked");
        pickup.DropPickUp();
    }
    public void WriteToLog(string msg)
    {
        string path = @"" + "mOutPut" + ".txt";
        using (StreamWriter writer = File.AppendText(path))
        {
            writer.WriteLine(msg + "\n");
            writer.Close();
        }
    }
}
