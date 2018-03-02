using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Created by: Takoda Ren
 * 
 * Attach to any object in a scene in which the player can return to the main menu from
 * Probably more intuitive if attached to the player object though.
 * Returns the player to the main menu if they press the center of the gearvr controller
 * touchpad if using the gearvr controller, or if they press the left triangle button while
 * using the Stratus XL
 */

public class ReturnToMainMenu : MonoBehaviour {
    private GameObject progression; //Holds the gameobject that contains the ReliefStats progression tracking script

    private void Start()
    {
        //Progression tracking script must be attached to gameobject named _ProgressionManager
        //for the progression object to be deleted correctly.
        progression = GameObject.Find("_ProgressionManager");
    }
    // Update is called once per frame
    void Update () {
        if (OVRInput.GetActiveController() == OVRInput.Controller.LTrackedRemote ||
           OVRInput.GetActiveController() == OVRInput.Controller.RTrackedRemote)
        {
            OVRInput.Update();
            Vector3 touchPosition = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);
            if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) && OVRInput.Get(OVRInput.Button.PrimaryTouchpad) && touchPosition.x > -.2 && touchPosition.x < .2 && touchPosition.y > -.2 && touchPosition.y < .2)
            {
                SceneManager.LoadScene("MainMenuScene");
                GameObject.Destroy(progression); //progression of gameobjects not kept if returning to main menu
            }
        }
        else
        {
            if(Input.GetButtonDown("Button 3")) //Y button on stratus xl
            {
                SceneManager.LoadScene("MainMenuScene");
                GameObject.Destroy(progression);
            }
        }
	}
}
