using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PG_AlwaysRotate : MonoBehaviour {

	//enum
	public enum PG_Direction {Right, Left};

	public PG_Direction vDirection = PG_Direction.Right;
	public bool vRotate = false;
	public float vRotateSpeed = 20f;
		

	void Update ()
	{
		//check if we rotate the planets
		if (vRotate) {
			//initialise variable
			float RotateByAngle = 0f;

			//check which direction we are rotating
			if (vDirection == PG_Direction.Right)
				RotateByAngle = Time.deltaTime * vRotateSpeed;
			else
				RotateByAngle = -Time.deltaTime * vRotateSpeed;

			//rotate
			Vector3 temp = transform.rotation.eulerAngles;
			temp.x = 0f;
			temp.y = 0f;
			temp.z += RotateByAngle;
			transform.rotation = Quaternion.Euler (temp);
		}
	}
}
