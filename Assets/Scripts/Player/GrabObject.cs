using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour {

    RaycastHit grabbedObject;
    Rigidbody playerRb;
    Animator animator;

    public bool isGrabbing;

	// Use this for initialization
	void Start ()
    {
        isGrabbing = false;
        playerRb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {		
	}

    void OnAnimatorIK()
    {
        if (isGrabbing)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0.5f);
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0.5f);
            animator.SetIKPosition(AvatarIKGoal.RightHand, grabbedObject.transform.position);
            animator.SetIKPosition(AvatarIKGoal.LeftHand, grabbedObject.transform.position);
        }
    }

    public void Grab()
    {
        if (!isGrabbing)
        {
            Ray ray = new Ray(transform.position + new Vector3(0, 0.1f, 0), transform.forward);
            if (!Physics.Raycast(ray, out grabbedObject, 1))
                return;

            if (grabbedObject.transform.tag == "Grabable")
            {
                animator.SetBool("grabbingObj", true);
                pushableObject hitObjScript = grabbedObject.transform.GetComponent<pushableObject>();
                hitObjScript.Grab(playerRb, grabbedObject.point);
                isGrabbing = true;
            }
        }
        else
        {
            pushableObject hitObjScript = grabbedObject.transform.GetComponent<pushableObject>();
            hitObjScript.LetGo();
            isGrabbing = false;
        }
    }
}
