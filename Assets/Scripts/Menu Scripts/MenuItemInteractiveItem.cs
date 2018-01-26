using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Created by: Takoda Ren with help from online youtube tutorial
 * 
 * Menu InteractiveItem, attached to any menu item displaying
 * one of the scenes in ReliefFinally.
 */
public class MenuItemInteractiveItem : MonoBehaviour {

    //distance the menu quad is to move forward or backward
    private float moveDistance = .5f;

    //interactiveItem that is provided with customized actions
    [SerializeField] private InteractiveItem interactiveItem;

    //Scene that this MenuItem is to load
    public string Scene;

    //whenever the object this script is attached to is active, add all the custom
    //actions to the interactiveItem object.
    private void OnEnable()
    {
        interactiveItem.OnOver += HandleOver;
        interactiveItem.OnOut += HandleOut;
        interactiveItem.OnClick += HandleClick;
    }

    private void OnDisable()
    {
        interactiveItem.OnOver -= HandleOver;
        interactiveItem.OnOut -= HandleOut;
        interactiveItem.OnClick -= HandleClick;
    }

    //Whenever a menu object is looked over, it will move forward by a distance defined by moveDistance
    private void HandleOver()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - moveDistance);

    }

    //Menu object will move back the corresponding distance when gaze leaves the object
    private void HandleOut()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + moveDistance);
    }

    //If the player clicks, we want to load the scene they are looking at.
    private void HandleClick()
    {
        SceneManager.LoadScene(Scene, LoadSceneMode.Single);
    }
}
