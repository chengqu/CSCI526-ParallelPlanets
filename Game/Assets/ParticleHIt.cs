using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHIt : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnParticleCollision (GameObject other) {
		if (other.tag == "Player") {
			PlayerController bunny;
			bunny = other.GetComponent<PlayerController> ();
			bunny.Damage (20);
		}
		if (other.tag == "Cloud") {
			Cloud cloud;
			cloud = other.GetComponent<Cloud> ();
			cloud.Damage (2);
		}
		if (other.tag == "Spikeman") {
			CharacterController Spikeman;
			Spikeman = other.GetComponent<CharacterController> ();
			Spikeman.Damage (5);
		}
	}

}
