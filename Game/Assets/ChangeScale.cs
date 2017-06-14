using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScale : MonoBehaviour {

	public GameObject vCurParent;
	// Use this for initialization
	void Start () {
		vCurParent = transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col.transform.tag == "PlanetScale" && col.transform.gameObject != vCurParent) {
			transform.parent = col.transform;
		}
	}
}
