using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leverAnimScript : MonoBehaviour
{

    public Animator animLever;
    public Animator animLogKeeper;
    public GameObject[] logkeeper;
    void Start()
    {
        animLever = GetComponent<Animator>();


    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            animLever.Play("Lever_Pull_Animation");
        }
    }

    void OnTriggerExit(Collider collider)
    {
        foreach (GameObject g in logkeeper)
        {
            animLogKeeper = g.GetComponent<Animator>();
            animLogKeeper.Play("LogKeeper_Open");

        }
    }


}
