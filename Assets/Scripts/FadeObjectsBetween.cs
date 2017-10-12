using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeObjectsBetween : MonoBehaviour {

    public Camera Camera;

    public GameObject Player;

    Vector3 playerV;
    Vector3 cameraV;
    RaycastHit[] hits;
    Vector3 direction;

    List<RaycastHit> transObjects;
    // Use this for initialization
    void Start () {
        transObjects = new List<RaycastHit>();
        
	}
	
	// Update is called once per frame
	void Update () {
        playerV = Player.transform.position;
        cameraV = Camera.transform.position;
        direction = playerV - cameraV;



        Debug.DrawRay(transform.position, direction);
        if (Physics.Linecast(cameraV, playerV))
        {
            DecreaseAlpha();
        }
        AddAlpha();
        
	}

    void DecreaseAlpha()
    {

        hits = Physics.RaycastAll(transform.position, direction, Vector3.Distance(cameraV, playerV));
        for (int i = 0; i < hits.Length; i++)
        {
            Renderer rend = hits[i].transform.GetComponent<Renderer>();

            if (rend.material.shader!=Shader.Find("Transparent/Diffuse"))
            {
                
                Debug.Log("add");
                transObjects.Add(hits[i]);

            }
            rend.material.shader = Shader.Find("Transparent/Diffuse");
            Color tempColor = rend.material.color;

            tempColor.a = 0.3F;
            rend.material.color = Color.Lerp(rend.material.color, tempColor, 10f * Time.deltaTime);


        }



    }

    void AddAlpha()
    {
        if(transObjects.Count != 0)
        {
            for (int i = 0; i < transObjects.Count; i++)
            {

                for (int c = 0; c < hits.Length; c++)
                {
                    if (hits[c].transform.gameObject == transObjects[i].transform.gameObject)
                    {
                        i++;
                        c = 0;
                    }
                }
                
                    Renderer rend = transObjects[i].transform.GetComponent<Renderer>();

                    Color tempColor = rend.material.color;

                    tempColor.a = 1F;


               


                    rend.material.color = Color.Lerp(rend.material.color, tempColor, 20f * Time.deltaTime);


                    if (rend.material.color.a>=1)
                    {
                        rend.material.shader = Shader.Find("Standard");
                        transObjects.Remove(transObjects[i]);
                    }

                
                
               
                
            
            }

        }
    }
}
