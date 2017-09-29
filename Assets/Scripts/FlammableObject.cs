using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlammableObject : MonoBehaviour {
    public bool onFire = false;
    private float burnTimer = 5;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (onFire)
        {
            burnTimer -= Time.deltaTime;
            if (burnTimer <= 0)
            {
                //Instantiate(Ash, transform.postition, Quaternion.identity)
                Destroy(this.gameObject);                
            }
        }
		
	}
    void SetFire()
    {
        //PLAY ANIMATION
        if (!onFire)
        {
            onFire = true;
        }       
    }
}
