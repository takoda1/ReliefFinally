using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeText : MonoBehaviour {

    /*
     * Class that attaches to a Text element.
     * Using setText, the text in the Text element will begin typing.
     */
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
