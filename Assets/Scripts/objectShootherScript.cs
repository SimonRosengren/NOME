using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectShootherScript : MonoBehaviour
{

    [SerializeField] Transform player;

    public bool isActive = false;
    public bool targetPlayer;
    public GameObject shootObj;
    projectileScript projectile;
    public float rateOfFire = 2;
    public int minForce = 10;
    public int maxForce = 20;
    float timer = 2;
    Vector3[] targets;
    public Animator anim;


    // Use this for initialization
    void Start()
    {
        targets = new Vector3[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            targets[i] = transform.GetChild(i).position;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Player")
        {            
            isActive = true;
            anim.SetBool("isActive", true);
        }
    }

    //void OnTriggerExit(Collider collider)
    //{
    //    if (collider.tag == "Player")
    //    {
    //        isActive = false;
    //        anim.SetBool("isActive", false);
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            timer += Time.deltaTime;

            if (timer >= rateOfFire)
            {
                if (!targetPlayer)
                {
                    timer = 0;
                    GameObject test = Instantiate(shootObj, transform.position, Random.rotation);
                    Vector3 target = targets[(int)Random.Range(0, transform.childCount)];
                    Vector3 dir = Vector3.Normalize(target - transform.position);

                    test.GetComponent<Rigidbody>().AddForce(dir * Random.Range(minForce, maxForce), ForceMode.VelocityChange);
                }

                else
                {
                    timer = 0;
                    GameObject test = Instantiate(shootObj, transform.position, Random.rotation);
                    Vector3 target = player.position;
                    Vector3 dir = Vector3.Normalize(target - transform.position);

                    test.GetComponent<Rigidbody>().AddForce(dir * Random.Range(minForce, maxForce), ForceMode.VelocityChange);
                }
            }
        }
    }
}
