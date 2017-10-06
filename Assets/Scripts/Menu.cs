using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Menu : MonoBehaviour {
    public Canvas mainCanvas;
    private bool delayLoad = false;
    private float timer = 0;
    private Fading fading;

    void Awake()
    {
        fading = GetComponent<Fading>();
        //mainCanvas.enabled = false;
    }
	// Use this for initialization
    void Update()
    {
        if (delayLoad)
        {
            if (fading.alpha >= 1)
            {
                delayLoad = false;
                LoadOn();
            }
            //timer -= Time.deltaTime;
            //if (timer <= 0)
            //{
            //    LoadOn();
            //}
        }
    }
	public void LoadOn()
    {
        Application.LoadLevel(1);        
    }
    public void DelayedLoad()
    {
        delayLoad = true;        
        //timer = 4;
    }
}
