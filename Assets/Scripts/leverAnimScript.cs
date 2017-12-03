using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leverAnimScript : MonoBehaviour
{
    public Animator animLever;
    public Animator animOther;
    public GameObject[] logKeeper;
    public string otherAnimStateName;
    public AudioSource audioS;
    public AudioClip audioC;

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
        foreach (GameObject g in logKeeper)
        {
            animOther = g.GetComponent<Animator>();
            animOther.Play(otherAnimStateName);
        }
    }
    void playLogKeeperSound()
    {
        audioS.PlayOneShot(audioC);
    }
}
