using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Planet : MonoBehaviour
{
	//enum
	public enum PG_Direction {Right, Left};

	//hold all the walking direction
	public bool vRotate = false;
	public float vRotateSpeed = 20f;
	public float vIncreaseSize = 1f;
	public PG_Direction vDirection = PG_Direction.Right;

	private List<GameObject> vTempList;
	private Vector3 vOriginalSize = Vector3.zero;
	private Vector3 vTargetSize = Vector3.zero;
	private bool vChangedScale = false;
	private SpriteRenderer vRenderer;
	private Material vSpriteMat;
	private Material vBlinkMat;
	private AudioSource vAudioSource;

	void Start()
	{
		//always check the X value. Every planets must have equal value on the scale. X=1f, Y = 0.85f = wrong. X = 1f, Y = 1f will make all the sprite works correctly
		vOriginalSize = transform.localScale;
		vTargetSize = transform.localScale * vIncreaseSize;
		vRenderer = transform.GetComponent<SpriteRenderer> ();
		vSpriteMat = vRenderer.material;														//get back the default material

	}

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
			foreach (Transform child in gameObject.transform)
			{
				if (child.gameObject.tag == "obstacal") {
					child.transform.rotation= Quaternion.Euler (temp);
				}
			}
		}
	}

}
