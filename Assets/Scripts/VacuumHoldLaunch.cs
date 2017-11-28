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
    public bool canFire = false;

    public Transform idleTarget;
    Vector3 target;
    public float speed;
    // Use this for initialization
    void Start ()
    {
        gP = GetComponentInParent<GravitationalPull>();
        stuckTimer = 3;
        suckTimer = 3;
        target = idleTarget.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {		
        if (stuckTimer < 0 && canFire)
        {
            holdOn = false;
            
            Launch();
            //Debug.Log(holdOn);
            //Debug.Log(gP.pullOn);
            //Debug.Log(stuckTimer);
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

    public void Launch()
    {
        Debug.Log("lauch");
        gP.target.transform.parent = null;
        gP.rbTarget.isKinematic = false;
        //gP.rbTarget.AddForce (FireAt(target), ForceMode.VelocityChange);
        gP.rbTarget.velocity = FireAt2(target, 3.1f);
        suckTimerOn = true;
        Debug.Log(gP.rbTarget.velocity);            
    }

    private Vector3 FireAt(Vector3 target/*, GameObject projectile*/)
    {
        Vector3 toTarget = target - transform.position;

        // Set up the terms we need to solve the quadratic equations.
        float gSquared = Physics.gravity.sqrMagnitude;
        float b = speed * speed + Vector3.Dot(toTarget, Physics.gravity);
        float discriminant = b * b - gSquared * toTarget.sqrMagnitude;

        // Check whether the target is reachable at max speed or less.
        if (discriminant < 0)
        {
            speed = speed * 2;
            b = speed * speed + Vector3.Dot(toTarget, Physics.gravity);
            discriminant = b * b - gSquared * toTarget.sqrMagnitude;
            // Target is too far away to hit at this speed.
            // Abort, or fire at max speed in its general direction?
        }

        float discRoot = Mathf.Sqrt(discriminant);

        // Highest shot with the given max speed:
        float T_max = Mathf.Sqrt((b + discRoot) * 2f / gSquared);

        // Most direct shot with the given max speed:
        float T_min = Mathf.Sqrt((b - discRoot) * 2f / gSquared);

        // Lowest-speed arc available:
        float T_lowEnergy = Mathf.Sqrt(Mathf.Sqrt(toTarget.sqrMagnitude * 4f / gSquared));

        float T = T_max;// choose T_max, T_min, or some T in-between like T_lowEnergy

        // Convert from time-to-hit to a launch velocity:
        Vector3 velocity = toTarget / T - Physics.gravity * T / 2f;

        return velocity;
        // Apply the calculated velocity (do not use force, acceleration, or impulse modes)
    }

    private Vector3 FireAt2(Vector3 target, float timeToTarget)   
    {
        // calculate vectors
        Vector3 origin = transform.position;
        Vector3 toTarget = target - origin;
        Vector3 toTargetXZ = toTarget;
        toTargetXZ.y = 0;

        // calculate xz and y
        float y = toTarget.y;
        float xz = toTargetXZ.magnitude;

        // calculate starting speeds for xz and y. Physics forumulase deltaX = v0 * t + 1/2 * a * t * t
        // where a is "-gravity" but only on the y plane, and a is 0 in xz plane.
        // so xz = v0xz * t => v0xz = xz / t
        // and y = v0y * t - 1/2 * gravity * t * t => v0y * t = y + 1/2 * gravity * t * t => v0y = y / t + 1/2 * gravity * t
        float t = timeToTarget;
        float v0y = y/ t + 0.5f * Physics.gravity.magnitude * t;
        float v0xz = xz/t;

        // create result vector for calculated starting speeds
        Vector3 result = toTargetXZ.normalized;        // get direction of xz but with magnitude 1
        result *= v0xz;                                // set magnitude of xz to v0xz (starting speed in xz plane)
        result.y = v0y;                                // set y to v0y (starting speed of y plane)

        return result;
    }

    void Activate()
    {
        Debug.Log("hej");
        canFire = true;
    }
    void Deactivate()
    {
        canFire = false;
    }
}



