using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour {

    public GameObject match;
    public ParticleSystem fireParticleSystem;
    private GameObject newMatch;
    public bool matchLit = false;
    public Transform matchLocation;
    private Transform firePosition;
    private float timer = 1;
    
	// Use this for initialization
	void Start () {
        
        //matchLocation = transform.Find("RightHand");
        firePosition = transform.Find("FirePos");
	}
	
	// Update is called once per frame
	void Update () {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        /*Change to button*/
        if (Input.GetKey(KeyCode.F) && timer <= 0)
        {
            if (matchLit)
            {
                Destroy(newMatch);
                matchLit = false;
                timer = 1;
                fireParticleSystem.Stop();

            }
            else if(!matchLit)
            {
                matchLit = true;
                newMatch = Instantiate(match, matchLocation.position, Quaternion.identity) as GameObject;
                newMatch.transform.parent = matchLocation;
                /*The fire should be dealt with in another way. Get top of match for transform instead of new empty object*/
                fireParticleSystem = Instantiate(fireParticleSystem, firePosition.position, firePosition.rotation);
                //ParticleSystem mPSystem = Instantiate(fireParticleSystem, firePosition.position, firePosition.rotation);
                fireParticleSystem.transform.parent = this.transform;
                timer = 1;
            }
            
        }
      
		
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "flammable" || other.tag == "ice")
        {
            other.SendMessageUpwards("SetFire");
        }
    }
}
