using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChasingRobot : MonoBehaviour {

    Transform player;               // Reference to the player's position.
    NavMeshAgent nav;               // Reference to the nav mesh agent.
    bool active = false;
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
        if (!isDead)
        {
            nav.enabled = true;
            nav.CalculatePath(player.position, navPath);
            int i = 1;
            while (i < navPath.corners.Length)
            {
                if (Vector3.Distance(transform.position, navPath.corners[i]) > 0.5f)
                {
                    direction = navPath.corners[i] - transform.position;
                    break;
                }
                i++;
            }
        }
    }
    void FixedUpdate()
    {
        if (!isDead)
        {
            nav.enabled = false;
            rb.AddForce(direction * moveSpeed * Time.deltaTime, ForceMode.Impulse);
        }

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
