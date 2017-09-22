using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabPushDrag : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    GameObject getInteractiveObject(float range)
    {
        Vector3 positon = gameObject.transform.position + new Vector3(0, 0.5f);
        RaycastHit raycastHit;
        Vector3 target = Camera.main.transform.forward + new Vector3(0, 0.5f) * range;
        Debug.DrawRay(positon, target, Color.red);
        if(Physics.Linecast(positon, target, out raycastHit))
        {
            return raycastHit.collider.gameObject;
        }
        return null;
    }
	// Update is called once per frame
	void Update () {
        Debug.Log(getInteractiveObject(0.5f));
        
	}
}
