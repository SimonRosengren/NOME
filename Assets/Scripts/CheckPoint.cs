using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

    [SerializeField] Transform startPoint;

    [SerializeField] GameObject gameHandler;

	void Start () {
		
	}
	
	void Update ()
    {
		
	}

    public void SetAsLastCheckpoint()
    {
        GameLogic gameLogic = gameHandler.transform.GetComponent<GameLogic>();
        gameLogic.setNewCheckpoint(startPoint);
    }

    public Transform GetPos()
    {
        return startPoint;
    }
}
