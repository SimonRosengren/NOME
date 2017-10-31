using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushableObject : MonoBehaviour {

    HingeJoint hj;
    Rigidbody mRigidBody;
    public bool x, z;
	void Start ()
    {
        mRigidBody = GetComponent<Rigidbody>();
        mRigidBody.constraints = RigidbodyConstraints.FreezeAll;
        

    }
	

	void Update ()
    {
		
	}

    /*Takes the players rigid body and point where ray cast hit the object as argument*/
    public void Grab(Rigidbody otherRb, Vector3 anchorPoint)
    {
        UnlockDirections();
        gameObject.AddComponent<HingeJoint>();
        hj = GetComponent<HingeJoint>();

        hj.connectedBody = otherRb;
        hj.anchor = anchorPoint; //Rotation?

        mRigidBody.freezeRotation = true;
    }

    public void LetGo()
    {
        mRigidBody.constraints = RigidbodyConstraints.FreezeAll;
        Destroy(hj);
        //mRigidBody.freezeRotation = false;
    }

    void UnlockDirections()
    {
        if (x == true)
        {
            mRigidBody.constraints &= ~(RigidbodyConstraints.FreezePositionX);

        }
        if (z == true)
        {
            mRigidBody.constraints &= ~(RigidbodyConstraints.FreezePositionZ);
        }
    }
    
}
