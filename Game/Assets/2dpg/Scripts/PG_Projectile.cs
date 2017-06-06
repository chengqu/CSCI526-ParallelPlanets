using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PG_Projectile : MonoBehaviour
{
	//public variables
	public List<AudioClip> vSoundList;										//hold a list of sound which can be randomly choosen for the shot sound. Not always the same one. use at least 3x differents sounds
	public float vDistanceGround = 0.3f;									//distance of the ground
	public float vWalkSpeed = 2f;											//how fast the projectile will go
	public float vLifeTime = 2f;											//how long will it last until it disappear?
	public PG_Character.PG_Direction vDirection = PG_Character.PG_Direction.Right;		//direction it will fly
	public bool vCanMove = false;											//prevent bullet from moving
	public bool vUseGravity = true;											//will follow the planet shapes. 
	public GameObject vImpactObj;											//put here the impact gameobject which will be spawned when the bullet die (explosion, spark, fire...)
	public List<GameObject> vChildObjList; 									//this object will lose it's parent
	public List<GameObject> vChildObjDestList; 								//this object will be destroyed on impact

	//private variables
	public SpriteRenderer vRenderer;
	private GameObject vRightObj;
	private GameObject vLeftObj;
	private GameObject vCurPlanet;
	private float vRotateSpeed = 10f;
	private float vElapsedTime = 0f;
	private AudioSource vAudioSource;
	private bool CanBeDestroyed = false;
	public bool Disabled = true;
	public int vProjectileDmg = 1;

    void Start()
    {
		vRenderer = GetComponent<SpriteRenderer> ();
		vAudioSource = GetComponent<AudioSource> ();

		if (vDirection == PG_Character.PG_Direction.Left)
			vRenderer.flipX = true;

		//keep the rotation in mind
		Vector3 vOriginalRotation = transform.rotation.eulerAngles;

		//rotate object to be normal
		transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

		//create left probe
		vLeftObj = Instantiate(Resources.Load("Probe") as GameObject);
		vLeftObj.transform.position = vRenderer.bounds.center + new Vector3(vRenderer.bounds.extents.x/2, 0f, 0f);
		vLeftObj.transform.parent = transform;
		vLeftObj.hideFlags = HideFlags.HideInHierarchy;

		//create left probe
		vRightObj = Instantiate(Resources.Load("Probe") as GameObject);
		vRightObj.transform.position = vRenderer.bounds.center + new Vector3(-vRenderer.bounds.extents.x/2, 0f, 0f);
		vRightObj.transform.parent = transform;
		vRightObj.hideFlags = HideFlags.HideInHierarchy;

		//then rotate to the original rotation
		transform.rotation = Quaternion.Euler(vOriginalRotation);

		//play a random sounds using the list
		if (vSoundList.Count > 0 && vAudioSource != null) {
			vAudioSource.clip = GetRandomClip ();
			vAudioSource.Play ();
		}
    }

	AudioClip GetRandomClip ()
	{
		//always get a random sounds to be played
		AudioClip vClip = vSoundList [Random.Range (0, vSoundList.Count - 1)];

		//return this clip
		return vClip;
	}

	public void ProjectileIsReady()
	{
		vCanMove = true;
		Disabled = false;
	}

	void Update ()
	{
		//check if ready to be destroyed
		if (CanBeDestroyed) {
			//destroy itself
			GameObject.Destroy (this.gameObject);
		}

		if (vCanMove) {
			if (vUseGravity) {
				Vector3 pos = Vector3.zero;

				//check if going RIGHT
				if (vDirection == PG_Character.PG_Direction.Right)
					pos += Vector3.right * vWalkSpeed * Time.deltaTime;

				//check if going LEFT
				if (vDirection == PG_Character.PG_Direction.Left)
					pos += Vector3.left * vWalkSpeed * Time.deltaTime;

				//move forward the bullet
				transform.Translate (pos);

				//check if we have to destroy itself because times up!
				if (vElapsedTime > vLifeTime) {
					GameObject.Destroy (this.gameObject);
				}
			} else {
				//move forward the bullet
				if (vDirection == PG_Character.PG_Direction.Right)
					transform.Translate (Vector3.right * vWalkSpeed * Time.deltaTime);
				else
					transform.Translate (Vector3.left * vWalkSpeed * Time.deltaTime);
			}

			//increase time
			vElapsedTime += Time.deltaTime;
		}
	}

	public void ChangeCurPlanet(GameObject vNewPlanet)
	{
		vCurPlanet = vNewPlanet;
		transform.parent = vNewPlanet.transform;
	}
		
	void FixedUpdate ()
	{
		if (vUseGravity && !Disabled) {
			Vector3 fwd = (Vector2)vLeftObj.transform.TransformDirection (-Vector3.up);

			//initialise variables
			float vLeftDist = 99f;
			float vRightDist = 99f;
			float vCenterDist = 0f;

			Vector3 vlpos = vRightObj.transform.position;

			//left ray
			RaycastHit2D[] hitAlll = Physics2D.RaycastAll (vlpos, (Vector2)(fwd));

			//get the first planet in range and make it's own
			if (vCurPlanet == null) {
				foreach (RaycastHit2D hit in hitAlll) {
					if (hit.transform.tag == "Planet" && vCurPlanet == null && hit.transform.gameObject != transform.gameObject)
						vCurPlanet = hit.transform.gameObject;
				}
			}

			foreach (RaycastHit2D hit in hitAlll) {
				if (vCurPlanet == hit.transform.gameObject) {
					Debug.DrawRay (vlpos, (Vector3)hit.point - vlpos, Color.blue);	
					vLeftDist = Vector3.Distance (vlpos, (Vector3)hit.point);
				}
			}

			Vector3 vrpos = vLeftObj.transform.position;

			//right ray
			RaycastHit2D[] hitAllr = Physics2D.RaycastAll (vrpos, (Vector2)(fwd));
			foreach (RaycastHit2D hit in hitAllr)
				if (vCurPlanet == hit.transform.gameObject) {
					Debug.DrawRay (vrpos, (Vector3)hit.point - vrpos, Color.red);	
					vRightDist = Vector3.Distance (vrpos, (Vector3)hit.point);
				}

			//center to be able to have some kind of gravity
			RaycastHit2D[] hitAllc = Physics2D.RaycastAll (vRenderer.bounds.center, (Vector2)(fwd));
			foreach (RaycastHit2D hit in hitAllc)
				if (vCurPlanet == hit.transform.gameObject) {
					Debug.DrawRay (vRenderer.bounds.center, (Vector3)hit.point - transform.position, Color.yellow);	
					vCenterDist = Vector3.Distance (vRenderer.bounds.center, (Vector3)hit.point);
				}

			//check if the Left or Right Probe ARE inside the collider which mean they cannot know the distance. 
			//So we just rotate them to get a distance
			if (vLeftDist == 0f)
				vRightDist = 99f;
			if (vRightDist == 0f)
				vLeftDist = 99f;

			//make sure if we doesn't find anything below the projectile, make him rotate until we find the floor!
			if (vLeftDist == 99f && vRightDist == 99f) {
				vLeftDist = 0f;
			}

			//check if we rotate the projectile
			if (vLeftDist != vRightDist) {

				//check we rotate at which speed.
				float vDiff = vLeftDist - vRightDist;
				if (vDiff < 0)
					vDiff *= -1;

				//here we calculate ow fast we must rotate the projectile. Always fast
				vRotateSpeed = 400f;

				//rotate the projectile in the direction it's going
				if (vLeftDist < vRightDist)
					RotateChar ("Left");
				else
					RotateChar ("Right");
			}

			//always keep the same distance on this new field
			if (vCenterDist > vDistanceGround) 
			//walk
			transform.Translate (-Vector3.up * vWalkSpeed * Time.deltaTime);
			else if (vCenterDist < (vDistanceGround - 0.1f))
			//make him going UP
			transform.Translate (Vector3.up * vWalkSpeed * Time.deltaTime);
		}
	}

	void RotateChar(string vDirection)
	{
		//initialise variable
		float RotateByAngle = 0f;

		//check which direction we are rotating
		if (vDirection == "Right")
			RotateByAngle = Time.deltaTime*vRotateSpeed;
		else
			RotateByAngle = -Time.deltaTime*vRotateSpeed;

		//rotate
		Vector3 temp = transform.rotation.eulerAngles;
		temp.x = 0f;
		temp.y = 0f;
		temp.z += RotateByAngle;
		transform.rotation = Quaternion.Euler(temp);
	}

	void SpawnImpact()
	{
		//remove spriterenderer
		SpriteRenderer[] vRenderers = GetComponentsInChildren<SpriteRenderer> ();

		//make it invisible
		foreach (SpriteRenderer vCurRenderer in vRenderers)
			vCurRenderer.color = new Color(1f,1f,1f,0f);

		//disable this item
		Disabled = true;
		vCanMove = false;

		//detach every child
		if (vChildObjList != null)
			foreach (GameObject vchildobj in vChildObjList) 
				if (vchildobj != null)
					vchildobj.transform.parent = null;


		while (vChildObjDestList.Count > 0) { 
			GameObject vObj = vChildObjDestList [0];
			GameObject.Destroy (vObj);
			vChildObjDestList.Remove (vObj);	//remove it from list
		}

		//wait for the sound to destroy this gameobject
		StartCoroutine (WaitForSound ());

		//only spawn impact if exist
		if (vImpactObj != null) {
			//create left probe
			GameObject vImpact = Instantiate (vImpactObj);
			vImpact.transform.position = transform.position;
			vImpact.transform.parent = transform.parent;
			vImpact.hideFlags = HideFlags.HideInHierarchy;
		}
	}

	IEnumerator WaitForSound()
	{
		while (vAudioSource.isPlaying)
			yield return null;

		//we now can destroy this gameobject
		CanBeDestroyed = true;
	}


	void OnTriggerEnter2D(Collider2D col)
	{
		if (!Disabled) {
			//check if it's the other faction
			if (col.tag == "Enemy") {
				PG_Character vTargetChar = col.GetComponent<PG_Character> ();
				vTargetChar.ReceiveDmg (vProjectileDmg);
				SpawnImpact ();
			} else if (!col.gameObject.GetComponent<PG_TeleportField> ())
			if (col.tag == "Planet" || col.tag == "Plateform") {
				SpawnImpact ();
			}
		}
	}
}
