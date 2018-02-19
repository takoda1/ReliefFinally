using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
 * Created by: Takoda Ren with help from online youtube tutorial
 * 
 * Menu InteractiveItem, attached to any menu item displaying
 * one of the scenes in ReliefFinally.
 * 
 * Attached to a menu item, and will cause the menu item
 * to move toward the main camera (but only in the +-X and Z directions)
 * when the main camera looks at the menu item.
 */
public class MenuItemInteractiveItem : InteractiveItem {

    private float moveDistance = .5f;
    private GameObject mainCamera;
    private Vector3 moveDirection;

    //Scene that this MenuItem is to load
    public string Scene;

    //find the camera and assign it
    private void Awake()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        //get the motion vector of the object toward the main camera
        Vector3 temp = Vector3.MoveTowards(this.transform.position, mainCamera.transform.position, 1);
        //normalize the above vector, and round the x and z components to integers. There should
        //only be one component that rounds to a 1/-1 and only one component should round to 0.
        moveDirection = new Vector3(Mathf.RoundToInt(temp.normalized.x), 0, Mathf.RoundToInt(temp.normalized.z));
    }

    //whenever the object this script is attached to is active, add all the custom
    //actions to the interactiveItem object.
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

    //Whenever a menu object is looked over, it will move forward by a distance defined by moveDistance
    private void HandleOver()
    {
        this.transform.position = new Vector3(this.transform.position.x - moveDistance * moveDirection.x, this.transform.position.y, this.transform.position.z - moveDistance * moveDirection.z);
    }

    //Menu object will move back the corresponding distance when gaze leaves the object
    private void HandleOut()
    {
        this.transform.position = new Vector3(this.transform.position.x + moveDistance * moveDirection.x, this.transform.position.y, this.transform.position.z + moveDistance * moveDirection.z);
    }

    //If the player clicks, we want to load the scene they are looking at.
    private void HandleClick()
    {
        Text alert = GetComponentInChildren<Text>();
        alert.text = "Loading";
        SceneManager.LoadScene(Scene, LoadSceneMode.Single);
    }
}
