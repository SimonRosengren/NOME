using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeCollsion : MonoBehaviour {

    public bool hanging=false;
    Rigidbody playerRb;

    private void Awake()
    {
        
        playerRb = transform.parent.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (Input.GetButtonDown("Jump") && hanging==true)
        {
            playerRb.constraints = RigidbodyConstraints.None|RigidbodyConstraints.FreezeRotation;
            
            playerRb.AddForce(Vector3.up * 6, ForceMode.Impulse);

            hanging = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="climbTrigger")
        {
            playerRb.constraints = RigidbodyConstraints.FreezeAll;
            hanging = true;
        }
    }

}
