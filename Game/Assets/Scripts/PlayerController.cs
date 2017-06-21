using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public bool findTarget = false;

    //public variables
	//mobile controller variables
	public Texture btnLeft_tex;
	public Texture btnRight_tex;
	public Texture btnJump_tex;
	public GUI btnLeft;
	public GUI btnRight;
	public GUI btnJump;

	public float moveSpeed = 5f;
	public float jumpForce = 50f;

	//in game variables
	public enum Walk_Direction {Right, Left};   //directions the player can walk
    public Walk_Direction WalkingDirection = Walk_Direction.Right;  //set initial walking direction to right
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

    public List<Sprite> LeftWalkAnimationList;
    public List<Sprite> RightWalkAnimationList;

    //private variables
    private bool moveLeft, moveRight, doJump = false;
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


	// Use this for initialization
	void Start () {
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
    }

	//change current planet

	public void ChangeCurPlanet(GameObject vNewPlanet)
	{
		vCurPlanet = vNewPlanet;
        vCurField = vNewPlanet.transform.parent.gameObject;
        //set the parent of the player the planet he's currently on
		//transform.parent = vCurPlanet.transform;
	}

	public void OnGUI(){
		if (IsPlayer) {
			if (GUI.RepeatButton (new Rect (0,Screen.height - 50,80,50), btnLeft_tex)) {
				moveRight = false;
				moveLeft = true;
			} else {
				moveLeft = false;
			}
			if (GUI.RepeatButton (new Rect (100,Screen.height - 50,80,50), btnRight_tex)) {
				moveLeft = false;
				moveRight = true;
			} else {
				moveRight = false;
			}

			if (GUI.Button (new Rect (Screen.width - 100,Screen.height - 50,80,50), btnJump_tex)) {
				doJump = true;
			}
		}
	}
	// Update is called once per frame
	void Update () {

		//check if this character can move freely or it's disabled
		if (vCanMove) {
			pos = Vector3.zero;

			//check if going RIGHT
			if ((IsPlayer && moveRight) || (IsPlayer && Input.GetAxis ("Horizontal") > 0 && !Input.GetButtonUp ("Horizontal")) || (IsAutoWalking && WalkingDirection == Walk_Direction.Right)) {
				pos += Vector3.right * vWalkSpeed * Time.deltaTime;
				WalkingDirection = Walk_Direction.Right;
			}

			//check if going LEFT
			if ((IsPlayer && moveLeft) || (IsPlayer && Input.GetAxis ("Horizontal") < 0 && !Input.GetButtonUp ("Horizontal")) || (IsAutoWalking && WalkingDirection == Walk_Direction.Left)) {
				pos += Vector3.left * vWalkSpeed * Time.deltaTime;
				WalkingDirection = Walk_Direction.Left;
			}

			//check if JUMP
			if ((IsPlayer && doJump) || IsPlayer && Input.GetAxis ("Vertical") > 0 && !IsJumping && CanJump) {
				IsJumping = true;
				CanJump = false;
				vElapsedHeight = 0f;
				IsReadyToChange = true;
				doJump = false;
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


	void FixedUpdate () {
        //update per frame, always keep the player down to a ground
			keepItDownDirectionPointToPlanet ();
        if (vCurPlanet != null)
        {
            AddGravity(vCurPlanet);
        }
    }

    //TODO: need to fix the variables
    void AddGravity(GameObject vCurPlanet)
    {
        Vector3 gravityDirection = (vCurPlanet.transform.position - transform.position).normalized;

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
			}
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
//
//	void UpdateCharacterAnimation()
//	{
//		//get the right list ot use
//		List<Sprite> vCurAnimList; 
//		if (WalkingDirection == PG_Direction.Right)
//			vCurAnimList = RightWalkAnimationList;
//		else
//			vCurAnimList = LeftWalkAnimationList;
//
//		if (vCurrentFrame + 1 >= vCurAnimList.Count)
//			vCurrentFrame = 0;
//		else
//			vCurrentFrame++;
//
//		//then change the sprite correctly
//		if (vCurAnimList.Count > 0)
//			vRenderer.sprite = vCurAnimList[vCurrentFrame];
//	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag ("TargetItem")) {
			Destroy (col.gameObject);
			findTarget = true;
		}
        //triggers when colide with a gameobject
        if (col.tag == "GravityField")
        {
            //if the colider is GravityField
            //add this planet
            if (col.gameObject != vCurField){
                vCurField = col.gameObject;
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
			Application.LoadLevel (Application.loadedLevel);
		}
	}

    void UpdateCharacterAnimation()
    {
        //get the right list ot use
        List<Sprite> vCurAnimList;
        if (WalkingDirection == Walk_Direction.Right)
            vCurAnimList = RightWalkAnimationList;
        else
            vCurAnimList = LeftWalkAnimationList;

        if (vCurrentFrame + 1 >= vCurAnimList.Count)
            vCurrentFrame = 0;
        else
            vCurrentFrame++;

        //then change the sprite correctly
        if (vCurAnimList.Count > 0)
            myRenderer.sprite = vCurAnimList[vCurrentFrame];
    }

}



