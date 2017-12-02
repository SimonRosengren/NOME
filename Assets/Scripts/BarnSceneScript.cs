using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarnSceneScript : MonoBehaviour {

    [SerializeField] LoadIndoorScene sceneLoader;
    [SerializeField] Animator cowAnimator;

    [SerializeField] GameObject leaveBarnTooEarlyTrigger;
    [SerializeField] GameObject cowThirstyTrigger;


    // Use this for initialization
    void Start () {
        sceneLoader.isActive = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        /*The bucket is grabbable*/
        if (other.tag == "Grabable")
        {
            sceneLoader.isActive = true;
            cowAnimator.SetBool("isDead", true);
            leaveBarnTooEarlyTrigger.GetComponent<DialogeImporter>().isActive = false;
            cowThirstyTrigger.GetComponent<DialogeImporter>().isActive = false;
            Destroy(leaveBarnTooEarlyTrigger);
            Destroy(cowThirstyTrigger);

        }
    }
}
