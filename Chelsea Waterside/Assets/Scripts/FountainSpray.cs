using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FountainSpray : MonoBehaviour {

	public GameObject waterSpray;
    public GameObject popcornPopper;
    public GameObject bubbleSpray;
	private AudioSource fountainSound;
    public AudioClip waterSound;
    public AudioClip bubbleSound;


	void OnMouseDown () {
        //	Debug.Log ("clicked");
        if (ManagerScript.waterOn == true) {
            waterSpray.GetComponent<ParticleSystem>().Play();
            fountainSound.clip = waterSound;
            fountainSound.volume = 1;
            fountainSound.Play();
        } else if (ManagerScript.popcornOn == true) {
            popcornPopper.GetComponent<PopcornPopper>().popEnabled = true;
       //     Debug.Log("Pop Enabled");
        } else if (ManagerScript.bubblesOn == true) {
            bubbleSpray.GetComponent<ParticleSystem>().Play();
            fountainSound.clip = bubbleSound;
            fountainSound.volume = 1;
            fountainSound.Play();

        }


    }

	void OnMouseUp () {
        if (ManagerScript.waterOn == true)
        {
            StartCoroutine(waterOff());
        } else if (ManagerScript.popcornOn == true) {
            popcornPopper.GetComponent<PopcornPopper>().resetPopcorn();
            //            StartCoroutine(popcornOff());
        } else if (ManagerScript.bubblesOn == true) {
            StartCoroutine(bubblesOff());
        }

    } 

	// Use this for initialization
	void Start () {
		//waterSpray.SetActive (false);
		waterSpray.GetComponent<ParticleSystem>().Pause();
        fountainSound = GetComponent<AudioSource> ();
        popcornPopper.GetComponent<PopcornPopper>().popEnabled = false;
        bubbleSpray.GetComponent<ParticleSystem>().Pause();
	} 
	
	// Update is called once per frame
	void Update () {

	}

	IEnumerator waterOff() {
		yield return new WaitForSeconds (6);
		waterSpray.GetComponent<ParticleSystem>().Stop();
		float volumeKnob = 0.1f;
		for (var i = 0; i < 10; i ++) {
			volumeKnob += (i * 0.1f);
            fountainSound.volume -= volumeKnob;
			yield return new WaitForSeconds (0.1f);
		}
        fountainSound.Stop ();
        fountainSound.volume = 1;

	}
	public void waterButton() {
		waterSpray.GetComponent<ParticleSystem>().Play ();
		StartCoroutine (waterOff());
	}

    IEnumerator popcornOff() {
        yield return new WaitForSeconds(4);
 //       popcornPopper.GetComponent<PopcornPopper>().popEnabled = false;
        popcornPopper.GetComponent<PopcornPopper>().resetPopcorn();
    }

    IEnumerator bubblesOff() {
        yield return new WaitForSeconds(6);
        bubbleSpray.GetComponent<ParticleSystem>().Stop();

    }

}
