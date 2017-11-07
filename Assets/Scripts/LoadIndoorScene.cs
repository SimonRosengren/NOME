using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadIndoorScene : MonoBehaviour {

    public Animator anim;
    public float fadeTimer = 1;
    public string scene = "Emils scene";

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
        yield return new WaitForSecondsRealtime(fadeTimer);
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}
