using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance = null)
            {
                GameObject go = new GameObject("GameManager");
                go.AddComponent<GameManager>();
            }
            return _instance;
        }
    }
	// Use this for initialization

    void Awake()
    {
        _instance = this;
    }
	void Start () 
    {
        
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}
}
