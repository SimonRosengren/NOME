using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FadeObjectsBetween : MonoBehaviour
{

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
    void Start()
    {
        transObjects = new List<GameObject>();
        hitsList = new List<GameObject>();
        changedObj = new List<ChangedObject>();

    }

    // Update is called once per frame
    void Update()
    {
        playerV = Player.transform.position;
        cameraV = Camera.transform.position;
        direction = playerV - cameraV;

        Debug.DrawRay(transform.position, direction);
        if (Physics.Linecast(cameraV, playerV))
        {
            DecreaseAlpha();
        }

        //compare old rays against new and see which objects are missing

        AddNewShader(FindDifferences(transObjects, hitsList));
    }

    void DecreaseAlpha()
    {
        hits = Physics.RaycastAll(transform.position, direction, Vector3.Distance(cameraV, playerV));

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.GetComponent<Renderer>() != null && hits[i].transform.tag != "wall")
            {
                Renderer rend = hits[i].transform.GetComponent<Renderer>();


                if (rend.material.shader != Shader.Find("Transparent/Diffuse"))
                {
                    transObjects.Add(hits[i].transform.gameObject);
                    Shader[] shaders = new Shader[rend.materials.Length];
                    Color[] color = new Color[rend.materials.Length];
                    for (int j = 0; j < rend.materials.Length; j++)
                    {
                        shaders[j] = rend.materials[j].shader;
                        color[j] = rend.materials[j].color;
                    }

                    ChangedObject cO = new ChangedObject(shaders, hits[i].transform.gameObject, color);
                    changedObj.Add(cO);
                }

                hitsList.Add(hits[i].transform.gameObject);
                for (int j = 0; j < rend.materials.Length; j++)
                {
                    Color tempColor = rend.materials[j].color;
                    tempColor.a = 0.2F;
                    rend.materials[j].color = Color.Lerp(rend.material.color, tempColor, 3f * Time.deltaTime);
                    rend.materials[j].shader = Shader.Find("Transparent/Diffuse");


                }
            }
        }
    }

    List<GameObject> FindDifferences(List<GameObject> trans, List<GameObject> hits)
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
                Color tempColor;


                for (int c = 0; c < changedObj.Count; c++)
                {
                    if (results[i] == changedObj[c].GO)
                    {

                        for (int j = 0; j < changedObj[c].shader.Length; j++)
                        {
                            Debug.Log(changedObj[c].shader);
                            rend.materials[j].shader = changedObj[c].shader[j];


                            tempColor = changedObj[c].color[j];

                            rend.materials[j].color = tempColor;


                        }
                        changedObj.Remove(changedObj[c]);
                        break;
                    }
                }



                transObjects.Remove(results[i]);
            }
        }
        // empty the list for next frames arrays
        hitsList.Clear();
    }
}

class ChangedObject
{
    public Shader[] shader;
    public GameObject GO;

    public Color[] color;
    public ChangedObject(Shader[] shader, GameObject GO, Color[] color)
    {
        this.shader = shader;
        this.GO = GO;
        this.color = color;
        //this.materials = materials;
    }
}
