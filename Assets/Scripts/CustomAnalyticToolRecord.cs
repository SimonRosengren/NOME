using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CustomAnalyticToolRecord : MonoBehaviour {

    public bool recordMovement;
    public float saveRate = 0.1f;

    //Player goes here if you want to track player movement
    [SerializeField]
    Transform objectToTrack;

    int sessionID;

    //Path to text file
    string path = @"CustomAnalyticsTool\Movement.txt";

    float timer = 0;


    void Start()
    {
        CreateSessionID();
    }

    void Update()
    {
        SaveToFile();
    }

    void SaveToFile()
    {
        if (recordMovement)
        {
            savePositions();
        }

    }
    public void savePositions()
    {
        timer += Time.deltaTime;

        if (timer >= saveRate)
        {
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(path, true))
            {
                file.WriteLine(objectToTrack.position + Vector3.up + "\n");
            }
            timer = 0;
        }
    }
    //This method creates the ID. For increased safety this should be done with a GUID Instead, 
    //however I find this exessive as of now
    void CreateSessionID()
    {
        sessionID = (int)Random.Range(0, 9999999);
        using (System.IO.StreamWriter file =
    new System.IO.StreamWriter(path, true))
        {
            file.WriteLine("SESSION " + sessionID + "\n");
        }
    }
}

