using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPosition : MonoBehaviour {

    public Transform[] targetPositions;
    public float speed;
    private int counter = 0;
	

    void Start()
    {

    }

    private void FixedUpdate()
    {
        if (Math.Pow(Math.Pow(targetPositions[counter].position.x - this.transform.position.x, 2)
                   + Math.Pow(targetPositions[counter].position.z - this.transform.position.z, 2), .5) > 5)
        {
            Vector3 pos = Vector3.MoveTowards(transform.position, targetPositions[counter].position, speed * Time.deltaTime);
            GetComponent<Rigidbody>().MovePosition(pos);
        }
        else
        {
            counter = (counter + 1) % targetPositions.Length;
        }
    }

    IEnumerator wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }


}
