using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeBackToMenuScript : MonoBehaviour {

    public Animator anim;
    public int startFadeAfterSeconds = 0;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Invoke("StartFade", startFadeAfterSeconds);
        }
    }
    public void StartFade()
    {
        StartCoroutine(FadeToScene());
    }

    IEnumerator FadeToScene()
    {
        anim.SetBool("FadeOut", true);
        yield return new WaitForSecondsRealtime(20);
        SceneManager.LoadScene("MainMenu1.1", LoadSceneMode.Single);
    }
}
