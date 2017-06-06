using UnityEngine;
using System.Collections;

public class PG_Gravity : MonoBehaviour {

	public float vDistanceGround = 0.3f;						//distance of the ground. Clouds are higher than character but still rotate!
	public bool CanWalkOnPlateform = false; 					//by default, these objects doesn't go on plateform

	private SpriteRenderer vRenderer;
	private GameObject vRightObj;
	private GameObject vLeftObj;
	public GameObject vCurPlanet;
	private float vRotateSpeed = 10f;	

	// Use this for initialization
	void Start () {
		vRenderer = GetComponent<SpriteRenderer> ();

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

		//get current planet
		vCurPlanet = transform.parent.gameObject;
	}
		
	public void ChangeCurPlanet(GameObject vNewPlanet)
	{
		vCurPlanet = vNewPlanet;
		transform.parent = vCurPlanet.transform;
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
			if (vCurPlanet == hit.transform.gameObject || (hit.transform.tag == "Plateform" && CanWalkOnPlateform)) {
				Debug.DrawRay (vRenderer.bounds.center, (Vector3)hit.point-transform.position, Color.yellow);	
				if (Vector3.Distance (vRenderer.bounds.center, (Vector3)hit.point) < vCenterDist || !vFoundGround) {
					vCenterDist = Vector3.Distance (vRenderer.bounds.center, (Vector3)hit.point);
					vFoundGround = true;
				}
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
				vRotateSpeed = 20f;				//small rotation to be smooth and be able to have the same exact position between Left and Right
			else if (vDiff >= 0.2f && vDiff < 0.4f)
				vRotateSpeed = 400f;			//need to turn a little bit faster
			else
				vRotateSpeed = 400f;			//we must turn VERY quick because it's a big corner

			//rotate the character in the direction it's going
			if (vLeftDist < vRightDist)
				RotateObj ("Left");
			else
				RotateObj ("Right");
		}

		//always keep the same distance on this new field
		if (vCenterDist > vDistanceGround) {
			//walk
			transform.Translate (-Vector3.up * 2f * Time.deltaTime);
		}
		else if (vCenterDist < (vDistanceGround-0.1f)){
			//make him going UP
			transform.Translate (Vector3.up * 2f * Time.deltaTime);
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
}
