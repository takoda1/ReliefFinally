using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlainsTigerRun : MonoBehaviour {

    public float runAwayDistance;
    private float MAX_RUN_BOUNDS = 50.0f;
    private float current_progress;
    private float direction;
    private GameObject player;
    private Vector3[] playerPositions = new Vector3[2]; //0 is current position, 1 is last known position
    private Vector3 initialOrientation;
    

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        current_progress = 0.0f;
        playerPositions[0] = player.transform.position;
        playerPositions[1] = player.transform.position;
        direction = -1.0f;
        initialOrientation = this.transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
       

        Vector3 currentPosition = this.transform.position;
        playerPositions[1] = playerPositions[0];
        playerPositions[0] = player.transform.position;
        Vector3 playerMovement = playerPositions[0] - playerPositions[1];
        Vector3 currentToTiger = currentPosition - playerPositions[0];
        Vector3 previousToTiger = currentPosition - playerPositions[1];

        if (currentToTiger.magnitude < runAwayDistance && previousToTiger.magnitude > currentToTiger.magnitude)
        {
            this.transform.position = new Vector3(currentPosition.x + playerMovement.x, currentPosition.y, currentPosition.z + playerMovement.z);
            this.transform.rotation = player.transform.rotation;
        }
        else
        {
            this.transform.position = new Vector3(currentPosition.x + (direction * .1f), currentPosition.y, currentPosition.z);
            if(direction == 1)
            {
                this.transform.rotation = Quaternion.Euler(initialOrientation.x, initialOrientation.y - 180, initialOrientation.z);
            }
            else
            {
                this.transform.rotation = Quaternion.Euler(initialOrientation.x, initialOrientation.y, initialOrientation.z);
            }
            current_progress += Mathf.Abs(direction * .1f);
            if (current_progress > MAX_RUN_BOUNDS)
            {
                current_progress = 0;
                Vector3 currentRotation = this.transform.rotation.eulerAngles;
                if (direction == -1)
                {
                    this.transform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y -180, currentRotation.z);
                }
                else
                {
                    this.transform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y - 180, currentRotation.z);
                }
                direction *= -1;
            }
        }


    }
}