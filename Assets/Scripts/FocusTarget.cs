using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusTarget : MonoBehaviour
{

    public GameObject target;
    public float panSpeed;
    public float panTime;
    public AudioSource aS;
    public float fadeoutTimer;

    CameraScriptFree camScript;
    bool cutsceneDone = false;
    bool inCutscene = false;
    float timer;

    void Start()
    {
        GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
        camScript = cam.GetComponent<CameraScriptFree>();
    }
    void Update()
    {
        if (inCutscene)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                camScript.Target = player.transform.Find("CameraTarget").gameObject;
                camScript.cameraSpeed = 10f;
                inCutscene = false;
                cutsceneDone = true;
                
            }
        }
    }


   
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !cutsceneDone)
        {
            
            camScript.cameraSpeed = panSpeed;
            camScript.Target = target;
            timer = panTime;
            StartCoroutine(AudioFadeOut.FadeOut(aS, fadeoutTimer));
            inCutscene = true;

        }
    }

    public static class AudioFadeOut
    {

        public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
        {
            float startVolume = audioSource.volume;

            while (audioSource.volume > 0)
            {
                audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

                yield return null;
            }

            audioSource.Stop();
            audioSource.volume = startVolume;
        }

    }

}
