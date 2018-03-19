using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Created by Takoda Ren, 2/27/2018.
 * 
 * Because I only need to have swim functionality in a world
 * that is entirely water, this script doesn't check if you
 * are in water or not; it is added to a scene that is guaranteed
 * to be all water.
 */

public class Swim : MonoBehaviour {

    //speed at which to move up and down
    public float speed;

    //character controller that is to be given movement directions
    private CharacterController parent;

	// Use this for initialization
	void Start () {
        parent = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
#if UNITY_ANDROID && !UNITY_EDITOR
        OVRInput.Update();

        //if using the GearVR Controller
		if (OVRInput.GetActiveController() == OVRInput.Controller.LTrackedRemote ||
            OVRInput.GetActiveController() == OVRInput.Controller.RTrackedRemote)
        {
            Vector2 touchPosition = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);
            //if the player is pressing up or down on the touchpad, they will move up or down.
            if (OVRInput.Get(OVRInput.Button.PrimaryTouchpad))
            {
                if(touchPosition.y >= .75)
                {
                    moveUp();
                }
                if(touchPosition.y <= -.75)
                {
                    moveDown();
                }
            }
        }
        //StratusXL
        else
        {
            if(Input.GetButton("Button 0")) //maps to A
            {
                moveUp();
            }
            if(Input.GetButton("Button 1")) //maps to B
            {
                moveDown();
            }
        }
#endif
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.R))
        {
            moveUp();
        }
        if (Input.GetKey(KeyCode.F))
        {
            moveDown();
        }
#endif
    }

    private void moveUp()
    {
        Vector3 move = new Vector3(0, 1f, 0);
        move *= speed;
        parent.Move(move * Time.deltaTime);
    }
    
    private void moveDown()
    {
        Vector3 move = new Vector3(0, -1f, 0);
        move *= speed;
        parent.Move(move * Time.deltaTime);

        //Vector3 destination = new Vector3(parent.transform.position.x, parent.transform.position.y - 1f, parent.transform.position.z);
        //parent.transform.position = Vector3.MoveTowards(parent.transform.position, destination, speed * Time.deltaTime);
    }
}
