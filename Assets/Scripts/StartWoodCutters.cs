using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWoodCutters : MonoBehaviour {

    public WoodCutterAnimScript[] woodcutters;

	void Start () {
		
	}
	

	void Update () {

	}

    IEnumerator OnTriggerEnter(Collider collider)
    {
        for (int i = 0; i < woodcutters.Length; i++)
        {

            woodcutters[i].startUp = true;
            yield return new WaitForSeconds(3);

        }
    }

   

}
