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
		if (!vPlanetList.Contains (col.gameObject)) {
			//make sure it's the player
			if (col.tag == "PlanetScale") {
				//add this planet
				foreach (Transform child in col.gameObject.transform) {
					Debug.Log ("2");
					if (child.gameObject.tag == "Planet") {
						Debug.Log ("1");
						vPlanetList.Add (child.gameObject);
					}
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
				Debug.Log ("2exit");
				if (child.gameObject.tag == "Planet") {
					Debug.Log ("1exit");
					vPlanetList.Remove (child.gameObject);
				}
			}
		}
	}
}

