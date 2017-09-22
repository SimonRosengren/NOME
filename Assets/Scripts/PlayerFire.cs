using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour {

    public GameObject match;
    private GameObject newMatch;
    public bool matchLit = false;
    private Transform matchLocation;
    private float timer = 1;
    public Vector3 vec;
	// Use this for initialization
	void Start () {
        
        matchLocation = transform.Find("MatchSpawn");
        vec = matchLocation.position;
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
                newMatch = Instantiate(match, matchLocation.transform.position, Quaternion.identity, matchLocation.transform.parent) as GameObject;
                //newMatch.transform.position = matchLocation.transform.position;
                //newMatch.transform.parent = matchLocation;
                //matchLocation.transform.parent = transform;
                //newMatch.transform.position = matchLocation.localPosition;
                //newMatch.transform.position = matchLocation.transform.position;
                //newMatch.transform.position = new Vector3(matchLocation.position.x, matchLocation.position.y, matchLocation.position.z);
                timer = 1;
            }
            
        }      
		
	}
}
