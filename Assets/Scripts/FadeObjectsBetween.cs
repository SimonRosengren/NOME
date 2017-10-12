using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeObjectsBetween : MonoBehaviour {

    public Camera Camera;

    public GameObject Player;

    Vector3 playerV;
    Vector3 cameraV;

    List<RaycastHit> transObjects;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 playerV = Player.transform.position;
        Vector3 cameraV = Camera.transform.position;
        Vector3 direction = playerV-cameraV;

        RaycastHit[] hits;

        //AddAlpha();

        if (Physics.Linecast(cameraV, playerV))
        {
            hits = Physics.RaycastAll(transform.position, direction, Vector3.Distance(cameraV, playerV));
            Debug.DrawRay(transform.position, direction);
            
            
                for(int i = 0; i < hits.Length; i++)
                {
                Renderer rend = hits[i].transform.GetComponent<Renderer>();



                // Change the material of all hit colliders
                // to use a transparent shader.

                rend.material.shader = Shader.Find("Transparent/Diffuse");
                Color tempColor = rend.material.color;
                tempColor.a = 0.3F;
                rend.material.color = tempColor;
                
                foreach (RaycastHit g in transObjects)
                {
                    if (!g.Equals(hits[i]))
                    {
                        transObjects.Add(hits[i]);
                    }
                    
                }
                
                    
                }

            

        }
	}

    void AddAlpha()
    {
        if(transObjects.Count != 0)
        {
            for (int i = 0; i < transObjects.Count; i++)
            {
            
                        Renderer rend = transObjects[i].transform.GetComponent<Renderer>();
                        Color tempColor = rend.material.color;
                        tempColor.a += 0.01F;
                        rend.material.color = tempColor;

                        if (rend.material.color.a > 0)
                        {
                            transObjects.Remove(transObjects[i]);
                        }
                
            
            }

        }
    }
}
