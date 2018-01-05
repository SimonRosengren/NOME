using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapult_player : MonoBehaviour {

    float timeLeft = 3;
    public float cForce = 7;
    [SerializeField] Light spotlight;
    [SerializeField] float lightIncrease;

    // Use this for initialization
    void Start ()
    {		
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == GameObject.FindGameObjectWithTag("Player"))
        {
            Rigidbody catapultObject = other.gameObject.GetComponent<Rigidbody>();
            timeLeft -= Time.deltaTime;
            if (spotlight.gameObject.activeInHierarchy == false)
            {
                spotlight.gameObject.SetActive(true);
            }
            spotlight.intensity += lightIncrease * Time.deltaTime;
            Debug.Log(timeLeft);
            if (timeLeft < 0)
            {
                Debug.Log("launch");
                catapultObject.AddForce(transform.up * cForce);            
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        timeLeft = 3;
        if (spotlight.gameObject.activeInHierarchy == true)
        {
            spotlight.intensity = 1;
            spotlight.gameObject.SetActive(false);
        }
    }
}
