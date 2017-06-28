using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RPB : MonoBehaviour {

	public Transform Planet_gravityfield_big;
	public Transform LoadingBar;
<<<<<<< Updated upstream
	public Transform TextIndicator;
	public float currentAmount;
	public float speed;
	public bool startTimeOut = false;
=======
	[SerializeField] private float currentAmount;
	[SerializeField] private float speed;

>>>>>>> Stashed changes
	// Update is called once per frame
	void Update () {
		if (startTimeOut) {
			if (currentAmount < 100) {
				currentAmount += speed * Time.deltaTime;
				TextIndicator.GetComponent<Text> ().text = ((int)currentAmount).ToString () + "%";
			} else {
				TextIndicator.GetComponent<Text> ().text = "DONE!";
			}
			LoadingBar.GetComponent<Image> ().fillAmount = currentAmount / 100;

<<<<<<< Updated upstream
=======
		if (currentAmount < 100) {
			currentAmount += speed * Time.deltaTime;
			Planet_gravityfield_big.gameObject.SetActive (true);
			LoadingBar.gameObject.SetActive (true);
		} else {
			Planet_gravityfield_big.gameObject.SetActive (false);
>>>>>>> Stashed changes
		}
	}
}
