using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody playerRb;

    /*Player movement*/
    public float speed = 6f;
    public float rotationSpeed = 200f;
    public float jumpForce = 10f;
    /*Movement vector*/
    float currentV;
    float currentH;

    bool isGrounded;

    

	void Start ()
    {
		
	}

    void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
    }

	void FixedUpdate ()
    {

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Move(h, v);

        if (isGrounded && Input.GetButton("Jump"))
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
	}

    private void OnCollisionEnter(Collision collision)
    {

        /*Check so that what we have collided with has its normal pointing up; then we are grounded*/
        ContactPoint[] contactPoints = collision.contacts;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                isGrounded = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        /*RISK FOR BUGS*/
        /*Need to check what collision we just exited!!*/
        isGrounded = false;
    }

    void Move(float h, float v)
    {
        currentH = Mathf.Lerp(currentH, h, Time.deltaTime * speed);
        currentV = Mathf.Lerp(currentV, v, Time.deltaTime * speed);

        transform.position += transform.forward * currentV * speed * Time.deltaTime;
        transform.Rotate(0, currentH * rotationSpeed * Time.deltaTime, 0);
    }
}
