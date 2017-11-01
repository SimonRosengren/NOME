using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

    public readonly int ID;

    [SerializeField] Transform startPoint;

    [SerializeField] GameObject gameHandler;

    [SerializeField] CustomAnalyticToolRecord analyticTool;

    void Start () {
		
	}
	
	void Update ()
    {
		
	}

    public void SetAsLastCheckpoint()
    {
        GameLogic gameLogic = gameHandler.transform.GetComponent<GameLogic>();
        gameLogic.setNewCheckpoint(startPoint);
        analyticTool.SaveCheckpointTime();
    }

    public Transform GetPos()
    {
        return startPoint;
    }
}
