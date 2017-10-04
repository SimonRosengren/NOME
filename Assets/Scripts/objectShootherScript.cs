using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectShootherScript : MonoBehaviour {

    public GameObject shootObj;
    public float rateOfFire;
    float timer = 2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer >= rateOfFire)
        {
            Instantiate(shootObj, transform.position, transform.rotation);
            timer = 0;
        }
	}
}
