using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlammableObject : MonoBehaviour {

    public bool onFire = false;
    private float burnTimer = 5;
    public ParticleSystem fire;

	void Start ()
    {		
	}
	
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
        ParticleSystem test = Instantiate(fire, this.transform.position, this.transform.rotation);
        test.Play();
        test.transform.parent = this.transform;

        //PLAY ANIMATION
        if (!onFire)
        {
            onFire = true;
        }       
    }
}
