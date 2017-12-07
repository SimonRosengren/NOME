using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndoorSceneScript : MonoBehaviour {

    [SerializeField] LoadIndoorScene radioSceneTrigger;
    [SerializeField] PickUpHandler puHandler;
    [SerializeField] GameObject dialoge;

    public Animator doorAnim;
    bool doorOpen = false; 
    void Start()
    {

    }

    void Update()
    {
        if (puHandler.kitchenKey && puHandler.tvKey && puHandler.vacuumKey && puHandler.tableKey && !doorOpen)
        {
            doorAnim.SetTrigger("allKeysTrig");
            doorOpen = true;
            Destroy(dialoge);
        }
    }
}
