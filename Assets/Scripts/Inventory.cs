using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour {
    public Canvas invCanvas;
    private float timer = 0;
    private float fadeSpeed = 0.4f;
    //public float alpha = 1.0f;
    private int fadeDir = -1;
    public CanvasGroup cg;
	// Use this for initialization

    void Awake()
    {
        cg = gameObject.GetComponent<CanvasGroup>();
        //invCanvas.enabled = false;
    }
	void Start () 
    {
		
	}
	void Update()
    {
        cg.alpha += fadeDir * fadeSpeed * Time.deltaTime;
        cg.alpha = Mathf.Clamp01(cg.alpha);
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.I) && timer <= 0)
        {
            FadeIn();
            timer = 1;
        }
        if (cg.alpha == 1)
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
	// Update is called once per frame
    //void Update () 
    //{
    //    if (timer >= 0)
    //    {
    //        timer -= Time.deltaTime;
    //    }
    //    if (Input.GetButton("Idunno") || Input.GetKey(KeyCode.V) && timer <= 0)
    //    {

    //        timer = 1;
    //    }
    //}
}
