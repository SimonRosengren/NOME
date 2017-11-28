using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerScript : MonoBehaviour
{


    public AudioSource walkerAudio;
    public AudioSource walkerHorn;
    Animator walkerAnim;
    public Transform target;
    public float walkSpeed;
    float step;


    // Use this for initialization
    void Start()
    {
        walkerAnim = this.GetComponent<Animator>();
        walkerAnim.SetBool("StartWalking", true);

    }

    void PlayWalk()
    {
        walkerAudio.Play();
    }

    void PlayHorn()
    {
        walkerHorn.Play();

    }

    // Update is called once per frame
    void Update()
    {
        step = walkSpeed * Time.deltaTime;
        this.transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        

        if(this.transform == target)
        {
            Destroy(this);
        }
    }
}
