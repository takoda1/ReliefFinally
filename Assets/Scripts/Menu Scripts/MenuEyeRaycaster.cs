using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Created by: Takoda Ren
 * Class that was created with help from a youtube tutorial,
 * forgot the name of the tutorial.
 * 
 * Used in the main menu scene of ReliefFinally to determine if
 * the player is looking at one of the menu options (a MenuItemInteractiveItem) 
 * and to determine if the player has clicked while hovering (with their gaze)
 * over one of the menu options. And calls the appropriate methods on the
 * InteractiveItem currentInteractible and lastInteractible.
 */

public class MenuEyeRaycaster : MonoBehaviour {
    //raycaster script that must be attached to camera
    private InteractiveItem currentInteractible { get; set; } //current InteractiveItem
    private InteractiveItem previousInteractible { get; set; } //Previous InteractiveItem
	
	// Update is called once per frame
	void Update () {
        EyeRaycast();
        OVRInput.Update();
        HandleInput();
	}

    private void EyeRaycast()
    {
        //Cast a ray in the forward direction
        Ray ray = new Ray(this.transform.position, this.transform.forward);
        RaycastHit hit;

        //See if ray hits an object
        if(Physics.Raycast(ray, out hit))
        { 
            //if it has, set it as the current InteractiveItem
            currentInteractible = hit.collider.GetComponent<InteractiveItem>();

            //If the interactive object called is not the same as the last item, call over
            //representing when the player first looks over an object
            if(currentInteractible != null && currentInteractible != previousInteractible)
                currentInteractible.Over();

            //Deactivate last interactive item if it isn't the same as the current item or if 
            if (currentInteractible != previousInteractible)
                DeactivatePreviousInteractible();

            previousInteractible = currentInteractible;
            


        }
        else
        {
            //Nothing is hit, so deactive last interactive item and current interactive item
            DeactivatePreviousInteractible();
            currentInteractible = null;
        }
    }


    private void DeactivatePreviousInteractible()
    {
        if (previousInteractible == null)
            return;

        previousInteractible.Out();
        previousInteractible = null;
    }

    private void HandleInput()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        //if gearVR remote
        if(OVRInput.GetActiveController() == OVRInput.Controller.LTrackedRemote ||
           OVRInput.GetActiveController() == OVRInput.Controller.RTrackedRemote)
        {
            //If the trigger is pressed when hovering over an interactive object, call Click
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                if (currentInteractible != null)
                    currentInteractible.Click();
            }
        }
        //assume stratus xl remote otherwise
        else
        {
            if(Input.GetButtonDown("Button 0")) //maps to button A on controller
            {
                if (currentInteractible != null)
                    currentInteractible.Click();
            }
        }
#endif
    }
}
