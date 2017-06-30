using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonRotate : MonoBehaviour {

	public bool isOn = false;
	public Planet planet;
	// Use this for initialization
	void Start () {
		planet.vRotate = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isOn) {
			planet.vRotate = true;
		} else {
			planet.vRotate = false;
		}
	}

	void OnTriggerStay2D(Collider2D col) {
		if (col.transform.tag == "Player") {
			isOn = !isOn;
		}
	}
}
