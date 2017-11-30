using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {

    private TypeText typeText;

    private Text treasureText;

    private string[] introduction = {"Welcome to ReliefFinally! To advance through tutorial text, press the trigger button.",
        "First, look around by moving your head, or look around by PRESSING left or right on the controller pad.",
        "Remember, if you ever want to return to the main menu, you can always press the trigger and press the middle of the touch pad at the same time.",
        "You can touch forward or backward on the touchpad to move forward or backward in the direction you are facing.",
        "You can also sprint while moving by holding the trigger button.",
        "Now, move toward the ---- marker up ahead for an explanation of maps."};
    private string[] mapExplanation = {"This is an explanation for maps in ReliefFinally. ReliefFinally has a few maps you can explore.",
        "The default map progression is Grassy Plains -> Snowy Mountain -> Barnacle Waters",
        "What that means is each of those maps has treasures specific to the next map that you can collect to progress to the next map in the sequence",
        "However, you can go straight to a specific map from the main menu if you want.",
        "Each map has portals leading to all other worlds, so if you have collected all the treasures from all worlds you can access any world.",
        "Now, move to the ---- marker ahead to the left for an explanation of treasures and portals."};
    private string[] treasureExplanation = {"Each map has approximately 6 treasures that you can collect that will allow you to advance to the next world.",
        "Look for the marker that labels a treasure and walk over and pick it up.",
        "A portal will have textures that represent the world it allows access to.",
        "To enter the Grassy Plains world, look for the marker that labels a portal and walk through the portal doors."
    };

	// Use this for initialization
	void Start () {
        typeText = GetComponentInChildren<TypeText>();
        GameObject temp = GameObject.FindGameObjectWithTag("Treasure Text");
        treasureText = temp.GetComponentInChildren<Text>();
    }

    void Update()
    {
        OVRInput.Update();
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.name)
        {
            case "Introduction":
                StopAllCoroutines();
                StartCoroutine(StartTyping(introduction));
                break;
            case "MapExplanation":
                StopAllCoroutines();
                StartCoroutine(StartTyping(mapExplanation));
                break;
            case "TreasureExplanation":
                StopAllCoroutines();
                StartCoroutine(StartTyping(treasureExplanation));
                break;

        }
    }
    
    IEnumerator StartTyping(string[] text)
    {
        int position = 0;
        do
        {
            typeText.setText(text[position]);
            yield return new WaitForSeconds(1);
            yield return StartCoroutine("IsButtonPressed");
            position++;
        } while (position < text.Length);

    }

    IEnumerator IsButtonPressed()
    {
        do
        {
            yield return null;
        } while (!(OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) && !OVRInput.Get(OVRInput.Touch.PrimaryTouchpad)));
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "snowPiece")
        {
            hit.gameObject.SetActive(false);
            treasureText.text = "Great Job! You collected a treasure.";
        }
    }
}
