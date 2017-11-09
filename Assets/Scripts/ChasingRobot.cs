using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChasingRobot : MonoBehaviour {

    Transform player;               // Reference to the player's position.
    NavMeshAgent nav;               // Reference to the nav mesh agent.
    public bool active = false;
    Rigidbody rb;
    NavMeshPath navPath;
    Vector3 direction;
    public bool isDead = false;

    [SerializeField] float moveSpeed = 10f;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        navPath = new NavMeshPath();        
    }


    void Update()
    {
        //if (!isDead)
        //{
        //    nav.enabled = true;
        //    nav.CalculatePath(player.position, navPath);            
        //    int i = 1;
        //    while (i < navPath.corners.Length)
        //    {
        //        if (Vector3.Distance(transform.position, navPath.corners[i]) > 0.5f)
        //        {
        //            direction = navPath.corners[i] - transform.position;
        //            break;
        //        }
        //        i++;
        //    }
        //}
        //if (isDead)
        //{
        //    rb.constraints = RigidbodyConstraints.None;
        //}
    }
    void FixedUpdate()
    {
        if (active && !isDead)
        {
            Vector3 dir = Vector3.Normalize(player.position - transform.position);
            rb.AddForce(dir * Time.deltaTime * moveSpeed, ForceMode.Impulse);
            
            //transform.rotation = Quaternion.LookRotation(new Vector3(player.position.x, transform.position.y, player.position.z));
            
        }

        //if (!isDead)
        //{
        //    nav.enabled = false;
        //    rb.AddForce(direction * moveSpeed * Time.deltaTime, ForceMode.Impulse);
        //    //rb.AddForce(Vector3.Normalize(player.position - transform.position) * moveSpeed * Time.deltaTime, ForceMode.Impulse);
        //    transform.rotation = Quaternion.LookRotation(rb.velocity);
        //}        
    }

    public void SetToActive()
    {
        active = true;
    }
    public void Deactivate()
    {
        active = false;
    }
}
