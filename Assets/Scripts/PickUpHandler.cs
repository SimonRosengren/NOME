using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpHandler : MonoBehaviour {

    //Should probably not be public
    private bool kitchenKey = false;
    private bool paperclip = false;
    private bool rope = false;
    private GameObject inventory;

	// Use this for initialization
	void Start ()
    {		
	}
	
	// Update is called once per frame
	void Update ()
    {		
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
                inventory.transform.Find("Key").GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("Images/key_found");
                break;

            case "VacuumKey":
                paperclip = true;
                inventory.transform.Find("Paperclip").GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("Images/paperclip_found");
                break;

            case "Rope":
                rope = true;
                inventory.transform.Find("Rope").GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("Images/rope_found");
                break;

            default:
                break;
        }
    }
}
