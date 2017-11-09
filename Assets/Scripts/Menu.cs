using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Menu : MonoBehaviour {
    public Canvas mainCanvas;
    private bool delayLoad = false;
    private Fading fading;

    void Awake()
    {
        fading = GetComponent<Fading>();
    }

    void Update()
    {
        if (delayLoad)
        {
            if (fading.alpha >= 1)
            {
                delayLoad = false;
                LoadOn();
            }
        }
    }

	public void LoadOn()
    {
        Application.LoadLevel(1);        
    }

    public void DelayedLoad()
    {
        delayLoad = true;        
    }
}
