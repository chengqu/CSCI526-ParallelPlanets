using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeCameraView : MonoBehaviour {

	public float size;
	public Camera myCamera;
	public Slider healthBar;
	// Use this for initialization
	void Start () {
		myCamera = GetComponent<Camera> ();
		size = myCamera.orthographicSize;
	}
	
	// Update is called once per frame
	void Update () {
		size = healthBar.value;
		myCamera.orthographicSize = size;
	}
}
