using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CustomAnalyticToolRecord : MonoBehaviour
{

    public bool recordMovement;
    public float saveRate = 0.1f;

    //Player goes here if you want to track player movement
    [SerializeField] Transform objectToTrack;

    int sessionID;

    //Path to text file
    public string movementPathName = "path";
    public string checkpointPathName = "path";
    string path;
    string checkPointPath;

    float timer = 0;

    void Start()
    {
        path = @"" + movementPathName + ".txt";
        checkPointPath = @"" + checkpointPathName + ".txt";
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
            using (StreamWriter writer = File.AppendText(path))
            {
                writer.WriteLine(objectToTrack.position + Vector3.up + "\n");
                writer.Close();
            }
            timer = 0;
        }
    }

    public void SaveCheckpointTime()
    {
        using (StreamWriter writer = File.AppendText(checkPointPath))
        {
            writer.WriteLine(Time.timeSinceLevelLoad + "\n");
            writer.Close();
        }
    }
    //This method creates the ID. For increased safety this should be done with a GUID Instead, 
    //however I find this exessive as of now
    void CreateSessionID()
    {
        sessionID = (int)Random.Range(0, 9999999);
        using (StreamWriter writer = File.AppendText(path))
        {
            writer.WriteLine("SESSION " + sessionID + "\n");
            writer.Close();
        }
        using (StreamWriter writer = File.AppendText(checkPointPath))
        {
            writer.WriteLine("SESSION " + sessionID + "\n");
            writer.Close();
        }
    }
}

