using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHIt : MonoBehaviour {

	public PlayerController bunny;
	public Cloud cloud;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnParticleCollision (GameObject other) {
		if (other.tag == "Player") {
			bunny = other.GetComponent<PlayerController> ();
			bunny.Damage (20);
		}
		if (other.tag == "Cloud") {
			cloud = other.GetComponent<Cloud> ();
			cloud.Damage (2);
		}
	}

}
