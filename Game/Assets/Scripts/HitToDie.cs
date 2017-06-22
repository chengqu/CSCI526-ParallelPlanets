﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitToDie : MonoBehaviour {
	public PlayerController bunny;
	public float damage = 20;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.transform.tag == "Player") {
			bunny.Damage (damage);
		}
	}

	void OnCollisionEnter2D (Collider2D col) {
		if (col.transform.tag == "Player") {
			bunny.Damage (damage);
		}
	}
}
