using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DropDownStatus : MonoBehaviour {
    public Button btn;
    public Dropdown dropdown;
    public Camera cam;
	// Use this for initialization
	void Start () {
        btn.onClick.AddListener(delegate { test(cam); });
    }

    void test(Camera c) {
        
    }
	
	// Update is called once per frame
	void Update () {
        
    }
}
