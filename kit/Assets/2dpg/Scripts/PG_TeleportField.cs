using UnityEngine;
using System.Collections;

public class PG_TeleportField : MonoBehaviour {

	public AudioClip vTeleportSound;
	public GameObject vLinkedObj; 					//other teleportfields. Can add as many teleport as you want. Just need to make hte LinkedObj the next one
	public bool TempDisable = false;				//check if it's temporary disabled 

	private AudioSource vAudioSource;
	private float vOrignalScale = 1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
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
		//check if it's the other faction
		if (CanBeTeleported (col.gameObject) && vLinkedObj != null) {
			
			StartCoroutine (TeleportPlayerEffect (col.gameObject));
		}
	}

	void ToggleComponentsObj(GameObject vObj, bool vChoice, GameObject vNewPlanet = null)
	{
		//character
		if (vObj.GetComponent<PG_Character> ()) {
			vObj.GetComponent<PG_Character> ().vCanMove = vChoice;

			if (vNewPlanet != null)
				vObj.GetComponent<PG_Character> ().ChangeCurPlanet (vNewPlanet);
		}

		//projectiles
		if (vObj.GetComponent<PG_Projectile> ()) {
			vObj.GetComponent<PG_Projectile> ().vCanMove = vChoice;

			if (vNewPlanet != null)
				vObj.GetComponent<PG_Projectile> ().ChangeCurPlanet (vNewPlanet);
		}

		//object
		if (vObj.GetComponent<PG_Object> ()) {
			vObj.GetComponent<PG_Object> ().vCanMove = vChoice;

			if (vNewPlanet != null)
				vObj.GetComponent<PG_Object> ().ChangeCurPlanet (vNewPlanet);
		}
	}

	IEnumerator TeleportPlayerEffect(GameObject vObj)
	{
		vOrignalScale = vObj.transform.localScale.x;

		//disable all these components
		ToggleComponentsObj(vObj, false);

		//Warp IN
		yield return StartCoroutine (TeleportEffectOnPlayer ("WarpIn",vObj));

		GameObject vCurObj = vObj;

		//if it's a gameobject, get it's parent instead
		if (vObj != null) {
			if (vObj.GetComponent<PG_Object> () != null)
				vCurObj = vObj.transform.parent.gameObject;

			//change obj position
			vCurObj.transform.position = vLinkedObj.transform.position;
			vCurObj.transform.position = (Vector2)vObj.transform.position + (Vector2)vLinkedObj.transform.TransformDirection (Vector3.up * .5f);
			vCurObj.transform.parent = vLinkedObj.transform.parent.transform;
			vCurObj.transform.localRotation = vLinkedObj.transform.localRotation;

			//change planets for the teleports
			//for character
			PG_Character vChar = vObj.GetComponent<PG_Character> ();
			if (vChar != null)
				vChar.ChangeCurPlanet (vLinkedObj.transform.parent.gameObject);

			//for projectile
			PG_Projectile vProj = vObj.GetComponent<PG_Projectile> ();
			if (vProj != null)
				vProj.ChangeCurPlanet (vLinkedObj.transform.parent.gameObject);

			//for projectile
			PG_Object vPGObj = vObj.GetComponent<PG_Object> ();
			if (vPGObj != null)
				vPGObj.ChangeCurPlanet (vLinkedObj.transform.parent.gameObject);

			//check if we teleport the player using the teleport effect
			//go back to it's original 
			yield return StartCoroutine (TeleportEffectOnPlayer ("WarpOut", vObj));

			//re-enable all these components if exist
			ToggleComponentsObj (vObj, true);
		}
	}

	IEnumerator TeleportEffectOnPlayer(string vChoice, GameObject vObj)
	{
		bool vIsScaling = true;
		if (vObj.GetComponent<PG_Projectile> ())
			vIsScaling = false;

		int vcptMax = 6;
		int vcpt = 0;

		if (vObj != null)
		//check if the player warp in or warp out
		if (vChoice == "WarpIn") { //WarpIn
			while (vcpt < vcptMax) 
			{
				vcpt++;
				if (vIsScaling)
					vObj.transform.localScale -= new Vector3(0.15f, -0.15f, 0f); //decrease X, but increase Y
				yield return null;
			}
		} else { //Warp Out
			while (vcpt < vcptMax) 
			{
				vcpt++;
				if (vIsScaling)
					vObj.transform.localScale -= new Vector3(-0.15f, 0.15f, 0f); //decrease X, but increase Y
				yield return null;
			}

			//go back to it's original 
			vObj.transform.localScale = new Vector3 (1f, 1f, 1f);
		}
	}

	bool CanBeTeleported (GameObject vObj)
	{
		bool vCanBeTeleported = false;

		//ONLY teleport these tags below
		if (((vObj.tag == "Player" || vObj.tag == "Enemy") && vObj.GetComponent<PG_Character>().vCanMove) 
			|| (vObj.tag == "Projectile" && vObj.GetComponent<PG_Projectile>().vCanMove)
			|| (vObj.tag == "Object" && vObj.GetComponent<PG_Object>().vCanMove)) {
			vCanBeTeleported = true;
		}

		//return value
		return vCanBeTeleported;
	}
}
