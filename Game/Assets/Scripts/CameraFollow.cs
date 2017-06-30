using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	Camera mycam;
	public float size = 1f;
	// Use this for initialization
	void Start () {
		mycam = GetComponent<Camera> ();

	}
	
	// Update is called once per frame
	void Update () {


		mycam.orthographicSize = (Screen.height / 100f) / size;

		if (target) {
			transform.position = Vector3.Lerp (transform.position, target.position, 0.1f) + new Vector3(0, 0, -10f);
		}
	}
}
