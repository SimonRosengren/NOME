using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerMovementForce : MonoBehaviour {

    Rigidbody playerRb;
    Vector3 hangingPos;

    /*Movement vector*/
    float currentV;
    float currentH;

    bool pulling = false;
    public bool isDead = false;
    Color deathColor = new Color(1f, 1f, 1f, 1f);
    float deathFadeSpeed = 2f;
    float timeToRespawn;

    [SerializeField] private Animator animator;
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float maxspeed = 10f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private bool IsHanging = false;
    [SerializeField] GameObject gameHandler;
    [SerializeField] Image deathImage;
    [SerializeField] float deathTimer = 2f;

    GameLogic gameLogic;

    void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        gameLogic = gameHandler.GetComponent<GameLogic>();
    }

    void FixedUpdate()
    {

        float xspeed = Input.GetAxisRaw("Horizontal");
        float zspeed = Input.GetAxisRaw("Vertical");

        Vector3 velocityAxis = new Vector3(xspeed, 0, zspeed);

        velocityAxis = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up) * velocityAxis;

        Move(velocityAxis);

        if (velocityAxis.magnitude > 0 && !pulling)
        {
            transform.rotation = Quaternion.LookRotation(velocityAxis);
        }

        if (IsGrounded() && Input.GetButtonDown("Jump") && !pulling)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        if (Input.GetButtonDown("Grab") && !pulling)
        {
            TryGrab();
        }
        if (Input.GetKeyDown(KeyCode.K) && pulling)
        {
            TryLettingGo();
        }
        Limitvelocity();

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
        if (!isDead)
        {
            if (!IsHanging)
            {
                playerRb.AddForce(velocityAxis.normalized * acceleration);
                animator.SetFloat("MoveSpeed", playerRb.velocity.magnitude);
            }
            else /*So we can jump while hanging. Will probably be switched to an animation*/
            {
                if (Input.GetButtonDown("Jump"))
                {
                    playerRb.constraints = RigidbodyConstraints.None;
                    playerRb.constraints = RigidbodyConstraints.FreezeRotation;
                    playerRb.AddForce(Vector3.up * 6, ForceMode.Impulse);
                    animator.SetBool("IsHanging", false);
                    IsHanging = false;
                }
            }
        }
    }
    void Limitvelocity()
    {
        Vector2 xzVel = new Vector2(playerRb.velocity.x, playerRb.velocity.z);
        if (xzVel.magnitude > maxspeed)
        {
            xzVel = xzVel.normalized * maxspeed;
            playerRb.velocity = new Vector3(xzVel.x, playerRb.velocity.y, xzVel.y);
        }
        animator.SetFloat("MoveSpeed", xzVel.magnitude);
    }

    /*The method, which is triggered by entering a climbTrigger, will cast Rays higher and higher and stop 
    when it no longer is hitting a climbable object. When the last ray misses I know that the last one hit the edge of the object. 
    I then move the player to this position. We need to play some kind of animation here, rather then just teleporting him to the new
    spot. */
    void Climb()
    {
        RaycastHit hitObj;
        Vector3 rayOriginOffset = new Vector3(0, -0.2f, 0);
        Ray ray = new Ray(transform.position + rayOriginOffset, transform.forward);
        Physics.Raycast(ray, out hitObj, 1);

        Vector3 lastRayHitPoint = transform.position;
        Physics.Raycast(ray, out hitObj, 1);
        if (hitObj.collider != null)
        {
            for (int i = 0; i < 12; i++)
            {
                rayOriginOffset.y += 0.1f;
                Ray rayTest = new Ray(transform.position + rayOriginOffset, transform.forward);
                Physics.Raycast(rayTest, out hitObj, 1);
                /*Vi får error här pga att vi kollar hittobj.collider även om null. Vet ej lösning*/
                if (hitObj.collider.tag != "climbableObject")
                {
                    //If this never happens we cannot reach ledge
                    break;
                }
                lastRayHitPoint = hitObj.point;
                playerRb.constraints = RigidbodyConstraints.FreezeAll;
                transform.position = lastRayHitPoint - new Vector3(0, 0.0f, 0) - (transform.forward * 0.2f);
                IsHanging = true;
                animator.SetBool("IsHanging", true);
                hangingPos = lastRayHitPoint;
            }
            playerRb.constraints = RigidbodyConstraints.FreezeAll;
            transform.position = lastRayHitPoint - new Vector3(0, 0.0f, 0) - (transform.forward * 0.2f);
            animator.SetBool("IsHanging", true);
        }
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position + new Vector3(0, 0.1f, 0), -transform.up, 0.5f);
    }

    void TryGrab()
    {
        RaycastHit hitObj;
        Ray ray = new Ray(transform.position + new Vector3(0, 0.1f, 0), transform.forward);
        Physics.Raycast(ray, out hitObj, 1);
        
        if (hitObj.transform.tag == "grabable")
        {
            pushableObject hitObjScript = hitObj.transform.GetComponent<pushableObject>();
            hitObjScript.Grab(playerRb, hitObj.point);
            pulling = true;
        }
    }
    void TryLettingGo()
    {
        RaycastHit hitObj;
        Ray ray = new Ray(transform.position + new Vector3(0, 0.1f, 0), transform.forward);
        Physics.Raycast(ray, out hitObj, 1);

        if (hitObj.transform.tag == "grabable")
        {
            pushableObject hitObjScript = hitObj.transform.GetComponent<pushableObject>();
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
            deathImage.color = Color.clear;
        }
    }
}
