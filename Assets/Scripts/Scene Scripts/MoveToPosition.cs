using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Created by: Takoda Ren
 * 1/26/2018
 * Description: Attached to any object that is to linearly
 * move between any number of fixed positions.
 * It will go to all of the positions provided then loop to
 * the first one when all have been reached.
 * Orientation is updated accordingly. Make sure to attach to gameobject whose
 * "front" is oriented facing +z. If a model's default front-facing
 * orientation is not +z, enclose it in another gameobject, change
 * the model's transform to have its "front" oriented with +z, and
 * attach this script to the enclosing gameobject.
 * 
 */
public class MoveToPosition : MonoBehaviour {

    public Transform[] targetPositions; //positions to move to
    public float speed;
    public float changeTargetDistance; //distance at which a target is considered as reached
    public bool changeOrientationOnChangedTarget; //whether the object is to rotate toward its destination
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
                Vector3 newDir = targetPositions[counter].position - transform.position;
                //Debug.DrawRay(transform.position, newDir, Color.red, 1);
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
