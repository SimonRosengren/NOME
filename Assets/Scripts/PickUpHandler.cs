using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpHandler : MonoBehaviour {

    //Should probably not be public
    public bool kitchenKey = false;
    public bool barnKey = false;
    public bool windowKey = false;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PickUp")
        {
            PickUp pickUp = other.GetComponent<PickUp>();
            string collectedItem = pickUp.Collect();
            UpdateCollections(collectedItem);
        }
    }


    /*If this gets to big, we should consider a database*/
    void UpdateCollections(string collectedItem)
    {
        switch (collectedItem)
        {
            case "KitchenKey":
                kitchenKey = true;
                break;
            case "BarnKey":
                barnKey = true;
                break;
            case "WindowKey":
                windowKey = true;
                break;
            default:
                break;
        }
    }
}
