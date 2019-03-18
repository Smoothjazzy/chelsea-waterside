using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class honker : MonoBehaviour {

    private AudioSource honk;


    void OnMouseDown()
    {
        honk.volume = 1;
        honk.Play();
    }

    // Use this for initialization
    void Start () {
	    honk = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
