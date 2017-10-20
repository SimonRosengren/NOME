using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CustomAnalyticTool : MonoBehaviour {

    //Player goes here if you want to track player movement
    [SerializeField] Transform objectToTrack;

    //Path to text file
    string path = @"CustomAnalyticsTool\Movement.txt";

    float timer = 0;

    public float saveRate = 3f;


    void Start ()
    {
		
	}

	void Update ()
    {
        SaveToFile();
    }

    void SaveToFile()
    {
        timer += Time.deltaTime;

        if (timer >= saveRate)
        {
            File.WriteAllText(path, "Test");
            timer = 0;
        }
    }
}
