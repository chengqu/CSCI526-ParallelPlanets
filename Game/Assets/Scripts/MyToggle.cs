using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyToggle : MonoBehaviour {
    public GameObject isOngameObject;
    public GameObject isOffgameObject;
    private Toggle toggle; 
	// Use this for initialization
	void Start () {
        toggle = GetComponent<Toggle>();
	}
	
	// Update is called once per frame
	void Update () {
        onValueChange(toggle.isOn);
    }

    public void onValueChange(bool isOn) {
        isOngameObject.SetActive(isOn);
        isOffgameObject.SetActive(!isOn);
    }
}
