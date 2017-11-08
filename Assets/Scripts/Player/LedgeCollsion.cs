using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeCollsion : MonoBehaviour {

    public bool hanging=false;
    public RuntimeAnimatorController controller;
    public Animator playerAnimator;
    Rigidbody playerRb;
    GameObject Player;
    Collider hangobject; //is this needed?
    HingeJoint hj;
    Vector3 p_offset;
    Vector3 r_offset;
    GameObject target;


    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerRb = transform.parent.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (Input.GetButtonDown("Jump") && hanging==true)
        {
            Player.transform.parent = null;
            playerRb.constraints = RigidbodyConstraints.None|RigidbodyConstraints.FreezeRotation;
            playerRb.AddForce(Vector3.up * 6, ForceMode.Impulse);
            hanging = false;
            Debug.Log("jump");
            playerAnimator.SetBool("isHanging", false);            
        }       
    }   

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="climbTrigger" && hanging==false)
        {
            //target = other.gameObject;
            //p_offset = Player.transform.position - target.transform.position;
            Player.transform.parent = other.transform;

            playerRb.constraints = RigidbodyConstraints.FreezeAll;
            hanging = true;           
        }
    }
}
