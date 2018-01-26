using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Used by: Takoda Ren
 * Modified from original found at: http://wiki.unity3d.com/index.php?title=AutoType
 * Description:
 * Class that is attached to any Unity Text element.
 * After calling the setText method, the text in the 
 * Text element will begin typing with specified
 * pause time defined in the pauseTime variable.
 * 
 * Used in the TutorialScene to make tutorial text
 * run across the screen.
 *
 */
public class TypeText : MonoBehaviour {

    public float pauseTime = .1f;

    string text;
    Text textElement;

    void Start()
    {
        textElement = GetComponent<Text>();
    }
	public bool setText(string text)
    {
        StopAllCoroutines();
        this.text = text;
        textElement.text = "";
        StartCoroutine(Type());
        return true;
    }

    IEnumerator Type()
    {
        foreach(char letter in text.ToCharArray())
        {
            textElement.text += letter;
            yield return new WaitForSeconds(pauseTime);
        }
    }
}
