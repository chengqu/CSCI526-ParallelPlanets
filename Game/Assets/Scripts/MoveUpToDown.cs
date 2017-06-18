using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpToDown : MonoBehaviour {

	public float upLimit = 2.5f;
	public float downLimit = 1.0f;
	public float speed = 2.0f;
	private int direction = 1;
	private Vector3 movement;
	private Vector3 initialPosition;
	void Start () {
		initialPosition = transform.position;
	}

	void Update () {
		if (transform.position.y > initialPosition.y + upLimit) {
			direction = -1;
		}
		else if (transform.position.y < initialPosition.y - downLimit) {
			direction = 1;
		}
		movement = Vector3.up * direction * speed * Time.deltaTime; 
		transform.Translate(movement); 
	}
}
