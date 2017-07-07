using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;
using UnityEngine.UI;
using AssemblyCSharp;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {

	public bool findTarget = false;

    //public variables

	public float moveSpeed = 5f;
	public float jumpForce = 50f;

	//in game variables
	public enum PG_Direction {Right, Left};   //directions the player can walk
    public PG_Direction WalkingDirection = PG_Direction.Right;  //set initial walking direction to right
	public PG_Direction LastDirection = PG_Direction.Right;		//direction it will walk automatically
    public List<GameObject> vPlanetList;        //a list of planet
    public GameObject vCurPlanet;               //gameobject to store the palyer's current planet
    public GameObject vCurField;                //gameobject to store the player's current gravity field

    public float vJumpHeight = 1f;              //The limit of height the player can jump
	public float vJumpSpeed = 6f;               //the speed the player can jump
	public float vDistanceGround = 2f;          //distance of the ground. Clouds are higher than character but still rotate!
    public float vWalkSpeed = 5f;               //how fast the character will walk
    public bool CanJump = true;                 //check if the player can jump
    public bool CanJumpOnOtherPlanets = true;   //check if the player can jump on other planets
    public float vElapsedHeight = 0f;           //elapsed jumping height
    public bool IsJumping = false;              //check if player is jumping
    public bool IsReadyToChange = false;        //check if the player movement ready to change

    public bool vCanMove = true;                //check if the character can move
    public bool CanWalkOnPlateform = false;     //check if it can jump on plateform. Prevent Clouds from getting higher when passing above the plateform
    public bool IsAutoWalking = false;          //check if the player can manipulate it or is walking automatically
    public bool IsPlayer = true;                //check if its the player

	//Controller
	public GameObject direction4;
	public GameObject direction2;
	public GameObject fireButton;
	public GameObject jumpButton;

    public List<Sprite> LeftWalkAnimationList;
    public List<Sprite> RightWalkAnimationList;
    public List<Sprite> DieAnimationList;
    public List<Sprite> DamageAnimationList;
    public List<Sprite> JumpAnimationList;

    public List<PG_Weapons> WeaponList;                         //we handle all the weapons that will be used for the character here
    public int CurrentWeaponIndex = 0;
    public GameObject vProjectile;								//hold a projectile to shoot
    public GameObject vWeaponObj;
    public bool vCanUseWeapon = false;

    public bool isDead = false;
    public bool isDamage = false;
    //sound effect variables
    public AudioSource jumpSfx;
	public AudioSource deathSfx;
	public AudioSource hitSfx;


    //private variables
    private Vector3 vGravityDirection;
    private Vector3 vFacingDirection;
    private bool deathFlag = true;
	private bool jumpFlag = false;
    private bool moveLeft, moveRight, doJump = false;
    private SpriteRenderer vWeaponRenderer;

    private float elapseanimation = 0f;         //elapsed walking animation
	private float animationSpeed = 0.1f;        //walk animation speed
    private int vCurrentFrame = 0;
    
    private Vector3 pos;                        //init the position as a 3d vector
	private Quaternion rotation;                //init the rotation of player

	private float vCenterDist;                  //the dist between player center to the ground
	private float vLeftDist;                    //dist between player center to the left hitpoint
	private float vRightDist;                   //dist between player center tot the right hitpoint
	private float vRotateSpeed = 10f;           //rotate speed of player	

	private bool IsWalking;                     //check if player is walking
    private bool FoundNearbyPlanet;              //check if a nearby planet found
               
    private GameObject vLeftObj, vRightObj;     //the left probe and right probe of the player to detect planets
	private GameObject vCircleCollider;         //player's circle collider
	private Rigidbody2D myRigidBody;            //player's rigid body
	private SpriteRenderer myRenderer;          //player's sprite renderer
    private Quaternion vStartingRotation;

    //healthbar
    public Slider healthBar;
	public float curHealth = 100;
	private float maxHealth = 100;
	//shin
	private SpriteRenderer vRenderer;
	private Material vSpriteMat;
	private Material vBlinkMat;
	// jetcraft variable
	public float jetPos = 1f;
	private bool JetCraft = false;
	private float currentAmount = 0;
	private float speed = 20;
	private float MovementSpeed = 1f;
	public Vector3 testV3;
	public GameObject jetpack;
	private SpriteRenderer jetRenderer;
	public GameObject explosion;
    public GameObject panel;
   

	public void Damage(float damage) {
	//	hitSfx.Play ();
		curHealth -= damage;
		healthBar.value = curHealth/maxHealth;
		if (curHealth <= 0) {
			isDead = true;
		}
		StartCoroutine(BlinkEffect(vRenderer));
	}
	// Use this for initialization
	void Start () {

        CurrentWeaponIndex = 0;
		fireButton.SetActive (false);
		direction4.SetActive (false);
		jetpack.SetActive (false);
		explosion.SetActive (false);
		vRenderer = GetComponent<SpriteRenderer> ();
		jetRenderer = jetpack.GetComponent<SpriteRenderer> ();
		vSpriteMat = vRenderer.material;														//get back the default material
		vBlinkMat = (Material)Resources.Load("Components/DroidSansMono", typeof(Material));	
        //check if we use the weapon list
        if (WeaponList.Count > 0)
        {

            //get the renderer
            vWeaponRenderer = vWeaponObj.GetComponent<SpriteRenderer>();

            //keep in memory the local rotation
            vStartingRotation = vWeaponObj.transform.localRotation;

            //change its weapon
            ChangeWeapon(CurrentWeaponIndex);
        }

        findTarget = false;
		myRigidBody = GetComponent<Rigidbody2D> ();
		myRenderer = GetComponent<SpriteRenderer> ();
		vCurPlanet = null;
        vCurField = null;
		//keep the rotation in mind
		Vector3 vOriginalRotation = transform.rotation.eulerAngles;

		//rotate object to be normal
		transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

		//create left probe
		vLeftObj = Instantiate(Resources.Load("Probe") as GameObject);
		vLeftObj.transform.position = myRenderer.bounds.center + new Vector3(myRenderer.bounds.extents.x/2, 0f, 0f);
		vLeftObj.transform.parent = transform;

		//create left probe
		vRightObj = Instantiate(Resources.Load("Probe") as GameObject);
		vRightObj.transform.position = myRenderer.bounds.center + new Vector3(-myRenderer.bounds.extents.x/2, 0f, 0f);
		vRightObj.transform.parent = transform;

		//then rotate to the original rotation
		transform.rotation = Quaternion.Euler(vOriginalRotation);

		IsReadyToChange = false;
        UpdateCharacterAnimation();

		healthBar.value = curHealth/maxHealth;
    }

	IEnumerator BlinkEffect(SpriteRenderer vRenderer)
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

	//change current planet

	public void ChangeCurPlanet(GameObject vNewPlanet)
	{
		vCurPlanet = vNewPlanet;
        vCurField = vNewPlanet.transform.parent.gameObject;
        //set the parent of the player the planet he's currently on
		transform.parent = vCurPlanet.transform;
	}
		
	// Update is called once per frame
	void Update () {

		if (isDead) {

            UpdateCharacterAnimation();
      
            Die ();
		}
			
		if (JetCraft) {
			direction4.SetActive (true);
			direction2.SetActive (false);
			if (currentAmount < 100) {
				jetpack.SetActive (true);
				transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
				float vJetSpeed = 1f;
				currentAmount += speed * Time.deltaTime;
				//vJumpHeight = 3f;
				CanJump = false;
				Vector3 movementVector = new Vector3(CnInputManager.GetAxis("Horizontal"), CnInputManager.GetAxis("Vertical"));
				testV3 = movementVector;
				transform.Translate (movementVector * vJetSpeed * Time.deltaTime);
				if (currentAmount > 80) {
					StartCoroutine(BlinkEffect(jetRenderer));
				}
				if (CnInputManager.GetButtonDown ("Jump")) {
					jetpack.SetActive (false);
					JetCraft = false;
					vJumpHeight = 1f;
					CheckIfNearbyPlanet ();
				}
			} else {
				jetpack.SetActive (false);
				Damage (30);
				explosion.SetActive (true);
				JetCraft = false;
				vJumpHeight = 1f;
				CheckIfNearbyPlanet ();
				StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>
					{
						explosion.SetActive (false);
					}, 2.0f));
			}

        	if (isDamage)
        	{

            	UpdateCharacterAnimation();

            	isDamage = false;
        	}
				

		} else {
			direction4.SetActive (false);
			direction2.SetActive (true);
			//check if this character can move freely or it's disabled
			if (vCanMove) {
				pos = Vector3.zero;

				//check if going RIGHT
				if ((IsPlayer && CnInputManager.GetAxis ("Horizontal") > 0) || (IsPlayer && Input.GetAxis ("Horizontal") > 0 && !Input.GetButtonUp ("Horizontal")) || (IsAutoWalking && WalkingDirection == PG_Direction.Right)) {
					pos += Vector3.right * vWalkSpeed * Time.deltaTime;
					WalkingDirection = PG_Direction.Right;
                }

				//check if going LEFT
				if ((IsPlayer && CnInputManager.GetAxis ("Horizontal") < 0) || (IsPlayer && Input.GetAxis ("Horizontal") < 0 && !Input.GetButtonUp ("Horizontal")) || (IsAutoWalking && WalkingDirection == PG_Direction.Left)) {
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
			if ((CnInputManager.GetButtonDown("Fire1") && vCanUseWeapon) && (WeaponList.Count - 1 >= CurrentWeaponIndex) && (WeaponList[CurrentWeaponIndex].vProjectile != null)) {
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

				vNewProj.transform.rotation = transform.rotation;
				vNewProj.transform.localScale = transform.localScale;
				vProj.ProjectileIsReady ();
                //vCanUseWeapon = false;
			}

				//check if JUMP
				if ((IsPlayer && (CnInputManager.GetButtonDown ("Jump") || Input.GetAxis ("Vertical") > 0)) && !IsJumping && CanJump) {
					IsJumping = true;
					CanJump = false;
					vElapsedHeight = 0f;
					IsReadyToChange = true;
					UpdateCharacterAnimation ();

					if (jumpFlag) {
						jumpSfx.Play ();
					}
					jumpFlag = true;
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


			}

		}
    }


	void FixedUpdate () {
        //update per frame, always keep the player down to a ground
		if (!JetCraft) {
			keepItDownDirectionPointToPlanet ();

			if (vCurPlanet != null) {
				AddGravity (vCurPlanet);
			}
		}


    }

    void AddGravity(GameObject vCurPlanet)
    {
        vGravityDirection = (vCurPlanet.transform.position - transform.position).normalized;

        //Add the gravity to the target object
        //myRigidBody.AddForce(gravity * gravityDirection);
    }

    void FindCurPlanet () {

		if (transform.tag == "GravityField") {
            vCurField = transform.gameObject;
			foreach (Transform child in transform) {
				if (child.gameObject.tag == "Planet")
					vCurPlanet = child.gameObject;
			}
		}
	}

	void keepItDownDirectionPointToPlanet() {
		Vector3 fwd = (Vector2)vLeftObj.transform.TransformDirection(-Vector3.up);

		//initialise variables
		vLeftDist = 99f;
		vRightDist = 99f;
		vCenterDist = 0f;

		Vector3 vlpos = vRightObj.transform.position;

		//left ray
		RaycastHit2D[] hitAlll = Physics2D.RaycastAll(vlpos, (Vector2)(fwd));

		//get the first planet in range and make it's own
	
			if (vCurPlanet == null) {
				foreach (RaycastHit2D hit in hitAlll) {
					if (hit.transform.tag == "Planet" && vCurPlanet == null && hit.transform.gameObject != transform.gameObject) {
						vCurPlanet = hit.transform.gameObject;
						transform.parent = vCurPlanet.transform;
					}
                        if(hit.transform.parent != null)
                          vCurField = hit.transform.parent.gameObject;
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
					Debug.DrawRay (vrpos, (Vector3)hit.point - vrpos, Color.blue);	
					vRightDist = Vector3.Distance (vrpos, (Vector3)hit.point);
				}

			//center to be able to have some kind of gravity
			RaycastHit2D[] hitAllc = Physics2D.RaycastAll (myRenderer.bounds.center, (Vector2)(fwd));
			bool vFoundGround = false;
			foreach (RaycastHit2D hit in hitAllc) {
				if (vCurPlanet == hit.transform.gameObject || ((hit.transform.tag == "Plateform" || hit.transform.tag == "Pushable") && CanWalkOnPlateform)) {
					Debug.DrawRay (myRenderer.bounds.center, (Vector3)hit.point - transform.position, Color.blue);	
					if (hit.transform.tag == "Plateform") {
						CanJump = true;
					}

					if (Vector3.Distance (myRenderer.bounds.center, (Vector3)hit.point) < vCenterDist || !vFoundGround) {
						vCenterDist = Vector3.Distance (myRenderer.bounds.center, (Vector3)hit.point);
						vFoundGround = true;
					}
				}
			}
			//check if the Left or Right Probe ARE inside the collider which mean they cannot know the distance. 
			//So we just rotate them to get a distance
			if (vLeftDist == 0f)
				vRightDist = 99f;
			if (vRightDist == 0f)
				vLeftDist = 99f;

			//make sure if we doesn't find anything below the character, make him rotate until we find the floor!
			if (vLeftDist == 99f && vRightDist == 99f) {
				vLeftDist = 0f;
			}

			//check if we rotate the character
			if (vLeftDist != vRightDist) {

				//check we rotate at which speed.
				float vDiff = vLeftDist - vRightDist;
				if (vDiff < 0)
					vDiff *= -1;

				//here we calculate how fast we must rotate the character
			if (vDiff < 0.01)
				vRotateSpeed = 1f;
			else if (vDiff < 0.2f)
					vRotateSpeed = 10f;				//small rotation to be smooth and be able to have the same exact position between Left and Right
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
				if (vElapsedHeight >= vJumpHeight || FoundNearbyPlanet) {
                    //check nearbyplanet if ready to change
                    if (IsReadyToChange)
                        CheckIfNearbyPlanet ();
					IsJumping = false;
					IsReadyToChange = false;
                    FoundNearbyPlanet = false;
				}
				//check if there is a nearby planet if JUMP and CAN change planets is activated

			} else if (!CanJump && Input.GetAxis ("Vertical") == 0) {
				CanJump = true;
			} else if (vCenterDist < (vDistanceGround - 0.1f) && !IsJumping) {
				//make him going UP
				transform.Translate (Vector3.up * vJumpSpeed * Time.deltaTime);
			}

	}

	public void CheckIfNearbyPlanet()
	{
		bool vFound = false;
		foreach (GameObject vPlanet in vPlanetList)
			if (vPlanet != vCurPlanet && !vFound && vPlanet != transform.gameObject) {
				//we found a planet. we transfert to the first one not in the range.
				vFound = true;
				IsReadyToChange = false; //cannot change planet without making another jump. Prevent the character to be stuck between 2 planets
                FoundNearbyPlanet = false;
                //change the planet
				vCurPlanet = vPlanet;
				//make sure the character scale isn't changed between planets
				transform.parent = vCurPlanet.transform;
				vCurField = vCurPlanet.transform.parent.gameObject;
			}
	}

    public void ChangeWeapon(int vNewWeaponIndex)
    {
        //change its weapon index then changing the weapon
        CurrentWeaponIndex = vNewWeaponIndex;

        //change the weapon for this one
        vWeaponRenderer.sprite = WeaponList[CurrentWeaponIndex].vSprite;

        //change its rotation by default to look forward.
        vWeaponObj.transform.localRotation = vStartingRotation;
    }

    //check if we rotate or not
    bool CanRotate(float angle)
    {
        bool vCanRotate = false;

        if (!WeaponList[CurrentWeaponIndex].UseGravity)
        {

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


	void OnTriggerEnter2D(Collider2D col)
	{
        if (col.CompareTag("Bazooka")){
            vCanUseWeapon = true;
            Destroy(col.gameObject);
            ChangeWeapon(1); //change the weapon from none to bazooka 
			fireButton.SetActive(true);
        }
		if (col.CompareTag ("TargetItem")) {
			Destroy (col.gameObject);
			findTarget = true;
		}

		if (IsPlayer && col.CompareTag ("JetCraftItem")) {
			transform.position += transform.up * jetPos;
			currentAmount = 0;
			Destroy (col.gameObject);
			JetCraft = true;
			transform.parent = null;
			vCurPlanet = null;
			vCurField = null;
		}

		if (col.tag == "Planet" && JetCraft) {
			vPlanetList.Add(col.gameObject);
			FoundNearbyPlanet = true;
			jetpack.SetActive (false);
			JetCraft = false;
			vJumpHeight = 1f;
			CheckIfNearbyPlanet ();
		}

        //triggers when colide with a gameobject
        if (col.tag == "GravityField")
        {
            //if the colider is GravityField
            //add this planet
            if (col.gameObject != vCurField){
                foreach (Transform child in col.gameObject.transform) {
                    if (child.gameObject.tag == "Planet" && !vPlanetList.Contains(child.gameObject)) {
                        vPlanetList.Add(child.gameObject);
                        FoundNearbyPlanet = true;
                    }
                }
            }
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		//triggers when exit a collision
		if (col.tag == "GravityField")
		{
			//remove this planet
			foreach (Transform child in col.gameObject.transform) {
				if (child.gameObject.tag == "Planet") {
					vPlanetList.Remove (child.gameObject);
				}
			}	
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		//triggers when two rigidbody object collide
		if (collision.collider.gameObject.layer == LayerMask.NameToLayer ("Enemy") && gameObject.tag == "Player" ){
			isDead = true;
		}
	}


    void UpdateCharacterAnimation()
    {
        //get the right list ot use
        List<Sprite> vCurAnimList;
        if (WalkingDirection == PG_Direction.Right)
            vCurAnimList = RightWalkAnimationList;
        else
            vCurAnimList = LeftWalkAnimationList;

        if (IsJumping )
            vCurAnimList = JumpAnimationList;

        if (isDead)
        {
            vCurAnimList = DieAnimationList;
           
        }
        if (isDamage)
        {
            vCurAnimList = DamageAnimationList;

        }


        if (vCurrentFrame + 1 >= vCurAnimList.Count)
            vCurrentFrame = 0;
        else
            vCurrentFrame++;

        //then change the sprite correctly
        if (vCurAnimList.Count > 0)
            myRenderer.sprite = vCurAnimList[vCurrentFrame];
    }


	void Die() {

		if (deathFlag) {
			deathSfx.Play ();
			deathFlag = false;
		}

        panel.SetActive(true);
       
        
	}

}



