using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody playerRb;

    [SerializeField] private Animator animator;

    Vector3 hangingPos;

    /*Player movement*/
    public float speed = 6f;
    public float rotationSpeed = 200f;
    public float jumpForce = 10f;

    /*Movement vector*/
    float currentV;
    float currentH;

    public bool IsHanging = false;



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

        if (IsGrounded() && Input.GetButton("Jump"))
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

    }

    private void OnCollisionExit(Collision collision)
    {

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
        if (!IsHanging)
        {
            currentH = Mathf.Lerp(currentH, h, Time.deltaTime * speed);
            currentV = Mathf.Lerp(currentV, v, Time.deltaTime * speed);

            transform.position += transform.forward * currentV * speed * Time.deltaTime;
            transform.Rotate(0, currentH * rotationSpeed * Time.deltaTime, 0);

            animator.SetFloat("MoveSpeed", currentV);

            Debug.Log(currentV);
        }
        else
        {
            if (Input.GetButton("Jump"))
            {
                playerRb.constraints = RigidbodyConstraints.None;
                playerRb.constraints = RigidbodyConstraints.FreezeRotation;
                playerRb.AddForce(Vector3.up * 6, ForceMode.Impulse);
                animator.SetBool("IsHanging", false);
                IsHanging = false;
            }
        }

    }

    void OnAnimatorIK()
    {
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        animator.SetIKPosition(AvatarIKGoal.RightHand, hangingPos);
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
                    //If this never happens we cannot reach ledge
                    break;
                }
                Debug.Log(lastRayHitPoint);
                lastRayHitPoint = hitObj.point;
                playerRb.constraints = RigidbodyConstraints.FreezeAll;
                transform.position = lastRayHitPoint - new Vector3(0, 0.6f, 0) - (transform.forward * 0.2f);
                IsHanging = true;
                animator.SetBool("IsHanging", true);
                hangingPos = lastRayHitPoint;
            }
            playerRb.constraints = RigidbodyConstraints.FreezeAll;
            transform.position = lastRayHitPoint - new Vector3(0, 0.2f, 0) - (transform.forward * 10);
            animator.SetBool("IsHanging", true);
        }
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position + new Vector3(0, 0.2f, 0), -transform.up, 0.3f);
    }



}

