using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

/*
 * Modified from the Unity Vr sample scene code
 * Class that is used as a variable 
 * for any interactive item (activated by looking
 * through raycasts, or clicks) where the encapsulating
 * class provides the customized actions to this class
 * and this class's methods can then be called accordingly by
 * any interacting class such as the MenuEyeRaycaster class.
 */
public class InteractiveItem : MonoBehaviour {

    public event Action OnOver;
    public event Action OnOut;
    public event Action OnClick;
    public event Action OnDown;

    protected bool isOver { get; set; }

    public void Over()
    {
        isOver = true;
        if (OnOver != null)
            OnOver();
    }

    public void Click()
    {
        if(OnClick != null)
            OnClick();
    }

    public void Out()
    {
        if(OnOut != null)
        {
            OnOut();
        }
    }

    public void Down()
    {
        if (OnDown != null)
            OnDown();
    }
}
