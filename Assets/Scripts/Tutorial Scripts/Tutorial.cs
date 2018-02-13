using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*
 * Created by: Takoda Ren
 * Description:
 * 
 * Attach to a PlayerController GameObject with typeText component on one of
 * the object's text-displaying children. 
 * Whenever a trigger (a tutorial zone in concrete terms) is entered, 
 * the tutorial zone's corresponding text is passed to the typeText
 * object to have it start typing on whatever Gui is being used.
 * 
 * If there are still remaining lines of text to be typed,
 * this class waits for the player to press the GearVR trigger
 * button before advancing to the next line.
 */ 
public class Tutorial : MonoBehaviour {

    private TypeText typeText;

    private Text treasureText;
    private bool usingGearVrRemote;

    // Use this for initialization
    void Start () {
        //initialize typeText variable
        typeText = GetComponentInChildren<TypeText>();

        /*
         * Specific to ReliefFinally tutorial, find the text-displaying
         * GameObject above the tutorial treasure chest, and initialize
         * the treasureText variable with the Text object from the
         * mentioned GameObject.
         */
        GameObject temp = GameObject.FindGameObjectWithTag("Treasure Text");
        treasureText = temp.GetComponentInChildren<Text>();

        if (OVRInput.GetActiveController() == OVRInput.Controller.LTrackedRemote ||
           OVRInput.GetActiveController() == OVRInput.Controller.RTrackedRemote)
            usingGearVrRemote = true;
        else
            usingGearVrRemote = false;
    }

    //OVRInput needs to be updated to update whether the GearVR trigger is being pressed or not
    void Update()
    {
        OVRInput.Update();
    }

    /*
     * @param zone: The collider of the gameobject the player has collided with
     * 
     * If there is zoneText attached to the entered gameobject with trigger, it implies that the 
     * gameobject entered is a tutorial zone, so all other Coroutines (such as StartTyping(),
     * IsButtonPressed(), as well as Coroutines in the TypeText class) are ceased
     * and the Coroutine StartTyping is started with the new zoneText.
     */
    private void OnTriggerEnter(Collider zone)
    {
        string[] temp = zone.gameObject.GetComponent<ZoneText>().zoneText;
        if(temp != null)
        {
            StopAllCoroutines();
            StartCoroutine(StartTyping(temp));
        }
    }
    
    /*
     * @param text: an array of strings to be typed
     * 
     * Sets every line of text in the parameter to the
     * typeText object and in between each line waits
     * for IsButtonPressed coroutine to return.
     */
    IEnumerator StartTyping(string[] text)
    {
        int position = 0;
        do
        {
            //setting typeText text automatically begins the typing coroutine in typeText
            typeText.setText(text[position]);
            //Wait for one second before checking whether the button is pressed because
            //if there is no delay, a depressed button will cause this loop to completely
            //run through with the only visual indication being immediately reaching the
            //last string in the text array
            yield return new WaitForSeconds(1);
            yield return StartCoroutine("IsButtonPressed");
            position++;
        } while (position < text.Length);

    }

    /*
     * Coroutine that only stops looping if:
     * when using GearVR remote:
     * GearVR trigger
     * is depressed and the player isn't touching the GearVR
     * touchpad (due to the dual use of the trigger as the sprint button)
     * when using stratus xl remote:
     * button A (maps to button 0) is depressed
     */
    IEnumerator IsButtonPressed()
    {
        if (usingGearVrRemote)
        {
            do
            {
                yield return null;
            } while (!(OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) && !OVRInput.Get(OVRInput.Touch.PrimaryTouchpad)));
        }
        else
        //stratus xl controller
        {
            do
            {
                yield return null;
            } while (!Input.GetButtonDown("Button 0"));
        }
    }

    /*
     * Specifically for when the player collides with the tutorial treasure; the treasure
     * is deactivated, resulting in the player "collecting" it, and confirmation text
     * on the treasureText object is set.
     */
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "snowPiece")
        {
            hit.gameObject.SetActive(false);
            treasureText.text = "Great Job! You collected a treasure.";
        }
    }
}
