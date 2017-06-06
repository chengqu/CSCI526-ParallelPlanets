using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PG_Switch : MonoBehaviour {

	public enum LinkFunction {PlanetScale, ChangePlanet};

	//public
	public GameObject vButtonObj; 
	public AudioClip vClickSound;
	public bool vSwitchStatus = false;
	public PG_Switch vSwitchLink;
	public LinkFunction vLinkFunction = LinkFunction.PlanetScale;			//use a custom function which will be sent to the vLinkObj. 
	public GameObject vLinkObj; 											//put here your gameobject, but we will use a different components to send the info to the custom function
	public SpriteRenderer vLightRenderer;

	//private
	private Vector3 vOriginalPosition;
	private Vector3 vTargetPosition;
	private List<GameObject> vNearbyPlayer;
	private AudioSource vAudioSource;

	// Use this for initialization
	void Start () {

		//initialise list
		vNearbyPlayer = new List<GameObject> ();
		vAudioSource = GetComponent<AudioSource> ();
		
		if (vButtonObj != null) {
			
			//get the calculated position to move between both position for the button to make the nice move animation smoothly
			vOriginalPosition = vButtonObj.transform.localPosition;
			vTargetPosition = vButtonObj.transform.localPosition + (Vector3.down*0.3f);

			//Get the starting position ON/OFF
			if (!vSwitchStatus)
				vButtonObj.transform.localPosition = vOriginalPosition + (Vector3.down*0.3f);
			else
				vButtonObj.transform.localPosition = vOriginalPosition;
		}

		//if there is a light attached, we show it
		if (vLightRenderer != null)
			vLightRenderer.enabled = vSwitchStatus;
	}


	public void PlaySound (AudioClip vClip)
	{
		if (vAudioSource != null)
		{
			vAudioSource.clip = vClip;
			vAudioSource.Play ();
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		//CANNOT click AGAIN on the button until the player leave the button.
		if (col.tag == "Player" && !vNearbyPlayer.Contains(col.gameObject) && vSwitchStatus) {
			//add the player
			vNearbyPlayer.Add (col.gameObject);
			ChangeSwitch (true);
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		//remove the gameobject
		if (vNearbyPlayer.Contains(col.gameObject))
			vNearbyPlayer.Remove (col.gameObject);
	}

	void UseLinkFunction ()
	{
		//check if there is a linkobj before. prevent crash here.
		if (vLinkObj != null)
		switch (vLinkFunction)
		{
			//when scaling planets
			case LinkFunction.PlanetScale: 
				vLinkObj.GetComponent<PG_Planet> ().CallChangeScale ();
			break;

			//when scaling planets
			case LinkFunction.ChangePlanet: 
				if (!vSwitchStatus) {

					//create temp list
					List<PG_Character> vCharList = new List<PG_Character> ();
					List<PG_Gravity> vGravList = new List<PG_Gravity> ();

					//get all character & gravity objects and change their gravity planet.
					foreach (Transform child in transform.parent) {
				
						//PG_Character
						if ((child.tag == "Enemy" || child.tag == "Player") && child.GetComponent<PG_Character> ())
							vCharList.Add (child.GetComponent<PG_Character> ());

						//PG_Gravity
						if (child.GetComponent<PG_Gravity> ())
							vGravList.Add (child.GetComponent<PG_Gravity> ());
					}

					//then change all the character & Items planets gravity
					foreach (PG_Character vCurChar in vCharList)
						vCurChar.ChangeCurPlanet (vLinkObj);
					foreach (PG_Gravity vCurGravity in vGravList)
						vCurGravity.ChangeCurPlanet (vLinkObj);
				}
			break;

		}
	}

	//change the switch status ON, OFF
	void ChangeSwitch(bool vChoice)
	{
		//toggle the status ON/OFF
		vSwitchStatus = !vSwitchStatus;

		//if there is a light attached, we show it
		if (vLightRenderer != null)
			vLightRenderer.enabled = vSwitchStatus;

		//send the info to the component
		UseLinkFunction ();

		//play the impact sounds
		if (vChoice);
			PlaySound(vClickSound);

		//prevent the game to crash if there is no obj
		if (vButtonObj != null) {

			//when finishing, switch ON the other switch... 
			//if there is no linked switch, it will skip this
			if (vSwitchLink != null && vChoice) {
				vSwitchLink.ChangeSwitch (false);
			}

			//then activate this buttons
			StartCoroutine (MoveSwitch (vChoice));
		}
	}

	IEnumerator MoveSwitch(bool vChoice)
	{
		float vElapseTime = 0f;
		float vTimeToMove = 1f;

		while (vElapseTime < vTimeToMove) {

			//check if we open/close the button
			if (!vSwitchStatus)
				vButtonObj.transform.localPosition = Vector3.Lerp (vOriginalPosition, vTargetPosition, (float)(vElapseTime / vTimeToMove));
			else
				vButtonObj.transform.localPosition = Vector3.Lerp (vTargetPosition, vOriginalPosition, (float)(vElapseTime / vTimeToMove));

			//wait some time
			yield return new WaitForSeconds (0.01f);

			//increase time
			vElapseTime += Time.deltaTime;
		}
	}
}
