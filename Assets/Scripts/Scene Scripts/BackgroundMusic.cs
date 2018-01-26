using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Created by: Takoda Ren
 * 
 * CLass that is to be attached to any player or playerController object that
 * moves around, with the purpose of playing looping background music.
 * The object this class attaches to must have 
 */

public class BackgroundMusic : MonoBehaviour {
    
    public AudioClip[] songs;
    private AudioSource audioSource;
    private int position;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        if(audioSource == null)
        {
            Debug.unityLogger.Log("Error", "AudioSource not attached to the object this class is attached to.");
        }
        //begin at a random position in the song array
        position = Random.Range(0, songs.Length);
    }
	
	// Update is called once per frame
	void Update () {
        //if there is not an audio source playing, change the position variable
        //accordingly and play the next song
        if (!audioSource.isPlaying)
        {
            if(position < songs.Length - 1)
            {
                position++;
            }
            else
            {
                position = 0;
            }
            audioSource.clip = songs[position];
            audioSource.Play();
        }
	}
}
