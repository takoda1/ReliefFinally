using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Created by: Takoda Ren
 * 1/26/2018
 * Description: Attached to any object that is to linearly
 * move between any number of fixed positions.
 */
public class MoveToPosition : MonoBehaviour {

    public Transform[] targetPositions;
    public float speed;
    public float changeTargetDistance;
    public bool changeOrientationOnChangedTarget;
    private int counter = 0;

    private void FixedUpdate()
    {
        //if the distance between this object and the current target position is greater than changeTargetDistance
        if (Math.Pow(Math.Pow(targetPositions[counter].position.x - this.transform.position.x, 2)
                   + Math.Pow(targetPositions[counter].position.z - this.transform.position.z, 2), .5) > changeTargetDistance)
        {
            //move the object toward target position by speed * Time.deltaTime
            Vector3 pos = Vector3.MoveTowards(transform.position, targetPositions[counter].position, speed * Time.deltaTime);
            if (changeOrientationOnChangedTarget)
            {
                Vector3 newDir = Vector3.RotateTowards(transform.position, targetPositions[counter].position, 15, 15);
                Debug.DrawRay(transform.position, newDir, Color.red, 1);
                this.transform.rotation = Quaternion.LookRotation(newDir);
            }
            this.transform.position = pos;
        }
        else
        {
            //will loop back to the beginning position if counter exceeds the number of targetPositions
            counter = (counter + 1) % targetPositions.Length;
        }
    }


}
