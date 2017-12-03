using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushableObject : MonoBehaviour {

    HingeJoint hj;
    Rigidbody mRigidBody;
    GameObject Player;
    public bool x, z, y;
    public Vector3 originalPosition;
    public bool maxConstraint;
    public float maxMovement;

	void Start ()
    {
        mRigidBody = GetComponent<Rigidbody>();
        Player = GameObject.FindGameObjectWithTag("Player");
        mRigidBody.constraints = RigidbodyConstraints.FreezeAll;
        originalPosition = transform.position;
        if (y == true)
        {
            mRigidBody.constraints &= ~(RigidbodyConstraints.FreezePositionY);
        }
    }	

	void Update ()
    {
        float stopX = originalPosition.x - transform.position.x;
        float stopY = originalPosition.y - transform.position.y;
        float stopZ = originalPosition.z - transform.position.z;

        if (maxConstraint && x)
        {
            if (Mathf.Abs(stopX) > maxMovement)
            {
                Player.GetComponent<GrabObject>().Grab();
                var heading = transform.position - originalPosition;
                var distance = heading.magnitude;
                var direction = heading / distance;
                transform.position -= direction;
            }

        }
        if (maxConstraint && y)
        {
            if (Mathf.Abs(stopY) > maxMovement)
            {
                Player.GetComponent<GrabObject>().Grab();
                var heading = transform.position - originalPosition;
                var distance = heading.magnitude;
                var direction = heading / distance;
                transform.position -= direction*Time.deltaTime;

            }

        }
        if (maxConstraint && z)
        {
            if (Mathf.Abs(stopZ) > maxMovement)
            {
                Player.GetComponent<GrabObject>().Grab();
                var heading = transform.position - originalPosition;
                var distance = heading.magnitude;
                var direction = heading / distance;
                transform.position -= direction;
            }

        }
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
        if (y == true)
        {
            mRigidBody.constraints &= ~(RigidbodyConstraints.FreezePositionY);
        }
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
