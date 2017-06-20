using UnityEngine;
using System.Collections;


public class Music : MonoBehaviour {
	public static Music instance = null;
	// Use this for initialization
	void Start () {
		if(instance!=null){
			Destroy(gameObject);
			return;
		}
		instance = this;
		DontDestroyOnLoad (gameObject);
		//if(GlobalOptions.isSound()){
		GetComponent<AudioSource>().Play();
		//}
	}
	
	public void Pause(){
		GetComponent<AudioSource>().Pause();
	}
	
	public void Play(){
		GetComponent<AudioSource>().Play();
	}
}
