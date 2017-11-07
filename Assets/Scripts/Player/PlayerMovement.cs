using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody playerRb;
    Vector3 velocityAxis;
    LedgeCollsion ledgeGrabArea;
    GrabObject grabObj;
    Animator animator;
    RaycastHit grabbedObj;
    BookHandler bookHandler;

    bool isDead;

    [SerializeField] AudioSource runSound;
    [SerializeField] float acceleration;
    [SerializeField] float jumpForce;
    [SerializeField] float maxMoveSpeed;

    public int inReachOfBook = 0;


    void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        ledgeGrabArea = GetComponentInChildren<LedgeCollsion>();
        grabObj = GetComponent<GrabObject>();
        animator = GetComponent<Animator>();
        bookHandler = GetComponent<BookHandler>();
        isDead = false;
        runSound.Play();
    }

    void Update()
    {
        HandleInput();
        animator.SetBool("isGrounded", IsGrounded());
    }

    void FixedUpdate()
    {
        Move();
        Limitvelocity();
    }

    void Move()
    {

        if (!isDead && !ledgeGrabArea.hanging)
        {
            playerRb.AddForce(velocityAxis.normalized * acceleration);
            animator.SetFloat("MoveSpeed", velocityAxis.magnitude);
        }
        if (velocityAxis.magnitude > 0 && !isGrabbing() && !isHanging())
        {
            transform.rotation = Quaternion.LookRotation(velocityAxis);
        }

        if (velocityAxis.magnitude > 0 && IsGrounded())
        {
            runSound.UnPause();
        }
        else
            runSound.Play();

    }

    void Jump()
    {
        if (IsGrounded() && !isHanging() && !isGrabbing())
        {
            //Set y-velocity to zero to prevent "super jumps"
            Vector3 newVel = playerRb.velocity;
            newVel.y = 0;
            playerRb.velocity = newVel;

            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetTrigger("isJumping");
            runSound.Pause();
        }
    }

    void HandleInput()
    {
        float xspeed = Input.GetAxisRaw("Horizontal");
        float zspeed = Input.GetAxisRaw("Vertical");
        velocityAxis = Quaternion.AngleAxis(
            Camera.main.transform.eulerAngles.y,
            Vector3.up) * new Vector3(xspeed, 0, zspeed);

        if (Input.GetButtonDown("Jump"))
            Jump();

        if (Input.GetButtonDown("MainAction"))
        {
            Grab();
            ReadBook();
        }      
    }

    void ReadBook()
    {
        if (!bookHandler.isActive && !isGrabbing() && inReachOfBook != 0)
        {
            bookHandler.ShowBook(inReachOfBook);
        }
        else
            bookHandler.CloseBook();
    }

    void Grab()
    {
        grabObj.Grab();
    }

    void Limitvelocity()
    {
        Vector2 xzVel = new Vector2(playerRb.velocity.x, playerRb.velocity.z);
        if (xzVel.magnitude > maxMoveSpeed)
        {
            xzVel = xzVel.normalized * maxMoveSpeed;
            playerRb.velocity = new Vector3(xzVel.x, playerRb.velocity.y, xzVel.y);
        }
    }

    bool IsGrounded()
    {
        return Physics.Raycast(
            transform.position + new Vector3(0, 0.2f, 0),
            -transform.up, 0.5f);        
    }
    bool isHanging()
    {
        return ledgeGrabArea.hanging;
    }
    bool isGrabbing()
    {
        return grabObj.isGrabbing;
    }
}

