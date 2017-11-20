using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWoodCutters : MonoBehaviour {

    public WoodCutterAnimScript[] woodcutters;

	void Start () {
		
	}
	

	void Update () {

	}

    void OnTriggerEnter(Collider collider)
    {
        for (int i = 0; i < woodcutters.Length; i++)
        {
            woodcutters[i].startUp = true;
        }
    }
}
