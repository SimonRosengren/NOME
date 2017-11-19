using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodCutterAnimScript : MonoBehaviour
{

    Animator anim;
    public bool start;
    public float speed;
    public float startStopSpeed;

    public bool walkFirst = false;
    public bool targetPlayer;

    public GameObject shootObj;
    projectileScript projectile;
    public int force = 60;

    public Transform endTransForm;
    public Transform idleTarget;
    public Transform player;


    Vector3 distance;
    Vector3 target;


    [SerializeField]
    [Range(10f, 80f)]
    private float angle = 30f;


    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        target = idleTarget.transform.position;


    }




    private Vector3 BallisticVelocity(Vector3 destination, float angle)
    {
        Vector3 dir = destination - transform.position; // get Target Direction
        float height = dir.y; // get height difference
        dir.y = 0; // retain only the horizontal difference
        float dist = dir.magnitude; // get horizontal direction
        float a = angle * Mathf.Deg2Rad; // Convert angle to radians
        dir.y = dist * Mathf.Tan(a); // set dir to the elevation angle.
        dist += height / Mathf.Tan(a); // Correction for small height differences

        // Calculate the velocity magnitude
        float velocity = Mathf.Sqrt(dist * Physics.gravity.magnitude / Mathf.Sin(2 * a));
        return velocity * dir.normalized; // Return a normalized vector.
    }

    private void FireCannonAtPoint(Vector3 point)
    {
        var velocity = BallisticVelocity(point, angle);
       // Debug.Log("Firing at " + point + " velocity " + velocity);

        GameObject test = Instantiate(shootObj, transform.position, transform.rotation);
        test.transform.position = transform.position;
        test.GetComponent<Rigidbody>().velocity = velocity;
    }

    void ShootChuck()
    {
        distance = idleTarget.position - player.position;
        if (distance.magnitude < 20)
        {
           
            target = player.transform.position;
            Debug.Log("playerTarget");
        }
        else
        {
            target = idleTarget.transform.position;
            Debug.Log("Target");
        }

        FireCannonAtPoint(target);
        //GameObject test = Instantiate(shootObj, transform.position, transform.rotation);
        //Vector3 dir = Vector3.Normalize(target - transform.position);

        //test.GetComponent<Rigidbody>().AddForce(dir * force, ForceMode.Impulse);

    }

    void Animations()
    {
        Vector3 direction = endTransForm.position - this.transform.position;
        direction.y = 0;
        float step = speed * Time.deltaTime;
        float step2 = startStopSpeed * Time.deltaTime;

        if (walkFirst && start)
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

        if(start && !walkFirst)
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
}
