using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndoorSceneScript : MonoBehaviour {

    [SerializeField] LoadIndoorScene radioSceneTrigger;
    [SerializeField] PickUpHandler puHandler;
    [SerializeField] GameObject dialoge;

    void Start()
    {

    }

    void Update()
    {
        if (puHandler.kitchenKey && puHandler.tvKey && puHandler.vacuumKey && puHandler.tableKey)
        {
            radioSceneTrigger.isActive = true;
            Destroy(dialoge);
        }
    }
}
