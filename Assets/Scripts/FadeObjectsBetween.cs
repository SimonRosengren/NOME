﻿using System.Collections;
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
    
    List<GameObject> hitsList;
    List<GameObject> transObjects;
    
    List<GameObject> Difference;
    List<ChangedObject> changedObj;

    // Use this for initialization
    void Start () {
        transObjects = new List<GameObject>();
        hitsList = new List<GameObject>();
        changedObj = new List<ChangedObject>();
        
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
  
        AddNewShader(FindDifferences(transObjects,hitsList));
	}

    void DecreaseAlpha()
    {
        hits = Physics.RaycastAll(transform.position, direction, Vector3.Distance(cameraV, playerV));

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.GetComponent<Renderer>()!=null)
            {
                Renderer rend = hits[i].transform.GetComponent<Renderer>();

                if (rend.material.shader != Shader.Find("Transparent/Diffuse"))
                {
                    transObjects.Add(hits[i].transform.gameObject);
                    ChangedObject cO = new ChangedObject(rend.material.shader, hits[i].transform.gameObject, rend.material.color.a);
                    changedObj.Add(cO);
                }

                hitsList.Add(hits[i].transform.gameObject);

                Color tempColor = rend.material.color;
                tempColor.a = 0.2F;
                rend.material.color = Color.Lerp(rend.material.color, tempColor, 3f * Time.deltaTime);
                rend.material.shader = Shader.Find("Transparent/Diffuse");

            }           
        }
    }

    List<GameObject> FindDifferences(List<GameObject>trans, List<GameObject> hits)
    {
        return trans.Except(hits).ToList();
    }


    void AddNewShader(List<GameObject> results)
    {
        if (results.Count > 0)
        {
            for (int i = 0; i < results.Count; i++)
            {                                                
                Renderer rend = results[i].transform.GetComponent<Renderer>();
                Color tempColor = rend.material.color;
                
                for (int c = 0; c < changedObj.Count; c++)
                {
                    if (results[i] == changedObj[c].GO)
                    {
                        rend.material.shader = changedObj[c].shader;
                        tempColor.a = changedObj[c].a;
                        changedObj.Remove(changedObj[c]);
                        break;
                    }
                }
                rend.material.color = tempColor;

                transObjects.Remove(results[i]);
            }     
        }
        // empty the list for next frames arrays
        hitsList.Clear();       
    }
}

class ChangedObject
{
    public Shader shader;
    public GameObject GO;
    public float a;
    public ChangedObject(Shader shader,GameObject GO,float alpha)
    {
        this.shader = shader;
        this.GO = GO;
        this.a = alpha;
    }
}
