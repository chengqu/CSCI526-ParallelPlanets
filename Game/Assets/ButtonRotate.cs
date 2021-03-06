﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonRotate : MonoBehaviour {

	public bool isOn = false;
	public AudioClip vClickSound;
	public AudioClip unClickSound;
	private AudioSource vAudioSource;
	public Planet planet;

	public GameObject vButtonObj; 
	private Vector3 vOriginalPosition;
	private Vector3 vTargetPosition;
	private List<GameObject> vNearbyPlayer;
	// Use this for initialization
	void Start () {


		planet.vRotate = false;
		vAudioSource = GetComponent<AudioSource> ();
		vNearbyPlayer = new List<GameObject> ();
		if (vButtonObj != null) {

			//get the calculated position to move between both position for the button to make the nice move animation smoothly
			vOriginalPosition = vButtonObj.transform.localPosition;
			vTargetPosition = vButtonObj.transform.localPosition + (Vector3.down*0.1f);

			//Get the starting position ON/OFF
			if (isOn)
				vButtonObj.transform.localPosition = vOriginalPosition + (Vector3.down*0.1f);
			else
				vButtonObj.transform.localPosition = vOriginalPosition;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (isOn) {
			planet.vRotate = true;
			vButtonObj.transform.localPosition = vOriginalPosition + (Vector3.down * 0.1f);
		} else {
			planet.vRotate = false;
			vButtonObj.transform.localPosition = vOriginalPosition;
		}
	}

	void OnTriggerStay2D(Collider2D col)
	{
//		
		//CANNOT click AGAIN on the button until the player leave the button.
		if ((col.tag == "Player" || col.tag == "Pushable" ) && !vNearbyPlayer.Contains(col.gameObject)) {
			//add the player
			vNearbyPlayer.Add (col.gameObject);
			isOn = true;
			PlaySound(vClickSound);
		}
	}
	public void PlaySound (AudioClip vClip)
	{
		if (vAudioSource != null)
		{
			vAudioSource.clip = vClip;
			vAudioSource.Play ();
		}
	}
	void OnTriggerExit2D(Collider2D col)
	{
		//remove the gameobject
		if (vNearbyPlayer.Contains (col.gameObject)) {
			vNearbyPlayer.Remove (col.gameObject);
			isOn = false;
			PlaySound(unClickSound);
		}
	}

}
