using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPrompt : MonoBehaviour {

    private Canvas canvas;
    private float fadeSpeed = 0.4f;
    private int fadeDir = -1;
    private CanvasGroup cg;

    private Transform target;
    private float speed = 1f;
    private Quaternion targetRotation;

        // Use this for initialization
    void Awake ()
    {
        cg = gameObject.GetComponent<CanvasGroup>();
        target = GameObject.FindGameObjectWithTag("MainCamera").transform;
        cg.alpha = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        cg.alpha += fadeDir * fadeSpeed * Time.deltaTime;
        cg.alpha = Mathf.Clamp01(cg.alpha);      
        
        if (cg.alpha > 0)
        {
            targetRotation = Quaternion.LookRotation(target.position - transform.position);
            float str = Mathf.Min(speed * Time.deltaTime, 1);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, str);
        }        
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            FadeIn();           
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            FadeOut();
        }
    }

    public float BeginFade(int direction)
    {
        fadeDir = direction;
        return (fadeSpeed);
    }

    public void FadeIn()
    {
        BeginFade(1);
    }

    public void FadeOut()
    {
        BeginFade(-1);
    }
}
