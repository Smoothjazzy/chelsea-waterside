using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swipeFinger : MonoBehaviour {

	private Vector3 startPosition = Vector3.zero;
	private Vector3 endPosition = Vector3.zero;
	private float floatdeltaX;
	private float floatdeltaY;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown(0))    // swipe begins
		{
			startPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Debug.Log (startPosition);
		}
		if (Input.GetMouseButtonUp(0))    // swipe ends
		{
			endPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Debug.Log ("Mouse button up");
		}

		if (startPosition != endPosition)
		{
			Debug.Log ("Swipe detected");
			floatdeltaX = endPosition.x - startPosition.x;
			floatdeltaY = endPosition.y - startPosition.y;
			Debug.Log ("floatdeltaY == " + floatdeltaY);
			if ((floatdeltaX > 5.0f || floatdeltaX < -5.0f) && (floatdeltaY >= -1.0f || floatdeltaY <= 1.0f))
			{
				if (startPosition.x < endPosition.x) // swipe LTR
				{
					Debug.Log("LTR");
				} else // swipe RTL
				{
					Debug.Log("RTL");
				}
			}
			startPosition = endPosition = Vector3.zero;
		}		
	}
}
