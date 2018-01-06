using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour {
    public Canvas invCanvas;
    private float timer = 0;
    private float fadeSpeed = 0.4f;
    private int fadeDir = -1;
    public CanvasGroup cg;

    void Awake()
    {
        cg = gameObject.GetComponent<CanvasGroup>();
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
        if (Input.GetButtonDown("Y") && timer <= 0)
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
}
