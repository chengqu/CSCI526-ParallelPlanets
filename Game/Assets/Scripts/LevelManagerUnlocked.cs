using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManagerUnlocked : MonoBehaviour {
	public int Level;
	public Image Image;
	private string LevelString;
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
	void Start () {
		if (ButtonSettings.releasedLevelStatic >= Level) {
			Levelunlocked ();
		} else {
			Levellocked ();
		}
	}


	public void LevelSelect(string _level)
	{
		LevelString = _level;
		SceneManager.LoadScene (LevelString);
	}

	

	void Levellocked()
	{
		GetComponent<Button> ().interactable = false;
		Image.enabled = true;
	}
	void Levelunlocked ()
	{
        float health = PlayerPrefs.GetFloat(Level + "");
        if (health > 0 && health < 30)
        {
            star1.SetActive(true);
        }
        else if (health >= 30 && health < 70)
        {
            star1.SetActive(true);
            star2.SetActive(true);
        }
        else if(health >= 70 && health <= 100) {
            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(true);
        }

         GetComponent<Button> ().interactable = true;
		Image.enabled = false;
	}

}
