﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RPB : MonoBehaviour {

	public Transform LoadingBar;
	public Transform TextIndicator;
	public float currentAmount;
	public float speed;
	public bool startTimeOut = false;
	// Update is called once per frame
	void Update () {
		if (startTimeOut) {
			if (currentAmount < 100) {
				currentAmount += speed * Time.deltaTime;
				TextIndicator.GetComponent<Text> ().text = ((int)currentAmount).ToString () + "%";
			} else {
				TextIndicator.GetComponent<Text> ().text = "DONE!";
			}
			LoadingBar.GetComponent<Image> ().fillAmount = currentAmount / 100;

		}
	}
}
