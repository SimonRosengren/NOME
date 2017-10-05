using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour {
    /*Set to start form beginning*/
    [SerializeField] Transform lastCheckpointPosition;

	void Start ()
    {

	}
	

	void Update ()
    {
		
	}

    public void setNewCheckpoint(Transform pos)
    {
        lastCheckpointPosition = pos;
    }
    public Transform GetLastCheckPoint()
    {
        return lastCheckpointPosition;
    }
}
