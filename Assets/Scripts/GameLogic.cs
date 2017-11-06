using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour {
    /*Set to start form beginning*/
    [SerializeField] Transform lastCheckpointPosition;
    [SerializeField] BookHandler bookHandler;

    PlayerMovementForce player;

	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementForce>();

    }
	

	void Update ()
    {
        if (Input.GetButtonDown("Grab") && player.inReachOfBook != 0)
        {
            Debug.Log("TRYING TO READ");
            if (!bookHandler.isActive)
            {
                bookHandler.ShowBook(player.inReachOfBook);
            }
            else
                bookHandler.CloseBook();

        }
        

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
