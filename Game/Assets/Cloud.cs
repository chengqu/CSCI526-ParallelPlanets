using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cloud : MonoBehaviour {

	public Slider healthBar;
	public float curHealth = 100;
	private float maxHealth = 100;
	public bool isDie = false;

	public void Damage(float damage) {
		curHealth -= damage;
		healthBar.value = curHealth/maxHealth;
		if (curHealth <= 0) {
			isDie = true;
		}
	}

	// Use this for initialization
	void Start () {
		healthBar.value = curHealth/maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		if (isDie) {
			Die ();
		}
	}

	void Die () {
		Destroy (gameObject);
	}

}
