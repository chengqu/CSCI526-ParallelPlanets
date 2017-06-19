﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogueHolder : MonoBehaviour {

	public string dialogue = "Hi,Bunny";
	private DialogueManager dMAn;
	public PlayerController bunny;
	// Use this for initialization
	void Start () {
		dMAn = (DialogueManager)FindObjectOfType(typeof(DialogueManager));
		Debug.Log (dMAn);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay2D(Collider2D col) {
		if (col.transform.tag == "Player") {

			if (bunny.findTarget) {
				dMAn.ShowBox ("YOU WIN");
			}
			else if (Input.GetKeyUp (KeyCode.Space)) {
				dMAn.ShowBox (dialogue);
			}
		}
	}
}
