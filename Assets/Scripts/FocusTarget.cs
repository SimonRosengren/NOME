using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusTarget : MonoBehaviour {
    
    public GameObject target;
    public float panSpeed;
    public float panTime;

    CameraScriptFree camScript;          
    bool cutsceneDone = false;
    bool inCutscene = false;
    float timer;

    void Start()
    {
        GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
        camScript = cam.GetComponent<CameraScriptFree>();
    }
    void Update ()
    {
        if (inCutscene)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                camScript.Target = player.transform.Find("CameraTarget").gameObject;
                camScript.cameraSpeed = 10f;
                inCutscene = false;
                cutsceneDone = true;
            }
        }
	}

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && !cutsceneDone)
        {
            camScript.cameraSpeed = panSpeed;
            camScript.Target = target;
            timer = panTime;
            inCutscene = true;
        }        
    }
}
