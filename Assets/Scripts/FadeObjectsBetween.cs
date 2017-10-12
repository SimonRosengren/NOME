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
        transObjects = new List<RaycastHit>();
	}
	
	// Update is called once per frame
	void Update () {
        playerV = Player.transform.position;
        cameraV = Camera.transform.position;
        Vector3 direction = playerV-cameraV;

        RaycastHit[] hits;

        AddAlpha();

        if (Physics.Linecast(cameraV, playerV))
        {
            hits = Physics.RaycastAll(transform.position, direction, Vector3.Distance(cameraV, playerV));
            Debug.DrawRay(transform.position, direction);
            
            
                for(int i = 0; i < hits.Length; i++)
                {
                    if (hits[i].transform.gameObject!=Player)
                    {
                        Renderer rend = hits[i].transform.GetComponent<Renderer>();



                        // Change the material of all hit colliders
                        // to use a transparent shader.

                        rend.material.shader = Shader.Find("Transparent/Diffuse");
                        Color tempColor = rend.material.color;
                        tempColor.a = 0.3F;
                
                        rend.material.color=Color.Lerp(rend.material.color, tempColor, 10f *Time.deltaTime);
                

                

                        for (int c = 0; c < transObjects.Count; c++)         
                        {
                            if (transObjects[c].Equals(hits[i]))
                            {
                                break;
                            }
                        }

                        transObjects.Add(hits[i]);

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

                rend.material.shader = Shader.Find("Standard");

                
                transObjects.Remove(transObjects[i]);
               
                
            
            }

        }
    }
}
