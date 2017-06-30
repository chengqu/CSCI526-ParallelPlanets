using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonRotate : MonoBehaviour {

	public bool isOn = false;
	public Planet planet;

	public GameObject vButtonObj; 
	private Vector3 vOriginalPosition;
	private Vector3 vTargetPosition;
	private List<GameObject> vNearbyPlayer;
	// Use this for initialization
	void Start () {

		//planet.vRotate = false;
//>>>>>>> eca037170a05ce2fd44de1958161b1d6b97068b3

		vNearbyPlayer = new List<GameObject> ();
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
	}
	
	// Update is called once per frame
	void Update () {
		if (isOn) {
			planet.vRotate = true;
			vButtonObj.transform.localPosition = vOriginalPosition + (Vector3.down * 0.1f);
		} else {
			planet.vRotate = false;
			vButtonObj.transform.localPosition = vOriginalPosition;
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
//		
		//CANNOT click AGAIN on the button until the player leave the button.
		if (col.tag == "Player" && !vNearbyPlayer.Contains(col.gameObject)) {
			//add the player
			vNearbyPlayer.Add (col.gameObject);
			isOn = !isOn;
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		//remove the gameobject
		if (vNearbyPlayer.Contains(col.gameObject))
			vNearbyPlayer.Remove (col.gameObject);
	}

}
