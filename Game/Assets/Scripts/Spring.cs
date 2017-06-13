using UnityEngine;
using System.Collections;

public class Spring : MonoBehaviour
{
    public AudioClip vSpringSound;
    public bool TempDisable = false;                //check if it's temporary disabled 

    private AudioSource vAudioSource;
    private float vOrignalScale = 1f;
    private float vElapsedHeight = 0f;
    private float vJumpHeight = 2f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlaySound(AudioClip vClip)
    {
        if (vAudioSource != null)
        {
            vAudioSource.clip = vClip;
            vAudioSource.Play();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //check if it's the other faction
        if (col.gameObject.GetComponent<PlayerController>())
        {
            GameObject vObj = col.gameObject;
            PlayerController vChar = vObj.GetComponent<PlayerController>();
            vChar.CanJump = true;
            vChar.CanJumpOnOtherPlanets = true;
            vObj.transform.Translate(Vector3.up * vChar.vJumpSpeed * Time.deltaTime);
            //increase jump time
            vElapsedHeight += Time.deltaTime * vChar.vJumpSpeed;

            //check if we jumped enought
            if (vElapsedHeight >= vJumpHeight)
            {
                col.gameObject.GetComponent<PlayerController>().CheckIfNearbyPlanet();
                vChar.CanJump = false;
                vChar.CanJumpOnOtherPlanets = false;
                vChar.isJumping = true;
                vElapsedHeight = 0f;
                vChar.IsReadyToChange = true;
            }
            
        }
    }

    }
