using UnityEngine;
using System;


public class ClickToActivatePopcorn:MonoBehaviour
{
    public PopcornPopper popcornSpawner;
    
    public void OnMouseDown() {
    	popcornSpawner.popEnabled = true;
    }
}
