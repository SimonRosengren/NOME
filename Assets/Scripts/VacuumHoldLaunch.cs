using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumHoldLaunch : MonoBehaviour {
    GravitationalPull gP;
    public Vector3 launchV;
    public float launchF;
    private float timer;
     bool HoldOn=true;
	// Use this for initialization
	void Start () {
        gP = GetComponentInParent<GravitationalPull>();
        timer = 3;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        if (HoldOn)
        {
            if (other.gameObject == gP.target)
            {
                gP.PullOn = false;
                timer -= Time.deltaTime;
                Debug.Log(timer);
                if (timer>0)
                {
                    Hold();
                }
                else
                {
                    HoldOn = false;
                    Launch();
                    timer = 3;
                }
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        HoldOn = true;
    }

    private void Hold()
    {
        gP.target.transform.position = transform.position;
    }

    private void Launch()
    {
        gP.rbTarget.AddForce(launchV * launchF);
    }

   
}
