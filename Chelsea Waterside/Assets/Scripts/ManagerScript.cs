using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerScript : MonoBehaviour {


    public static bool waterOn;
    public static bool popcornOn;
    public static bool bubblesOn;
    public GameObject popcornButton;
    public GameObject bubbleButton;
    private int popcornTimer;
    private int buttonTimer;

	// Use this for initialization
	void Start () {
        waterOn = true;
        popcornOn = false;
        bubblesOn = false;
        popcornButton.SetActive(false);
        bubbleButton.SetActive(false);
        StartCoroutine(popcornMaker());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void activatePopcornFountain() {
        StartCoroutine(popcornFountain());
    }

    public void activateBubblesFountain()
    {
        StartCoroutine(bubblesFountain());
    }

    IEnumerator popcornMaker() {
        popcornTimer = Random.Range(10, 40);
        yield return new WaitForSeconds(popcornTimer);
        popcornButton.SetActive(true);
        Debug.Log("You should see the popcorn now at " + popcornTimer + "seconds");
    }

    IEnumerator buttonInitiator()
    {
        buttonTimer = Random.Range(10, 40);
        yield return new WaitForSeconds(buttonTimer);
        if (Random.value >= 0.5)
        {
            popcornButton.SetActive(true);
        }
        else {
            bubbleButton.SetActive(true);
        }
        Debug.Log("You should see the button now at " + buttonTimer + "seconds");
    }


    IEnumerator popcornFountain() {
        popcornButton.SetActive(false);
        popcornOn = true;
        waterOn = false;
        yield return new WaitForSeconds(30);
        popcornOn = false;
        waterOn = true;
        StartCoroutine(buttonInitiator());
    }

    IEnumerator bubblesFountain()
    {
        bubbleButton.SetActive(false);
        bubblesOn = true;
        waterOn = false;
        yield return new WaitForSeconds(30);
        bubblesOn = false;
        waterOn = true;
        StartCoroutine(buttonInitiator());
    }
}
