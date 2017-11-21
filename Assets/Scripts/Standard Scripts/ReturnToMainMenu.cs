using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReturnToMainMenu : MonoBehaviour {
    private GameObject progression;

    private void Start()
    {
        progression = GameObject.Find("_ProgressionManager");
    }
    // Update is called once per frame
    void Update () {
        OVRInput.Update();
        Vector3 touchPosition = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);
        if(OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) && OVRInput.Get(OVRInput.Button.PrimaryTouchpad) && touchPosition.x > -.2 && touchPosition.x < .2 && touchPosition.y > -.2 && touchPosition.y < .2)
        {
            SceneManager.LoadScene("MainMenuScene");
            GameObject.Destroy(progression);
        }
	}
}
