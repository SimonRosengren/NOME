using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabPushDrag : MonoBehaviour
{

    // Use this for initialization
    public float distance = 1f;
    public LayerMask PushObjectMask;

    GameObject pushableObject;
    RaycastHit hitObject;


    // Update is called once per frame
    void Update()
    {
        Ray hit = new Ray(transform.position + (Vector3.up / 2), transform.forward + (Vector3.up / 2));
        Physics.Raycast(hit, out hitObject, 1);

        if(hitObject.collider.tag == "pushable" && Input.GetKey(KeyCode.E))
        {
            pushableObject = hitObject.transform.gameObject;

            pushableObject.GetComponent<Rigidbody>().AddForce(this.GetComponent<PlayerMovement>().velocityAxis.normalized * this.GetComponent<PlayerMovement>().acceleration);
            //Move the object 
        }
     
        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position + (Vector3.up / 2), transform.position + transform.forward + (Vector3.up/2) * distance);
    }
}
