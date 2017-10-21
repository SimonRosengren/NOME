using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CustomAnalyticToolRecord : MonoBehaviour {

    //Player goes here if you want to track player movement
    [SerializeField]
    Transform objectToTrack;

    //Path to text file
    string path = @"CustomAnalyticsTool\Movement.txt";

    float timer = 0;

    public float saveRate = 0.1f;


    void Start()
    {

    }

    void Update()
    {
        SaveToFile();
    }

    void SaveToFile()
    {
        timer += Time.deltaTime;

        if (timer >= saveRate)
        {
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(path, true))
            {
                file.WriteLine(objectToTrack.position + "\n");
            }
            timer = 0;
        }
    }
}

