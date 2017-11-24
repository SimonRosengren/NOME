using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomAnalyticsToolPresenter : MonoBehaviour {

    public class PlayerPath
    {
        public readonly int ID;
        public readonly Color color;
        public List<Vector3> path;

        public PlayerPath(int id)
        {
            this.ID = id;
            this.color = UnityEngine.Random.ColorHSV();
            this.path = new List<Vector3>();
        }
    }

    string[] positions;
    string[] separators = { " ", "(", ")", "," };

    List<Vector3> parsedPositions;
    List<PlayerPath> playerPaths;

    //Path to text file
    public string fileName = "filename";
    string posPath;

    void Start () {
        posPath = @"CustomAnalyticsTool\" + fileName + ".txt";
        parsedPositions = new List<Vector3>();
        playerPaths = new List<PlayerPath>();
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
        int currentSession = 0;

        // i+=2 because it save a empty line between every line, so we want to skip those
        for (int i = 0; i < positions.Length; i+=2)
        {
            if (positions[i].Contains("SESSION"))
            {
                string[] sess = positions[i].Split(separators, StringSplitOptions.RemoveEmptyEntries);
                int id;
                int.TryParse(sess[1], out id);
                playerPaths.Add(new PlayerPath(id));
                currentSession++;
            }
            else if (!positions[i].Contains("SESSION"))
            {
                string[] xyz = positions[i].Split(separators, StringSplitOptions.RemoveEmptyEntries);
                Vector3 p = new Vector3();
                float.TryParse(xyz[0], out p.x);
                float.TryParse(xyz[1], out p.y);
                float.TryParse(xyz[2], out p.z);
                //parsedPositions.Add(p);
                playerPaths[currentSession - 1].path.Add(p);
                
            }
        }
    }

    void DrawLinesBetweenPoints()
    {
        //for (int i = 0; i < parsedPositions.Count - 1; i++)
        //{
        //    Debug.DrawLine(parsedPositions[i], parsedPositions[i + 1], Color.red);
        //}
        for (int i = 0; i < playerPaths.Count; i++)
        {
            Color pColor = UnityEngine.Random.ColorHSV();
            for (int j = 0; j < playerPaths[i].path.Count - 1; j++)
            {
                Debug.DrawLine(playerPaths[i].path[j], playerPaths[i].path[j + 1], playerPaths[i].color);
            }
        }
    }
}
