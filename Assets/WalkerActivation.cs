using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerActivation : MonoBehaviour {

    public WalkerScript[] walkers;

    

    IEnumerator OnTriggerEnter(Collider collider)
    {
        for (int i = 0; i < walkers.Length; i++)
        {
            
            walkers[i].startWalker = true;
            yield return new WaitForSeconds(1);

        }
    }
}
