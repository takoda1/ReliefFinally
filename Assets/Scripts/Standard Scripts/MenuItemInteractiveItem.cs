using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuItemInteractiveItem : MonoBehaviour {

    private float moveDistance = .5f;

    [SerializeField] private InteractiveItem interactiveItem;

    public string Scene;
	
	// Update is called once per frame

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

    private void HandleOver()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - moveDistance);

    }

    private void HandleOut()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + moveDistance);
    }

    private void HandleClick()
    {
        SceneManager.LoadScene(Scene, LoadSceneMode.Single);
    }
}
