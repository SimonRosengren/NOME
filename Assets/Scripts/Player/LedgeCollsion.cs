using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeCollsion : MonoBehaviour {

    public bool hanging=false;
    public RuntimeAnimatorController controller;
    public Animator playerAnimator;
    Rigidbody playerRb;
    GameObject Player;
    Collider hangobject;

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
    private void LateUpdate()
    {
        if (hanging == true)
        {
           // Player.transform.position = hangobject.transform.position;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="climbTrigger")
        {
            //hangobject = other;
            playerRb.constraints = RigidbodyConstraints.FreezeAll;
            hanging = true;
            playerAnimator.SetBool("isHanging", true);

           //playerAnimator.SetTrigger("isHangingTrigger");

        }
    }

}
