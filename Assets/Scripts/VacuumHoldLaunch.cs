using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumHoldLaunch : MonoBehaviour {
    GravitationalPull gP;
    public Vector3 launchV;
    public float launchF;
    private float stuckTimer,suckTimer;
    bool holdOn = false;
    bool suckTimerOn;

	// Use this for initialization
	void Start ()
    {
        gP = GetComponentInParent<GravitationalPull>();
        stuckTimer = 3;
        suckTimer = 3;
	}
	
	// Update is called once per frame
	void Update ()
    {		
        if (stuckTimer < 0)
        {
            holdOn = false;
            
            Launch();
            Debug.Log(holdOn);
            Debug.Log(gP.pullOn);
            Debug.Log(stuckTimer);
            stuckTimer = 3;        
        }

        if (holdOn)
        {
            gP.pullOn = false;
            stuckTimer -= Time.deltaTime;
            Hold();
        }
        Sucking();       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (stuckTimer>0)
        {
            holdOn = true;
        }
    }   

    private void Hold()
    {
        if (gP.target.transform.position!=transform.position)
        {
            gP.target.transform.position = transform.position;
        }
        gP.target.transform.parent = transform;
        gP.rbTarget.isKinematic = true;
    }

    void Sucking()
    {
        if (suckTimerOn)
        {
            suckTimer = -Time.deltaTime;
        }

        if (suckTimer < 0)
        {
            gP.pullOn = true;
            suckTimer = 3;
            suckTimerOn = false;
        }
    }

    private void Launch()
    {
        gP.target.transform.parent = null;
        gP.rbTarget.isKinematic = false;
        gP.rbTarget.AddForce(launchV * launchF, ForceMode.Impulse);
        suckTimerOn = true;                
    }  
}
