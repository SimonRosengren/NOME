using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarnSceneScript : MonoBehaviour {

    [SerializeField] LoadIndoorScene sceneLoader;
    [SerializeField] Animator cowAnimator;

    [SerializeField] GameObject leaveBarnTooEarlyTrigger;
    [SerializeField] GameObject cowThirstyTrigger;

    [SerializeField] AudioSource cowBreaths;
    public AudioSource aS;


    // Use this for initialization
    void Start () {
        sceneLoader.isActive = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        /*The bucket is grabbable*/
        if (other.tag == "Grabable")
        {
            sceneLoader.isActive = true;
            cowAnimator.SetBool("isDead", true);
            leaveBarnTooEarlyTrigger.GetComponent<DialogeImporter>().isActive = false;
            cowThirstyTrigger.GetComponent<DialogeImporter>().isActive = false;
            Destroy(leaveBarnTooEarlyTrigger);
            Destroy(cowThirstyTrigger);
            cowBreaths.Stop();
            StartCoroutine(AudioFadeOut.FadeOut(aS, 7));
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
