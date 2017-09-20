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



    void Start()
    {

    }

    void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "climbTrigger")
        {
            Climb();
        }
    }

    void Move(float h, float v)
    {
        currentH = Mathf.Lerp(currentH, h, Time.deltaTime * speed);
        currentV = Mathf.Lerp(currentV, v, Time.deltaTime * speed);

        transform.position += transform.forward * currentV * speed * Time.deltaTime;
        transform.Rotate(0, currentH * rotationSpeed * Time.deltaTime, 0);
    }


    /*The method, which is triggered by entering a climbTrigger, will cast Rays higher and higher and stop 
    when it no longer is hitting a climbable object. When the last ray misses I know that the last one hit the edge of the object. 
    I then move the player to this position. We need to play some kind of animation here, rather then just teleporting him to the new
    spot. */
    void Climb()
    {
        RaycastHit hitObj;
        Vector3 rayOriginOffset = new Vector3(0, 0.2f, 0);
        Ray ray = new Ray(transform.position + rayOriginOffset, transform.forward);
        Physics.Raycast(ray, out hitObj, 1);

        Debug.DrawRay(ray.origin, ray.direction, Color.blue, 20f);

        Vector3 lastRayHitPoint = transform.position;
        Physics.Raycast(ray, out hitObj, 1);
        if (hitObj.collider != null)
        {
            for (int i = 0; i < 12; i++)
            {
                rayOriginOffset.y += 0.1f;
                Ray rayTest = new Ray(transform.position + rayOriginOffset, transform.forward);
                Physics.Raycast(rayTest, out hitObj, 1);
                Debug.DrawRay(rayTest.origin, rayTest.direction, Color.blue, 20f);
                /*Vi får error här pga att vi kollar hittobj.collider även om null. Vet ej lösning*/
                if (hitObj.collider.tag != "climbableObject")
                {
                    break;
                }
                Debug.Log(lastRayHitPoint);
                lastRayHitPoint = hitObj.point;
            }
            transform.position = lastRayHitPoint;

        }
    }



}

