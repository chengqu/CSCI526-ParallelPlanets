﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleObject : MonoBehaviour {

	public PG_Gravity vParent;
	public bool vCanMove = true;

	void Start()
	{
		//get the parent
		vParent = transform.parent.GetComponent<PG_Gravity> ();
	}

	public void ChangeCurPlanet(GameObject vNewPlanet)
	{
		vParent.ChangeCurPlanet (vNewPlanet);
	}
}
