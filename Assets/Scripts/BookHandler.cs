using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookHandler : MonoBehaviour {

    string a = "Midvinternattens köld är hård\n stjärnorna gnistra och glimma.\nAlla sova i enslig gård\ndjupt under midnattstimma.\nMånen vandrar sin tysta ban\nsnön lyser vit på fur och gran,\nsnön lyser vit på taken.\nEndast tomten är vaken.\n";
    string b = "This text is just for testing. First of all, it's faaar too long. Second of all, it is stored in a f*ck#ng string...\n Anyhow, it's super important test number is 2";

    [SerializeField] Image bookImage;
    [SerializeField] Text bookText;

    public bool isActive;
     

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /*This really should be using a database*/
    /*This is called from GameLogic*/
    public void ShowBook(int bookId)
    {
        isActive = true;
        Color tempColor = bookImage.color;
        tempColor.a = 1;
        bookImage.color = tempColor;
        tempColor = bookText.color;
        tempColor.a = 1;
        bookText.color = tempColor;

        switch (bookId)
        {
            case 1:
                bookText.text = a;
                break;
            case 2:
                bookText.text = b;
                break;
            default:
                break;
        }
    }

    public void CloseBook()
    {
        this.isActive = false;
        Color tempColor = bookImage.color;
        tempColor.a = 0;
        bookImage.color = tempColor;
        tempColor = bookText.color;
        tempColor.a = 0;
        bookText.color = tempColor;
    }
}
