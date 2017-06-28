using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudDamage : MonoBehaviour {

	// Use this for initialization
	public PlayerController bunny;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnParticleCollision (GameObject other) {
		if (other.tag == "Player") {
			bunny = other.GetComponent<PlayerController> ();
			bunny.Damage (8);
		}
	}
}
