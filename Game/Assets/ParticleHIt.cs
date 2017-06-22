using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHIt : MonoBehaviour {

	public PlayerController bunny;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnParticleCollision (GameObject other) {
		if (other.tag == "Player") {
			bunny.Damage (20);
		}
	}
}
