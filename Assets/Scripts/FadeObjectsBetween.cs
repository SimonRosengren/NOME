using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeObjectsBetween : MonoBehaviour {

    public GameObject Camera;

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


        AddNewShader();

        Debug.DrawRay(transform.position, direction);
        if (Physics.Linecast(cameraV, playerV))
        {
            DecreaseAlpha();
        }
        
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

            tempColor.a = 0.2F;
            rend.material.color = Color.Lerp(rend.material.color, tempColor, 3f * Time.deltaTime);

            Debug.Log(hits.Length);
        }



    }
    
    void AddNewShader()
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
