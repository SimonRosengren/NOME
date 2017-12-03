using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float : MonoBehaviour {
    //Height of the waterlevel
    public float waterLevel = 1.8f;
    //Offset float height above waterlevel
    public float floatHeight = 2;
    //The bobbing frequency in water
    public float bounceDamp = 0.08f;
    //Offset to adjust the objects center of buoyancy(from objects center).
    public Vector3 buoyancyOffsetCenter = Vector3.zero;

    private float forceFactor;
    private Vector3 actionPoint;
    private Vector3 upLift;
    private Rigidbody rb;

	void Awake ()
    {
        rb = gameObject.GetComponent<Rigidbody>();
	}
	
	void Update ()
    {
        //Gets the center of buoyancy and calculates force to apply dependant on objects height in water
        actionPoint = transform.position + transform.TransformDirection(buoyancyOffsetCenter);
        forceFactor = 1f - ((actionPoint.y - waterLevel) / floatHeight);
        //Applies an lifting force as long as the object is below the waterlevel
        if (forceFactor > 0f)
        {
            //Calculates lift force
            //rb.mass might not work right here
            upLift = -Physics.gravity * (forceFactor - rb.velocity.y * bounceDamp) * rb.mass;
            rb.AddForceAtPosition(upLift, actionPoint);
        }
	}
}
