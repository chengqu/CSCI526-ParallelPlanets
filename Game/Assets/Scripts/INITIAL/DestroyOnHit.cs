using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnHit : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D others) {
		if (others.transform.tag == "Rock") {
			Destroy (others.gameObject);
		}
	}
}
