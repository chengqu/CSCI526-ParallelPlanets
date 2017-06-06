using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;

public class PG_Character : MonoBehaviour
{
	//hold all the walking direction
	public enum PG_Direction {Right, Left};

	//public variables
	public float vDistanceGround = 0.3f;						//distance of the ground. Clouds are higher than character but still rotate!
	public float vWalkSpeed = 2f;								//how fast the character will walk
	public float vJumpSpeed = 2f;
	public float vJumpHeight = 0.5f;
	public int vHP = 10;
	public bool CanJumpOnOtherPlanets = false;					//check if we create a 2d circle collider around it to detect others planets, then if jump, we change the current planets for this character
	public bool IsPlayer = false;
	public bool IsAutoWalking = false;							//check if the player can manipulate it or is walking automatically
	public bool CanWalkOnPlateform = false;						//check if it can jump on plateform. Prevent Clouds from getting higher when passing above the plateform
	public AudioClip vImpactSound; 								//make the default sound when receiving dmg. Metal/Wood/Character/Explosion...
	public PG_Direction WalkingDirection = PG_Direction.Right;	//direction it will walk automatically
	public PG_Direction LastDirection = PG_Direction.Right;		//direction it will walk automatically
	public List<Sprite> LeftWalkAnimationList;
	public List<Sprite> RightWalkAnimationList;
	public List<PG_Weapons> WeaponList; 						//we handle all the weapons that will be used for the character here
	public int CurrentWeaponIndex = 0;
	public GameObject vProjectile;								//hold a projectile to shoot
	public List<GameObject> vSpawningPool; 						//hold a list of enemy which will be spawned every X sec
	public float vSpawningTime = 0f; 							//each X sec, it will spawn a enemy in the vSpawningPool randomly if there is more than 1
	public bool vCanMove = true;								//check if the character can move
	public GameObject vWeaponObj;
	public bool vCanUseWeapon = false;

	//private variables
	private SpriteRenderer vRenderer;
	private GameObject vRightObj;
	private GameObject vLeftObj;
	private bool IsWalking = false;
	private float animationSpeed = 0.1f;
	private float elapseanimation = 0f;
	private int vCurrentFrame = 0;
	private GameObject vCurPlanet;
	private bool IsJumping = false;
	private bool CanJump = true;
	private float vElapsedHeight = 0f;
	private float vRotateSpeed = 10f;	
	private PG_PlanetCollider vPlanetCollider;
	private bool IsReadyToChange = false;
	private Material vSpriteMat;
	private Material vBlinkMat;
	private AudioSource vAudioSource;
	private float vElapseSpawn = 0f;
	private Vector3 pos = Vector3.zero;
	private SpriteRenderer vWeaponRenderer;
	private Quaternion vStartingRotation;

    void Start()
    {
		CurrentWeaponIndex = 0;
		CanJump = true;

		//check if we use the weapon list
		if (WeaponList.Count > 0) {

			//get the renderer
			vWeaponRenderer = vWeaponObj.GetComponent<SpriteRenderer> ();

			//keep in memory the local rotation
			vStartingRotation = vWeaponObj.transform.localRotation;

			//change its weapon
			ChangeWeapon (CurrentWeaponIndex);
		}

		//initialise variable
		IsWalking = false;
		vAudioSource = GetComponent <AudioSource> ();

		vRenderer = GetComponent<SpriteRenderer> ();
		vSpriteMat = vRenderer.material;														//get back the default material
		vBlinkMat = (Material)Resources.Load("Components/DroidSansMono", typeof(Material));		//get this material which make the sprite white

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

		//check if we create a 2d Circle Collider on this character
		if (CanJumpOnOtherPlanets) {
			IsReadyToChange = false;
			GameObject vCircleCollider = Instantiate(Resources.Load("CircleCollider") as GameObject);
			vCircleCollider.transform.parent = transform;
			vCircleCollider.transform.localPosition = new Vector3 (0f, 0f, 0f);
			vPlanetCollider = vCircleCollider.GetComponent<PG_PlanetCollider> ();
			vCircleCollider.hideFlags = HideFlags.HideInHierarchy;
		}

		//update once the character starting position
		UpdateCharacterAnimation ();
    }

	public void ChangeCurPlanet(GameObject vNewPlanet)
	{
		vCurPlanet = vNewPlanet;
		transform.parent = vCurPlanet.transform;
	}

	public void ChangeWeapon(int vNewWeaponIndex)
	{
		//change its weapon index then changing the weapon
		CurrentWeaponIndex = vNewWeaponIndex;

		//change the weapon for this one
		vWeaponRenderer.sprite = WeaponList [CurrentWeaponIndex].vSprite;

		//change its rotation by default to look forward.
		vWeaponObj.transform.localRotation = vStartingRotation;
	}

	void Update ()
	{
		//check if this character can move freely or it's disabled
		if (vCanMove) {
			pos = Vector3.zero;

			//check if going RIGHT
			if ((IsPlayer && Input.GetAxis ("Horizontal") > 0 && !Input.GetButtonUp ("Horizontal")) || (IsAutoWalking && WalkingDirection == PG_Direction.Right)) {
				pos += Vector3.right * vWalkSpeed * Time.deltaTime;
				WalkingDirection = PG_Direction.Right;
			}

			//check if going LEFT
			if ((IsPlayer && Input.GetAxis ("Horizontal") < 0 && !Input.GetButtonUp ("Horizontal")) || (IsAutoWalking && WalkingDirection == PG_Direction.Left)) {
				pos += Vector3.left * vWalkSpeed * Time.deltaTime;
				WalkingDirection = PG_Direction.Left;
			}

			//make sure were using the weapon list
			if (LastDirection != WalkingDirection && WeaponList.Count > 0) {

				//update last direction for the current new direction
				LastDirection = WalkingDirection;

				//change rotation of the weapon
				if (vWeaponObj != null) {
					Quaternion vNewRotation = vWeaponObj.transform.localRotation;
					if (WalkingDirection == PG_Direction.Left)
						vWeaponRenderer.flipX = true;
					else
						vWeaponRenderer.flipX = false;

					//change its rotation
					vWeaponObj.transform.localRotation = vNewRotation;
				}
			}

			//if spacebar, create a projectile going on players
			if (Input.GetMouseButtonDown (0) && vCanUseWeapon) 
			if (WeaponList.Count-1 >= CurrentWeaponIndex)
			if (WeaponList[CurrentWeaponIndex].vProjectile != null){
				//create the projectile which will move in the same direction as this character and hit other characters
				GameObject vNewProj = Instantiate (WeaponList[CurrentWeaponIndex].vProjectile);
				PG_Projectile vProj = vNewProj.GetComponent<PG_Projectile> ();
				vProj.vProjectileDmg = WeaponList [CurrentWeaponIndex].vDmgValue;
				vProj.vDirection = WalkingDirection;				
				vNewProj.transform.position = transform.position;

				//send to the projectile if the weapon use the gravity
				vProj.vUseGravity = WeaponList [CurrentWeaponIndex].UseGravity;

				//make him a child ONLY if we use the gravity
				if (vProj.vUseGravity)
					vNewProj.transform.parent = transform.parent.transform;	
				
				vNewProj.transform.rotation = vWeaponObj.transform.rotation;
				vNewProj.transform.localScale = transform.localScale;
				vProj.ProjectileIsReady ();
			}

			//check if JUMP
			if (IsPlayer && Input.GetAxis ("Vertical") > 0 && !IsJumping && CanJump) {
				IsJumping = true;
				CanJump = false;
				vElapsedHeight = 0f;
				IsReadyToChange = true;

				//check if there is a nearby planet if JUMP and CAN change planets is activated
				if (CanJumpOnOtherPlanets)
					CheckIfNearbyPlanet ();
			}

			//check if the character is walking
			if (pos != Vector3.zero)
				IsWalking = true;
			else
				IsWalking = false;

			//ONLY show walking animation when moving!
			if (IsWalking) {
				//move
				transform.Translate (pos);

				//increase time
				elapseanimation += Time.deltaTime;
				if (elapseanimation >= animationSpeed) {
					UpdateCharacterAnimation ();
					elapseanimation = 0f;
				}
			}

			//ONLY use the spawning pool if there is something to spawn!
			if (vSpawningPool.Count > 0) {
				//spawn a random mobs in the list every x sec vSpawningTime
				vElapseSpawn += Time.deltaTime;
				if (vElapseSpawn >= vSpawningTime) {

					//create the projectile which will move in the same direction as this character and hit other characters
					GameObject vNewSpawn = Instantiate (vSpawningPool [(int)Random.Range (0f, (float)(vSpawningPool.Count))]);
					PG_Character vChar = vNewSpawn.GetComponent<PG_Character> ();
					int vRandomDir = Random.Range (0, 2);
					vChar.WalkingDirection = (PG_Character.PG_Direction)vRandomDir;
					vNewSpawn.transform.position = transform.position;
					vNewSpawn.transform.localRotation = transform.localRotation;
					vNewSpawn.transform.parent = transform.parent.transform;
					vNewSpawn.transform.localScale = transform.localScale;

					//reset velapsespawn to wait again X sec
					vElapseSpawn = 0f;
				}
			}
		}
	}

	public void PlaySound (AudioClip vClip)
	{
		if (vAudioSource != null)
		{
			vAudioSource.clip = vClip;
			vAudioSource.Play ();
		}
	}

	IEnumerator BlinkEffect()
	{
		int cpt = 0;
		int cptMax = 6;

		while (cpt < cptMax) {
			//white color
			vRenderer.material = vBlinkMat;
			yield return new WaitForSeconds (0.05f);

			//normal color
			vRenderer.material = vSpriteMat;
			yield return new WaitForSeconds (0.05f);

			cpt++;
		}
	}

	IEnumerator	CharacterDie ()
	{
		//just stop walking 
		IsAutoWalking = false;
		IsWalking = false;

		//make him RED color
		vRenderer.material = vBlinkMat;
		vRenderer.color = Color.red;
		yield return new WaitForSeconds (0.1f);

		float vAlpha = 1;
		while (vAlpha > 0f)
		{
			vAlpha -= 0.05f; 
			yield return new WaitForSeconds (0.01f);
			ChangeAlpha (vAlpha); //make him dissapear
		}

		//destroy this character once and for good!
		GameObject.Destroy (gameObject);
	}

	void ChangeAlpha (float vNewAlpha)
	{
		//get all the spriterenderer in character and it's children
		SpriteRenderer[] vRenderer = GetComponentsInChildren<SpriteRenderer> ();

		//change the color for the whole character alpha
		foreach (SpriteRenderer vCurRenderer in vRenderer)
			vCurRenderer.color = new Color(1f,1f,1f,vNewAlpha);
	}

	public void ReceiveDmg(int vDmg)
	{
		//play the impact sounds
		PlaySound (vImpactSound);

		//remove HP
		vHP -= vDmg;
		if (vHP <= 0) {
			//character die here
			StartCoroutine(CharacterDie());
		}
		else
			//do blink dmg animation ONLY if not dead
			StartCoroutine(BlinkEffect());
	}

	void UpdateCharacterAnimation()
	{
		//get the right list ot use
		List<Sprite> vCurAnimList; 
		if (WalkingDirection == PG_Direction.Right)
			vCurAnimList = RightWalkAnimationList;
		else
			vCurAnimList = LeftWalkAnimationList;

		if (vCurrentFrame + 1 >= vCurAnimList.Count)
			vCurrentFrame = 0;
		else
			vCurrentFrame++;

		//then change the sprite correctly
		if (vCurAnimList.Count > 0)
			vRenderer.sprite = vCurAnimList[vCurrentFrame];
	}

	void FixedUpdate ()
	{
		Vector3 fwd = (Vector2)vLeftObj.transform.TransformDirection(-Vector3.up);

		//initialise variables
		float vLeftDist = 99f;
		float vRightDist = 99f;
		float vCenterDist = 0f;

		Vector3 vlpos = vRightObj.transform.position;

		//left ray
		RaycastHit2D[] hitAlll = Physics2D.RaycastAll(vlpos, (Vector2)(fwd));

		//get the first planet in range and make it's own
		if (vCurPlanet == null) {
			foreach (RaycastHit2D hit in hitAlll) {
				if (hit.transform.tag == "Planet" && vCurPlanet == null && hit.transform.gameObject != transform.gameObject)
					vCurPlanet = hit.transform.gameObject;
			}
		}

		foreach (RaycastHit2D hit in hitAlll){
			if (vCurPlanet == hit.transform.gameObject) {
				Debug.DrawRay (vlpos, (Vector3)hit.point - vlpos, Color.blue);	
				vLeftDist = Vector3.Distance (vlpos, (Vector3)hit.point);
			}
		}

		Vector3 vrpos = vLeftObj.transform.position;

		//right ray
		RaycastHit2D[] hitAllr = Physics2D.RaycastAll(vrpos, (Vector2)(fwd));
		foreach (RaycastHit2D hit in hitAllr)
		if (vCurPlanet == hit.transform.gameObject) {
				Debug.DrawRay (vrpos, (Vector3)hit.point-vrpos, Color.red);	
				vRightDist = Vector3.Distance(vrpos,(Vector3)hit.point);
		}

		//center to be able to have some kind of gravity
		RaycastHit2D[] hitAllc = Physics2D.RaycastAll(vRenderer.bounds.center, (Vector2)(fwd));
		bool vFoundGround = false;
		foreach (RaycastHit2D hit in hitAllc)
			if (vCurPlanet == hit.transform.gameObject || ((hit.transform.tag == "Plateform" || hit.transform.tag == "Pushable") && CanWalkOnPlateform)) {
				Debug.DrawRay (vRenderer.bounds.center, (Vector3)hit.point-transform.position, Color.yellow);	
				if (Vector3.Distance (vRenderer.bounds.center, (Vector3)hit.point) < vCenterDist || !vFoundGround) {
					vCenterDist = Vector3.Distance (vRenderer.bounds.center, (Vector3)hit.point);
					vFoundGround = true;
				}
			}

		//rotate weapon if have one
		Vector3 vMousePosition = Input.mousePosition;
		if (vWeaponObj != null) {
			Vector3 vWeaponPosition = vWeaponObj.transform.position;

			//calcualte the angle
			Vector3 pos = Camera.main.WorldToScreenPoint (vWeaponPosition);
			Vector3 dir = vMousePosition - pos;

			Quaternion newRotation = Quaternion.LookRotation (dir, Vector3.forward);
			newRotation.x = 0f;
			newRotation.y = 0f;

			float vNewAngle = 0f;

			//rotate a little bit more left & right walk movement
			if (WalkingDirection == PG_Direction.Right)
				vNewAngle = newRotation.eulerAngles.z - 90f;
			else
				vNewAngle = newRotation.eulerAngles.z - 270f;

			newRotation = Quaternion.Euler (0f, 0f, vNewAngle);

			//check if we can rotate there
			if (CanRotate (vNewAngle))
				vWeaponObj.transform.rotation = Quaternion.Slerp (vWeaponObj.transform.rotation, newRotation, 1f);
		}

		//check if the Left or Right Probe ARE inside the collider which mean they cannot know the distance. 
		//So we just rotate them to get a distance
		if (vLeftDist == 0f) vRightDist = 99f;
		if (vRightDist == 0f) vLeftDist = 99f;

		//make sure if we doesn't find anything below the character, make him rotate until we find the floor!
		if (vLeftDist == 99f && vRightDist == 99f) {
			vLeftDist = 0f;
		}

		//check if we rotate the character
		if (vLeftDist != vRightDist) {

			//check we rotate at which speed.
			float vDiff = vLeftDist-vRightDist;
			if (vDiff < 0)
				vDiff *= -1;

			//here we calculate how fast we must rotate the character
			if (vDiff < 0.2f)
				vRotateSpeed = 30f;				//small rotation to be smooth and be able to have the same exact position between Left and Right
			else if (vDiff >= 0.2f && vDiff < 0.4f)
				vRotateSpeed = 80f;				//need to turn a little bit faster
			else
				vRotateSpeed = 400f;			//we must turn VERY quick because it's a big corner

			//rotate the character in the direction it's going
			if (vLeftDist < vRightDist)
				RotateObj ("Left");
			else
				RotateObj ("Right");
		}

		//always keep the same distance on this new field
		if (vCenterDist > vDistanceGround && !IsJumping) {
			//walk
			transform.Translate (-Vector3.up * vJumpSpeed * Time.deltaTime);
		} else if (IsJumping) {	//going to the jumpheight
			//make him going UP
			transform.Translate (Vector3.up * vJumpSpeed * Time.deltaTime);

			//increase jump time
			vElapsedHeight += Time.deltaTime * vJumpSpeed;

			//check if we jumped enought
			if (vElapsedHeight >= vJumpHeight) {
				IsJumping = false;
				IsReadyToChange = false;
			}
		} else if (!CanJump && Input.GetAxis ("Vertical") == 0) {
			CanJump = true;
		}
		else if (vCenterDist < (vDistanceGround-0.1f) && !IsJumping){
			//make him going UP
			transform.Translate (Vector3.up * vJumpSpeed * Time.deltaTime);
		}
	}

	void CheckIfNearbyPlanet()
	{
		bool vFound = false;

		foreach (GameObject vPlanet in vPlanetCollider.vPlanetList)
			if (vPlanet != vCurPlanet && !vFound && vPlanet != transform.gameObject) {
				//we found a planet. we transfert to the first one not in the range.
				vFound = true;
				IsReadyToChange = false; //cannot change planet without making another jump. Prevent the character to be stuck between 2 planets
				//change the planet
				vCurPlanet = vPlanet;

				//make sure the character scale isn't changed between planets
				transform.parent = vCurPlanet.transform;
			}
	}
		
	//check if we rotate or not
	bool CanRotate(float angle)
	{
		bool vCanRotate = false;

		if (!WeaponList [CurrentWeaponIndex].UseGravity) {

			//get the player rotation
			angle = angle - transform.rotation.eulerAngles.z;

			//make sure the calculated angle is in the 180f/-180f range.
			if (angle < -180f)
				angle += 360f;
			if (angle > 180f)
				angle -= 360f;

			//cannot rotate gun behind character.
			if (angle <= 85f && angle >= -85f)
				vCanRotate = true;
		}

		return vCanRotate;
	}

	void RotateObj(string vDirection)
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
}
