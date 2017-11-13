using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodCutterAnimScript : MonoBehaviour
{

    public Transform endTransForm;
    public Animator anim;
    public bool start;
    
    public float speed;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = endTransForm.position - this.transform.position;
        direction.y = 0;
        float step = speed * Time.deltaTime;



        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Start_Walking_") && direction.magnitude > 1 ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("Walking_Loop_") && direction.magnitude > 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, endTransForm.position, step);

        }

        else if(direction.magnitude < 1)
        { 
            anim.SetBool("isActive", false);
            anim.SetBool("StartWalk", false);
            anim.SetBool("StopWalking", true);
            anim.SetBool("StartCutting", true);

        }

        
        


    }
}
