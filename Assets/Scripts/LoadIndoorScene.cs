using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadIndoorScene : MonoBehaviour {

    public Animator anim;
    public float fadeTimer = 1;
    public string scene = "Emils scene";
    public bool isActive = true;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && isActive)
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
