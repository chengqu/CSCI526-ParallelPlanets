using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getchild : MonoBehaviour {

	// Use this for initialization
	void Start () {
		foreach (Transform child in gameObject.transform)
		{
			Debug.Log ("2");
			if (child.gameObject.tag == "child") {
				Debug.Log ("1");
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
