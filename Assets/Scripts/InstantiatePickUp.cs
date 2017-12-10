using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePickUp : MonoBehaviour {

    [SerializeField] PickUp pickUp;
    [SerializeField] Transform dropPoint;


	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DropPickUp()
    {
        Invoke("instatiatePickUp", 2);

    }
    public void instatiatePickUp()
    {
        Instantiate(pickUp, dropPoint);
    }
}
