using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogeImporter : MonoBehaviour {

    public GameObject textBox;

    public Text text;

    PlayerMovement player;

    public TextAsset textFile;
    public string[] textLines;

    public int currentLine;
    public int endAtLine;


    public bool isActive = false;

	// Use this for initialization
	void Start () {

        player = FindObjectOfType<PlayerMovement>();
        textBox.SetActive(false);

        if (textFile != null)
        {
            textLines = (textFile.text.Split('\n'));
        }

        //If we want to read everything
        if (endAtLine == 0)
        {
            endAtLine = textLines.Length - 1; 
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !isActive)
        {
            OpenBox();
        }
    }


    void Update()
    {
        if (isActive)
        {

            text.text = textLines[currentLine];

            if (Input.GetButtonDown("MainAction") && player.isReadingDialog)
            {
                currentLine++;
            }
            if (currentLine > endAtLine)
            {
                CloseBox();
            }
        }
    }
    void CloseBox()
    {
        textBox.SetActive(false);
        player.isReadingDialog = false;
        isActive = false;
        currentLine = 0;
    }
    void OpenBox()
    {
        textBox.SetActive(true);
        player.isReadingDialog = true;
        isActive = true;
    }
	
	
}
