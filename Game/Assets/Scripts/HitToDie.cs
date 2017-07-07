using System.Collections;
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
		if (col.tag == "Player") {
			PlayerController bunny;
			bunny = col.GetComponent<PlayerController> ();
			bunny.Damage (20);
		}
		if (col.tag == "Cloud") {
			Cloud cloud;
			cloud = col.GetComponent<Cloud> ();
			cloud.Damage (2);
		}
		if (col.tag == "Spikeman") {
			CharacterController Spikeman;
			Spikeman = col.GetComponent<CharacterController> ();
			Spikeman.Damage (5);
		}
	}

//	void OnCollisionEnter2D (Collider2D col) {
//		if (col.transform.tag == "Player") {
//			bunny.Damage (damage);
//		}
//	}
}
