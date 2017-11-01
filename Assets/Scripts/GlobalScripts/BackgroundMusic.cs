using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour {
    
    public AudioClip[] songs;
    private AudioSource audioSource;
    private int position;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        position = Random.Range(0, songs.Length);
    }
	
	// Update is called once per frame
	void Update () {
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
