using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMainMenu : MonoBehaviour {
    private GameObject progression;

    private void Start()
    {
        progression = GameObject.Find("_ProgressionManager");
    }
    // Update is called once per frame
    void Update () {
        OVRInput.Update();
        if(OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) && OVRInput.Get(OVRInput.Button.PrimaryTouchpad))
        {
            SceneManager.LoadScene("MainMenuScene");
            GameObject.Destroy(progression);
        }
	}
}
