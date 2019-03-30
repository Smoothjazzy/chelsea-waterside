using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {


//    private AudioSource menuHonk;


    // Use this for initialization
    public void startPlaying()
    {
        SceneManager.LoadScene("Main scene");
    }

//    public void honk() {
  //      menuHonk = GetComponent<AudioSource>();
    //    menuHonk.Play();
  //  }
}
