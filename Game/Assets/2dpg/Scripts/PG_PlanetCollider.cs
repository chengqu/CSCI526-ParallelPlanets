using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PG_PlanetCollider : MonoBehaviour {

	public List<GameObject> vPlanetList;

	void Start ()
	{
		//initialise list 
		vPlanetList = new List<GameObject> ();
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (!vPlanetList.Contains(col.gameObject))
		//make sure it's the player
		if (col.tag == "Planet")
		{
			//add this planet
			vPlanetList.Add (col.gameObject);
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		//make sure it's the player
		if (col.tag == "Planet")
		{
			//remove this planet
			vPlanetList.Remove (col.gameObject);
		}
	}
}
