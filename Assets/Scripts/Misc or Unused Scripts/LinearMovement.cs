using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearMovement : MonoBehaviour {

    public float MAX_SWIM_BOUNDS;
    public float speed;
	private float current_progress;
	private float direction;

	// Use this for initialization
	void Start () {
		current_progress = 0.0f;
		direction = -1.0f;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 updatedPosition = this.transform.position;
		this.transform.position = new Vector3(updatedPosition.x + (direction * speed), updatedPosition.y, updatedPosition.z);
		current_progress += Mathf.Abs (direction*speed);
		if (current_progress > MAX_SWIM_BOUNDS) {
			current_progress = 0;
			Vector3 currentRotation = this.transform.rotation.eulerAngles;
			if (direction == -1) {
				this.transform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y + 180, currentRotation.z);
			} else {
				this.transform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y + 180, currentRotation.z);	
			}
			direction *= -1;
		}
	}
}
