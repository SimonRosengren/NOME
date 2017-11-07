using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerMovementForce : MonoBehaviour
{
    
    Rigidbody playerRb;
    AudioSource runSound;
    Vector3 hangingPos;
    Vector3 velocityAxis;
    LedgeCollsion ledgegrabArea;
    RaycastHit ObjectGrabbed;
    
    /*Movement vector*/
    float currentV;
    float currentH;

    bool pulling = false;
    public bool isDead = false;
    Color deathColor = new Color(1f, 1f, 1f, 1f);
    float deathFadeSpeed = 2f;
    float timeToRespawn;
    public int inReachOfBook = 0;
    float moveSpeed;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float acceleration = 10f;
    [SerializeField]
    private float maxspeed = 10f;
    [SerializeField]
    private float jumpForce = 10f;
    [SerializeField]
    private bool IsHanging = false;
    [SerializeField]
    GameObject gameHandler;
    [SerializeField]
    Image deathImage;
    float deathTimer = 2f;

    GameLogic gameLogic;

    void Awake()
    {
        
        playerRb = GetComponent<Rigidbody>();
        ledgegrabArea = GetComponentInChildren<LedgeCollsion>();
        gameLogic = gameHandler.GetComponent<GameLogic>();
        runSound = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        runSound.Play();
    }

    void FixedUpdate()
    {

        float xspeed = 0;
        xspeed = Input.GetAxisRaw("Horizontal");
        float zspeed = 0;
        zspeed = Input.GetAxisRaw("Vertical");




        velocityAxis = new Vector3(xspeed, 0, zspeed);

        velocityAxis = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up) * velocityAxis;



        Move(velocityAxis);
        animator.SetBool("isGrounded", IsGrounded());



        /*Do I NEED THIS!!?=*/
        if (velocityAxis.magnitude > 0 && !pulling && !ledgegrabArea.hanging)
        {
            transform.rotation = Quaternion.LookRotation(velocityAxis);
            runSound.UnPause();

        }
        else
            runSound.Pause();

        if (IsGrounded() && Input.GetButtonDown("Jump") && !pulling)
        {
            Debug.Log("jump");
            Vector3 newVel = playerRb.velocity;
            newVel.y = 0;
            playerRb.velocity = newVel;

            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetTrigger("isJumping");
            runSound.Pause();
        }
        if (Input.GetButtonDown("Grab") && !pulling)
        {
            TryGrab();
        }
        else if (Input.GetButtonDown("Grab") && pulling)
        {
            TryLettingGo();
        }
        //Constant updates of animation floats.------------------------
        Limitvelocity();

        //------------------------------------------------------------
        if (isDead)
        {
            handleDeath();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "climbTrigger")
        {
            Climb();
        }
        if (other.tag == "CheckPoint")
        {
            other.transform.GetComponent<CheckPoint>().SetAsLastCheckpoint();
        }
        if (other.tag == "DeathTrigger")
        {
            Die();
        }
    }


    void Move(Vector3 velocityAxis)
    {
        //Debug.Log(velocityAxis.magnitude);
        if (!isDead)
        {
            if (!ledgegrabArea.hanging)
            {
                playerRb.AddForce(velocityAxis.normalized * acceleration);
                moveSpeed = playerRb.velocity.magnitude;
                animator.SetFloat("MoveSpeed", velocityAxis.magnitude);
            }
            else /*So we can jump while hanging. Will probably be switched to an animation*/
            {
                if (!IsHanging)
                {
                    IsHanging = true;
                    playerRb.AddForce(velocityAxis.normalized * acceleration);
                    moveSpeed = playerRb.velocity.magnitude;
                    animator.SetFloat("MoveSpeed", velocityAxis.magnitude);
                }


                else /*So we can jump while hanging. Will probably be switched to an animation*/
                {
                    if (Input.GetButtonDown("Jump"))
                    {
                        Debug.Log("jump");
                        playerRb.constraints = RigidbodyConstraints.None;
                        playerRb.constraints = RigidbodyConstraints.FreezeRotation;
                        Vector3 newVel = playerRb.velocity;
                        newVel.y = 0;
                        playerRb.velocity = newVel;
                        playerRb.AddForce(Vector3.up * 1, ForceMode.Impulse);

                        animator.SetBool("IsHanging", false);
                        IsHanging = false;
                    }
                }
            }

        }
        Limitvelocity();

    }
    void Limitvelocity()
    {
        Vector2 xzVel = new Vector2(playerRb.velocity.x, playerRb.velocity.z);
        if (xzVel.magnitude > maxspeed)
        {
            xzVel = xzVel.normalized * maxspeed;
            playerRb.velocity = new Vector3(xzVel.x, playerRb.velocity.y, xzVel.y);
        }
        // animator.SetFloat("MoveSpeed", xzVel.magnitude);
    }

    /*The method, which is triggered by entering a climbTrigger, will cast Rays higher and higher and stop 
    when it no longer is hitting a climbable object. When the last ray misses I know that the last one hit the edge of the object. 
    I then move the player to this position. We need to play some kind of animation here, rather then just teleporting him to the new
    spot. */
    void Climb()
    {


    }

    bool IsGrounded()
    {
        Debug.DrawRay(transform.position + new Vector3(0, 0.2f, 0), -transform.up, Color.red);
        return Physics.Raycast(transform.position + new Vector3(0, 0.2f, 0), -transform.up, 0.5f);
    }

    void TryGrab()
    {
        Ray ray = new Ray(transform.position + new Vector3(0, 0.3f, 0), transform.forward);
        Physics.Raycast(ray, out ObjectGrabbed, 1);
        if (ObjectGrabbed.transform.tag == "grabable")
        {
            animator.SetBool("grabbingObj", true);
            pushableObject hitObjScript = ObjectGrabbed.transform.GetComponent<pushableObject>();
            hitObjScript.Grab(playerRb, ObjectGrabbed.point);
            pulling = true;


        }
    }

    void OnAnimatorIK()
    {
        if (pulling)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0.5f);
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0.5f);
            animator.SetIKPosition(AvatarIKGoal.RightHand, ObjectGrabbed.transform.position);
            animator.SetIKPosition(AvatarIKGoal.LeftHand, ObjectGrabbed.transform.position);
        }
    }

    void TryLettingGo()
    {
        if (pulling==true)
        {
            pushableObject hitObjScript = ObjectGrabbed.transform.GetComponent<pushableObject>();
            hitObjScript.LetGo();
            pulling = false;
        }
    }
    void Die()
    {
        isDead = true;
        timeToRespawn = deathTimer;
    }
    void handleDeath()
    {
        timeToRespawn -= Time.deltaTime;
        deathImage.color = Color.Lerp(deathImage.color, Color.black, deathFadeSpeed * Time.deltaTime);
        if (timeToRespawn <= 0)
        {
            isDead = false;
            transform.position = gameLogic.GetLastCheckPoint().position;
            transform.rotation = gameLogic.GetLastCheckPoint().rotation;
            deathImage.color = Color.clear;
        }
    }
}
