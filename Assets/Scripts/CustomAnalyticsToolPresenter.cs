using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomAnalyticsToolPresenter : MonoBehaviour {

    string[] positions;
    string[] separators = { " ", "(", ")", "," };

    List<Vector3> parsedPositions;

    //Path to text file
    string posPath = @"CustomAnalyticsTool\Movement.txt";

    void Start () {
        parsedPositions = new List<Vector3>();
        ReadInPositions();
        ParseToPositions();

    }
	

	void Update () {
        DrawLinesBetweenPoints();
    }

    void ReadInPositions()
    {
        positions = System.IO.File.ReadAllLines(posPath);
    }

    //Splits the string in positions according to separatin rules.
    //Tries to parse them into vector3 and isnerts them into list
    void ParseToPositions()
    {
        // i+=2 because it save a empty line between every line, so we want to skip those
        for (int i = 0; i < positions.Length; i+=2)
        {
            if(positions[i] != "newplayer")
            {
                string[] xyz = positions[i].Split(separators, StringSplitOptions.RemoveEmptyEntries);
                Vector3 p = new Vector3();
                float.TryParse(xyz[0], out p.x);
                float.TryParse(xyz[1], out p.y);
                float.TryParse(xyz[2], out p.z);
                parsedPositions.Add(p);
            }
            else if(positions[i] == "newplayer")
            {
                
            }

        }

    }

    void DrawLinesBetweenPoints()
    {
        for (int i = 0; i < parsedPositions.Count - 1; i++)
        {
            Debug.DrawLine(parsedPositions[i], parsedPositions[i + 1], Color.red);
        }
    }
}
