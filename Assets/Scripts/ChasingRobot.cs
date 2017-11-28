﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChasingRobot : MonoBehaviour
{

    Transform player;               // Reference to the player's position.
    NavMeshAgent nav;               // Reference to the nav mesh agent.
    public bool active = false;
    Rigidbody rb;
    NavMeshPath navPath;
    Vector3 direction;
    public bool isDead = false;

    [SerializeField]
    float moveSpeed = 10f;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        navPath = new NavMeshPath();
    }
    void FixedUpdate()
    {
        if (active && !isDead)
        {
            Vector3 targetPostition = new Vector3(player.position.x,
                                       this.transform.position.y,
                                       player.position.z);
            this.transform.LookAt(targetPostition);
            Vector3 dir = Vector3.Normalize(player.position - transform.position);
            rb.AddForce(dir * Time.deltaTime * moveSpeed, ForceMode.Impulse);
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
