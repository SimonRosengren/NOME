using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FadeObjectsBetween : MonoBehaviour {

    public GameObject Camera;

    public GameObject Player;

    Vector3 playerV;
    Vector3 cameraV;
    RaycastHit[] hits;
    Vector3 direction;
    
    List<GameObject> hitslist;
    List<GameObject> transObjects;
    // Use this for initialization
    void Start () {
        transObjects = new List<GameObject>();
        hitslist = new List<GameObject>();
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
        
        //compare old rays against new and see which objects are missing
        List<GameObject> results = transObjects.Except(hitslist).ToList();

        Debug.Log(results.Count);
        
        AddNewShader(results);

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
                transObjects.Add(hits[i].transform.gameObject);

            }
            hitslist.Add(hits[i].transform.gameObject);

            rend.material.shader = Shader.Find("Transparent/Diffuse");

            Color tempColor = rend.material.color;
            tempColor.a = 0.2F;
            rend.material.color = Color.Lerp(rend.material.color, tempColor, 3f * Time.deltaTime);
           
            
        }
        


    }




    void AddNewShader(List<GameObject> results)
    {


        if (results.Count > 0)
        {

            for (int i = 0; i < results.Count; i++)
            {
                
                   
                
                Renderer rend = results[i].transform.GetComponent<Renderer>();
                Color tempColor = rend.material.color;
                tempColor.a = 1f;

                rend.material.color = tempColor;
               
                rend.material.shader = Shader.Find("Standard");
     
                
                
                transObjects.Remove(results[i]);

                
    
            }
            
            
        }
        // empty the list for next frames arrays
        hitslist.Clear();
        
    }


   
}
