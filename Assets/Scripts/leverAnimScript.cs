using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leverAnimScript : MonoBehaviour
{
    public Animator animLever;
    public Animator animOther;
    public GameObject[] logKeeper;
    public string otherAnimStateName;
    public AudioSource audioS, audioLever, audioGenerator;
    public AudioClip audioC, audioCLever, audioCGenerator;

    void Start()
    {
        animLever = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            animLever.Play("Lever_Pull_Animation");
            audioLever.PlayOneShot(audioCLever);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        Invoke("Playdelay", 1);
        Invoke("WoodCutterDelay", 20);
    }
    void playLogKeeperSound()
    {
        audioS.PlayOneShot(audioC);
    }

    void Playdelay()
    {
        audioGenerator.PlayOneShot(audioCGenerator);

    }

    void WoodCutterDelay()
    {
        foreach (GameObject g in logKeeper)
        {
            animOther = g.GetComponent<Animator>();
            animOther.Play(otherAnimStateName);
        }

    }
}
