using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectShootherScript : MonoBehaviour {

    public GameObject shootObj;
    projectileScript projectile;
    public float rateOfFire = 2;
    public int minForce = 10;
    public int maxForce = 20;
    float timer = 2;
    Vector3[] targets;


	// Use this for initialization
	void Start () {
        targets = new Vector3[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            targets[i] = transform.GetChild(i).position;
        }
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        if (timer >= rateOfFire)
        {
            timer = 0;
            GameObject test = Instantiate(shootObj, transform.position, Random.rotation);
            Vector3 target = targets[(int)Random.Range(0, transform.childCount)];
            Vector3 dir = Vector3.Normalize(target - transform.position);

            test.GetComponent<Rigidbody>().AddForce(dir * Random.Range(minForce, maxForce), ForceMode.VelocityChange);

        }
	}
}
