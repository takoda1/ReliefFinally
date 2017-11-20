using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

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
