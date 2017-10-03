using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushableObject : MonoBehaviour {

    HingeJoint hj;
    Rigidbody mRigidBody;
	void Start ()
    {
        mRigidBody = GetComponent<Rigidbody>();
	}
	

	void Update ()
    {
		
	}

    /*Takes the players rigid body and point where ray cast hit the object as argument*/
    public void Grab(Rigidbody otherRb, Vector3 anchorPoint)
    {
        gameObject.AddComponent<HingeJoint>();
        hj = GetComponent<HingeJoint>();

        hj.connectedBody = otherRb;
        hj.anchor = anchorPoint; //Rotation?

        mRigidBody.freezeRotation = true;
    }

    public void LetGo()
    {
        Destroy(hj);
        mRigidBody.freezeRotation = false;
    }
}
