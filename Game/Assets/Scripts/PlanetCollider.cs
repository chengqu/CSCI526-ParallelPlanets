using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlanetCollider : MonoBehaviour {

	public List<GameObject> vPlanetList;

	void Start ()
	{
		//initialise list 
		vPlanetList = new List<GameObject> ();
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		Debug.Log("1");
		if (col.tag == "PlanetScale")
		{
			//add this planet
			foreach (Transform child in col.gameObject.transform) {
				if (child.gameObject.tag == "Planet" && !vPlanetList.Contains (child.gameObject)) {
					vPlanetList.Add (child.gameObject);
					Debug.Log("collider");
				}
			}	
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		//make sure it's the player
		if (col.tag == "PlanetScale")
		{
			//remove this planet
			foreach (Transform child in col.gameObject.transform) {
				if (child.gameObject.tag == "Planet") {
					vPlanetList.Remove (child.gameObject);
					Debug.Log("colliderExit");
				}
			}	
		}
	}
}

