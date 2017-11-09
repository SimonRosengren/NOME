using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Menu : MonoBehaviour {
    public Canvas mainCanvas;
    private bool delayLoad = false;
    private Fading fading;
    public string SceneToLoad;

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
        SceneManager.LoadScene(SceneToLoad);        
    }

    public void DelayedLoad()
    {
        delayLoad = true;        
    }

    public void Exit()
    {
        Application.Quit();
    }
}
