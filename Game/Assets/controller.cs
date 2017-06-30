using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;
public class controller : MonoBehaviour {

	private Vector3 pos;  
	public float vWalkSpeed = 5f; 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		pos = Vector3.zero;
		if (CnInputManager.GetAxis("Horizontal") > 0) {
			pos += Vector3.right * vWalkSpeed * Time.deltaTime;
		}

		//check if going LEFT
		if (CnInputManager.GetAxis("Horizontal") < 0) {
			pos += Vector3.left * vWalkSpeed * Time.deltaTime;
		}
			

		if (CnInputManager.GetAxis("Vertical") > 0) {
			pos += Vector3.up * vWalkSpeed * Time.deltaTime;	
		}

			//check if going LEFT
		if (CnInputManager.GetAxis("Vertical") < 0) {
			pos += Vector3.down * vWalkSpeed * Time.deltaTime;
		}
		transform.Translate (pos);
	}
}
