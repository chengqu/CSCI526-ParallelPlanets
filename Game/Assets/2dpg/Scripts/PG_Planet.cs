using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PG_Planet : MonoBehaviour
{
	//enum
	public enum PG_Direction {Right, Left};

	//hold all the walking direction
	public bool vRotate = false;
	public float vRotateSpeed = 20f;
	public float vIncreaseSize = 1f;
	public PG_Direction vDirection = PG_Direction.Right;
	public AudioClip vIncreaseSound;							//sound when planets is inreasing in size
	public AudioClip vDecreaseSound;							//sound when planets is decreasing in size

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
		vBlinkMat = (Material)Resources.Load("Components/DroidSansMono", typeof(Material));		//get this material which make the sprite white
		vAudioSource = GetComponent<AudioSource>();
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

	//increase the planets with a factor 2x,3x,4x...
	public void CallChangeScale()
	{
		//toggle stats. 
		vChangedScale = !vChangedScale;

		//create a temp list for every gameobject which are childs currently, but we will temporary remove them to increase planets scale and not them.
		//also, after we will add them back as a childs
		//reset temp list
		vTempList = new List<GameObject>();

		//ONLY get PG_Gravity and PG_Character in the temp list
		foreach (Transform child in transform)
		{
			if (child.GetComponent<PG_Gravity> () != null || child.GetComponent<PG_Character>() != null) {
				vTempList.Add (child.gameObject);
			}
		}

		//remove parents
		foreach (GameObject vCurObj in vTempList)
			vCurObj.transform.parent = null;

		//check if planets is increasing or decreasing so we play the sound 
		if  (Vector3.Distance(vOriginalSize, vTargetSize) > 0 && vChangedScale)
			PlaySound (vIncreaseSound);
		else
			PlaySound (vDecreaseSound);

		//change size
		StartCoroutine (ChangeScale ());
	}

	IEnumerator ChangeScale()
	{
		float vElapseTime = 0f;
		float vTimeToMove = 1f;

		//make the planets white
		vRenderer.material = vBlinkMat;

		while (vElapseTime < vTimeToMove) {

			//check if we increase/descrease size
			if (vChangedScale)
				transform.localScale = Vector3.Lerp (vOriginalSize, vTargetSize, (float)(vElapseTime / vTimeToMove));
			else
				transform.localScale = Vector3.Lerp (vTargetSize, vOriginalSize, (float)(vElapseTime / vTimeToMove));

			//wait some time
			yield return new WaitForSeconds (0.01f);

			//increase time
			vElapseTime += Time.deltaTime;
		}

		//get back alls childs correctly
		foreach (GameObject vCurObj in vTempList)
			vCurObj.transform.parent = transform;

		//make the planets normal color
		vRenderer.material = vSpriteMat;
	}
}
