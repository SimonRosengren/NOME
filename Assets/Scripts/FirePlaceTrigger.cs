using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FirePlaceTrigger : MonoBehaviour {

    bool brickRemoved = false;
    bool sprinklerOn = false;

    [SerializeField]GameObject sprinkler;

    [SerializeField]Transform sprinklerTransform;

    [SerializeField]Transform smokeTransform;

    [SerializeField]RobotBehaviour robot;
    [SerializeField]FlammableObject fire;

    [SerializeField]ParticleSystem smoke;


	void Start () {

	}

	void Update () {
        ActivateSprinkler();
	}

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Grabable")
        {
            WriteToLog("Brick left trigger exit");
            brickRemoved = true;
            WriteToLog("brickRemoved is now: " + brickRemoved);
            WriteToLog("fire.onFire is now: " + fire.onFire);
            WriteToLog("sprinklerOn is now: " + sprinklerOn);
        }
    }

    public void ActivateSprinkler()
    {
        if (fire.onFire && brickRemoved && !sprinklerOn)
        {
            WriteToLog("Entered if-statement inside ActiateSprinkler");
            Invoke("DeactivateRobot", 2);
            Instantiate(sprinkler, sprinklerTransform);
            Instantiate(smoke, smokeTransform);
            sprinklerOn = true;
            WriteToLog("sprinklerOn = " + sprinklerOn);
        }
    }

    public void DeactivateRobot()
    {
        WriteToLog("DeactivateRobote has now been invoked");
        robot.wet = true;
    }
    public void WriteToLog(string msg)
    {
        string path = @"" + "mOutPut" + ".txt";
        using (StreamWriter writer = File.AppendText(path))
        {
            writer.WriteLine(msg + "\n");
            writer.Close();
        }
    }
}
