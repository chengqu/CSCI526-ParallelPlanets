using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonRotate2 : MonoBehaviour {

	public bool isOn = false;
	public Planet planet;

	public float delaytime = 2.0f;
	public float rotateSpeed = 20f;
	public GameObject vButtonObj; 
	private Vector3 vOriginalPosition;
	private Vector3 vTargetPosition;
	public SpriteRenderer vLightRenderer;
	// Use this for initialization
	void Start () {
		planet.vRotate = false;
		planet.vRotateSpeed = rotateSpeed;
		if (vButtonObj != null) {

			//get the calculated position to move between both position for the button to make the nice move animation smoothly
			vOriginalPosition = vButtonObj.transform.localPosition;
			vTargetPosition = vButtonObj.transform.localPosition + (Vector3.down*0.1f);

			//Get the starting position ON/OFF
			if (isOn)
				vButtonObj.transform.localPosition = vOriginalPosition + (Vector3.down*0.1f);
			else
				vButtonObj.transform.localPosition = vOriginalPosition;
		}
		if (vLightRenderer != null)
			vLightRenderer.enabled = true;
	}

	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col)
	{	
		if (col.tag == "Player" && !isOn) {
			planet.vRotate = true;
			vLightRenderer.enabled = false;
			isOn = true;
			vButtonObj.transform.localPosition = vOriginalPosition + (Vector3.down*0.1f);
			StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>
				{
					planet.vRotate = false;
				}, delaytime));
		}

	}
}
