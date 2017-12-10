using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodCutterAnimScript : MonoBehaviour
{

    Animator anim;
    public bool startUp;
    public float mainSpeed;
    public float startStopSpeed;

    public bool walkToTarget = false;
    public Transform endTransForm;

    public bool targetPlayer;
    public Transform player;
    public Transform idleTarget;

    public GameObject shootObj;

    Vector3 distPlayerToIdle;
    Vector3 target;
    public float speed = 1;

    public AudioSource audioS;
    public AudioClip walking;
    public AudioClip saw;



    private float angle = 10;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        target = idleTarget.transform.position;


    }


   

    private void FireAt(Vector3 target, GameObject projectile)
    {
        Vector3 toTarget = target - transform.position;

        // Set up the terms we need to solve the quadratic equations.
        float gSquared = Physics.gravity.sqrMagnitude;
        float b = speed * speed + Vector3.Dot(toTarget, Physics.gravity);
        float discriminant = b * b - gSquared * toTarget.sqrMagnitude;

        // Check whether the target is reachable at max speed or less.
        if (discriminant < 0)
        {
            speed = speed * 2;
            b = speed * speed + Vector3.Dot(toTarget, Physics.gravity);
            discriminant = b * b - gSquared * toTarget.sqrMagnitude;
            // Target is too far away to hit at this speed.
            // Abort, or fire at max speed in its general direction?
        }

        float discRoot = Mathf.Sqrt(discriminant);

        // Highest shot with the given max speed:
        float T_max = Mathf.Sqrt((b + discRoot) * 2f / gSquared);

        // Most direct shot with the given max speed:
        float T_min = Mathf.Sqrt((b - discRoot) * 2f / gSquared);

        // Lowest-speed arc available:
        float T_lowEnergy = Mathf.Sqrt(Mathf.Sqrt(toTarget.sqrMagnitude * 4f / gSquared));

        float T = T_min;// choose T_max, T_min, or some T in-between like T_lowEnergy

        // Convert from time-to-hit to a launch velocity:
        Vector3 velocity = toTarget / T - Physics.gravity * T / 2f;

        // Apply the calculated velocity (do not use force, acceleration, or impulse modes)
        projectile.GetComponent<Rigidbody>().AddForce(velocity, ForceMode.VelocityChange);
    }

    void ShootChuck()
    {
        distPlayerToIdle = this.transform.position - player.position;
        if (distPlayerToIdle.magnitude < 10)
        {

            target = player.transform.position;
           // Debug.Log("playerTarget");
        }
        else
        {
            //Debug.Log("Target");
        }
        target = idleTarget.transform.position;

        //FireAtPoint(target);
        GameObject projectile = Instantiate(shootObj, transform.position, transform.rotation);
        FireAt(target, projectile);
        //Vector3 dir = (target - transform.position).normalized;

        //test.GetComponent<Rigidbody>().AddForce(dir * force);
      

    }

    void Animations()
    {
        Vector3 direction = endTransForm.position - this.transform.position;
        direction.y = 0;
        float step = mainSpeed * Time.deltaTime;
        float step2 = startStopSpeed * Time.deltaTime;

        if (walkToTarget && startUp)
        {

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                anim.SetBool("isActive", true);
                anim.SetBool("Start_Walking", true);
            }


            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Start_Walking_") && direction.magnitude > 1)
            {
                transform.position = Vector3.MoveTowards(transform.position, endTransForm.position, step2);

            }
            else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Walking_Loop_") && direction.magnitude > 1)
            {
                transform.position = Vector3.MoveTowards(transform.position, endTransForm.position, step);

            }

            else if (direction.magnitude < 1)
            {
                anim.SetBool("isActive", false);
                anim.SetBool("Start_Walking", false);
                anim.SetBool("StopWalking", true);
                anim.SetBool("StartCutting", true);

            }
        }

        if(startUp && !walkToTarget)
        {
            anim.SetBool("isActive", true);
            anim.SetBool("StartCutting", true);

        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Cutting_lpop_"))
            {

                ShootChuck();
            }
    }
    // Update is called once per frame
    void Update()
    {
        Animations();


    }

    void playWalking()
    {
        audioS.PlayOneShot(walking);
    }

    void playSaw()
    {
        audioS.PlayOneShot(saw);
    }
   
}
