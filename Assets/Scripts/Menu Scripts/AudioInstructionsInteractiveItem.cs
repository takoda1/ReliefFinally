using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioInstructionsInteractiveItem : InteractiveItem {

    public string Controller;

    private Text notification;
    private AudioSource instructions;

	// Use this for initialization
	void Start () {
        notification = GetComponentInChildren<Text>();
        instructions = GetComponent<AudioSource>();
        HandleOut();
	}

    private void OnEnable()
    {
        OnOver += HandleOver;
        OnOut += HandleOut;
        OnClick += HandleClick;
    }

    private void OnDisable()
    {
        OnOver -= HandleOver;
        OnOut -= HandleOut;
        OnClick -= HandleClick;
    }

    private void HandleOver()
    {
        notification.text = "Press trigger on the GearVR Controller or A on the StratusXL to listen to the controls for " + Controller;
    }

    private void HandleOut()
    {
        notification.text = Controller + " Instructions";
    }

    private void HandleClick()
    {
        instructions.Play();
    }
}
