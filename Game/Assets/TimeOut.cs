using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeOut : MonoBehaviour {

	public RPB timeBar;
	public GameObject vCurPlanet;
	public GameObject cloud;
	// Use this for initialization
	void Start () {
		foreach (Transform child in transform) {
			if (child.gameObject.tag == "Planet")
				vCurPlanet = child.gameObject;
			if (child.gameObject.tag == "Cloud") {
				cloud = child.gameObject;
				cloud.SetActive (false);
			}
		}
	}

	// Update is called once per frame
	void Update () {
		if (vCurPlanet != null) {
			foreach (Transform child in vCurPlanet.transform) {
				if (child.gameObject.tag == "Player")
					timeBar.startTimeOut = true;
			}
		}
		if (timeBar != null && timeBar.currentAmount >= 100) {
			cloud.SetActive (true);
		}
	}

}
