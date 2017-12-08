using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeBackToMenuScript : MonoBehaviour {

    public Animator anim;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(FadeToScene());
        }
    }

    IEnumerator FadeToScene()
    {
        anim.SetBool("FadeOut", true);
        yield return new WaitForSecondsRealtime(5);
        SceneManager.LoadScene("Menu1.1", LoadSceneMode.Single);
    }
}
