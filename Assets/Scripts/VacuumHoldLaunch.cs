using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumHoldLaunch : MonoBehaviour {
    GravitationalPull gP;
    public Vector3 launchV;
    public float launchF;
    private float timer;
    bool holdOn =true;
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
        //Debug.Log(holdOn);
        if (holdOn)
        {
            gP.rbTarget.isKinematic = true;
            if (other.gameObject == gP.target)
            {
                gP.pullOn = false;
                timer -= Time.deltaTime;
                //Debug.Log(timer);
                if (timer>0)
                {
                    Hold();
                }
                else
                {
                    holdOn = false;
                    gP.rbTarget.isKinematic = false;
                    Launch();
                    timer = 3;
                    Debug.Log(holdOn);
                    Debug.Log(gP.pullOn);
                    Debug.Log(timer);
                }
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        holdOn = true;
    }

    private void Hold()
    {
        gP.target.transform.position = transform.position;
    }

    private void Launch()
    {
        
        gP.rbTarget.AddForce(launchV * launchF, ForceMode.Impulse);
        
        
    }

   
}
