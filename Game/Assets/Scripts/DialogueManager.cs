using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour {

	public GameObject dBox;
	public Text dText;

	public bool dialogueAcctive;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (dialogueAcctive && Input.GetKeyDown (KeyCode.Space)) {
			dBox.SetActive (false);
			dialogueAcctive = false;
		}
	}

	public void ShowBox(string dialogue) {
		dBox.SetActive (true);
		dialogueAcctive = true;
		dText.text = dialogue;
	}
}
