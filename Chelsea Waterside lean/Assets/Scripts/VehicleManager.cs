using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleManager : MonoBehaviour {

    public GameObject[] vehicles;
    public bool repeat = true;

	// Use this for initialization
	void Start () {
        if (vehicles != null) {
        StartCoroutine(vehicleQueue());
        }
    }
	
	// Update is called once per frame
	void Update () {
	

	}

    IEnumerator vehicleQueue() {
        while(repeat) {
            yield return new WaitForSeconds(10);
            //Debug.Log("OnCoroutine: " + Time.time);
            int vehicleChoice = Random.Range(0, vehicles.Length);
            vehicles[vehicleChoice].GetComponent<Animator>().SetTrigger("Go");
        }
    }
}
