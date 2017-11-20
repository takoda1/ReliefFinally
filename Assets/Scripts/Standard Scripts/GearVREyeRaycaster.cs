using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearVREyeRaycaster : MonoBehaviour {
    //raycaster script that must be attached to camera
    private InteractiveItem currentInteractible { get; set; } //current InteractiveItem
    private InteractiveItem lastInteractible { get; set; } //Previous InteractiveItem
	
	// Update is called once per frame
	void Update () {
        EyeRaycast();
        OVRInput.Update();
        HandleInput();
	}

    private void EyeRaycast()
    {
        
        Ray ray = new Ray(this.transform.position, this.transform.forward);
        RaycastHit hit;

        //See if ray hits an interactive object
        if(Physics.Raycast(ray, out hit))
        { 
            currentInteractible = hit.collider.GetComponent<InteractiveItem>();

            //If the interactive object called is not the same as the last item, call over
            if(currentInteractible != null && currentInteractible != lastInteractible)
                currentInteractible.Over();

            //Deactivate last interactive item
            if (currentInteractible != lastInteractible)
                DeactivateLastInteractible();

            lastInteractible = currentInteractible;
            


        }
        else
        {
            //Nothing is hit, so deactive last interactive item and current interactive item
            DeactivateLastInteractible();
            currentInteractible = null;
        }
    }


    private void DeactivateLastInteractible()
    {
        if (lastInteractible == null)
            return;

        lastInteractible.Out();
        lastInteractible = null;
    }

    private void HandleInput()
    {
        //If there is a remote
        if(OVRInput.GetActiveController() == OVRInput.Controller.LTrackedRemote ||
           OVRInput.GetActiveController() == OVRInput.Controller.RTrackedRemote)
        {
            //If the trigger is pressed when hovering over an interactive object, call Click
            if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
            {
                if (currentInteractible != null)
                    currentInteractible.Click();
            }
        }
    }
}
