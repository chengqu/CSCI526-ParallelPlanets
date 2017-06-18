using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftToRight : MonoBehaviour {

	public float rightLimit = 2.5f;
	public float leftLimit = 1.0f;
	public float speed = 2.0f;
	private int direction = 1;
	private Vector3 movement;
	private Vector3 initialPosition;
	void Start () {
		initialPosition = transform.position;
	}

	void Update () {
		if (transform.position.x > initialPosition.x + rightLimit) {
			direction = -1;
		}
		else if (transform.position.x < initialPosition.x - leftLimit) {
			direction = 1;
		}
		movement = Vector3.right * direction * speed * Time.deltaTime; 
		transform.Translate(movement); 
	}
		
}
