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


    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        target = idleTarget.transform.position;


    }

    //void OnTriggerEnter(Collider collider)
    //{
    //    if (collider.tag == "Player")
    //    {
    //        targetPlayer = true;
    //        Debug.Log("playerTarget");

    //    }
    //}

    //void OnTriggerExit(Collider collider)
    //{
    //    if (collider.tag == "Player")
    //    {
    //        targetPlayer = false;
    //        Debug.Log("Target");
    //    }
    //}

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


        GameObject test = Instantiate(shootObj, transform.position, transform.rotation);
        Vector3 dir = Vector3.Normalize(target - transform.position);

        Rigidbody rb = test.GetComponent<Rigidbody>();/*.AddForce(dir * force, ForceMode.Impulse);*/
        rb.velocity = dir * force;
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
