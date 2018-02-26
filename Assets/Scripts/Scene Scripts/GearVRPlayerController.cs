using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearVRPlayerController : OVRPlayerController {


    //Game specific variables
    public AudioSource footstepAudioSource;
    private bool isMoving;

    // Use this for initialization
    override public void Start () {
        base.Start();
        useProfileData = false;
	}
	
	// Update is called once per frame
	override public void Update () {
        base.Update();
        if(OVRInput.Get(OVRInput.Touch.PrimaryTouchpad) && !OVRInput.Get(OVRInput.Button.PrimaryTouchpad) ||
           OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y != 0
           ){
            isMoving = true;
        }
#if UNITY_EDITOR
        else if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S))
        {
            isMoving = true;
        }
#endif
        else
        {
            isMoving = false;
        }
        UpdateFootstepAudio();
    }


    private void UpdateFootstepAudio()
    {
        if (!Controller.isGrounded)
        {
            return;
        }
        if (isMoving && footstepAudioSource.isPlaying)
        {
            return;
        }
        else if (isMoving && !footstepAudioSource.isPlaying)
        {
            footstepAudioSource.Play();
        }
        else if (!isMoving && footstepAudioSource.isPlaying)
        {
            footstepAudioSource.Stop();
        }
        // pick & play a random footstep sound from the array,
        // excluding sound at index 0
        //int n = Random.Range(1, footstepSounds.Length);
        //audioSource.clip = footstepSound;
        //audioSource.Play();
        //audioSource.PlayOneShot(audioSource.clip);
        // move picked sound to index 0 so it's not picked next time
        //footstepSounds[n] = footstepSounds[0];
        //footstepSounds[0] = audioSource.clip;
    }
}
