using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


[System.Serializable]
public class PG_Sample
{
	public Vector3 vSamplePos;
	public GameObject vSampleObj;
	public bool UseWeapon = true;
}

public class demo : MonoBehaviour {

	//coded position of each sample
	public List<PG_Sample> vSampleList;		//contains all demos samples 
	public Toggle vToggle;
	public int vCurSample = -1;			//check wich sample we are showing 
	public GameObject vLeftBtn;
	public GameObject vRightBtn;

	private bool vChoice = true;
	private PG_Character vCurPlayer;

	// Use this for initialization
	void Start () {
		//load Sample2 first
		MoveCameraTo (vCurSample);
		RotateAllPlanets ();
	}

	void DisableAllSamples()
	{
		//disable all scene by default
		foreach (PG_Sample vSample in vSampleList)
			vSample.vSampleObj.SetActive (false);
	}

	public void ChangeWeapon(int vIndexWeapon)
	{
		//try to find the player and change its weapon
		if (vCurPlayer.WeaponList[vIndexWeapon] != null)
			vCurPlayer.ChangeWeapon(vIndexWeapon);
	}

	public void MoveCameraTo(int vPos)
	{
		//disable all sample
		DisableAllSamples ();

		//increase/decrease samples
		if (vChoice)
			vCurSample = vPos;
		else
			vCurSample += vPos;

		//disable it after
		vChoice = false;
		
		if (vCurSample < 0)
			vCurSample = vSampleList.Count - 1;
		else if (vCurSample >= vSampleList.Count)
			vCurSample = 0;

		//move the camera to the right sample
		Camera.main.transform.position = vSampleList [vCurSample].vSamplePos;

		//activate the right sample
		vSampleList [vCurSample].vSampleObj.SetActive (true);

		//check if we checked Rotate All Planets on Canvas
		RotateAllPlanets ();

		//get the right player in it
		GameObject vplayer = GameObject.Find("Player");
		if (vplayer != null)
			vCurPlayer = vplayer.GetComponent<PG_Character> ();

		//use the right button
		if (vLeftBtn != null) vLeftBtn.SetActive (vSampleList [vCurSample].UseWeapon);
		if (vRightBtn != null) vRightBtn.SetActive (vSampleList [vCurSample].UseWeapon);

		vCurPlayer.vCanUseWeapon = vSampleList [vCurSample].UseWeapon;
	}

	public void RotateAllPlanets()
	{
		//get all planets in scene and make them rotate!
		foreach (PG_Planet vPlanet in Object.FindObjectsOfType<PG_Planet>())
			vPlanet.vRotate = vToggle.isOn;

		foreach (PG_AlwaysRotate vScript in Object.FindObjectsOfType<PG_AlwaysRotate>())
			vScript.vRotate = vToggle.isOn;
	}
}

