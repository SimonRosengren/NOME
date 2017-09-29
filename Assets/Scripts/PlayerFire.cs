using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour {

    public GameObject match;
    private GameObject newMatch;
    public bool matchLit = false;
    private Transform matchLocation;
    private float timer = 1;
    
	// Use this for initialization
	void Start () {
        
        matchLocation = transform.Find("RightHand");
        
	}
	
	// Update is called once per frame
	void Update () {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.F) && timer <= 0)
        {
            if (matchLit)
            {
                Destroy(newMatch);
                matchLit = false;
                timer = 1;
            }
            else if(!matchLit)
            {
                matchLit = true;
                newMatch = Instantiate(match, matchLocation.position, Quaternion.identity) as GameObject;
                newMatch.transform.parent = matchLocation;
                
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
