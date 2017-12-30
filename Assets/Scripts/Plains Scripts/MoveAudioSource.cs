using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAudioSource : MonoBehaviour {

    public Transform point1;
    public Transform point2;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    // Update is called once per frame
    void Update () {
        Vector3 middle = new Vector3(average(point1.position.x, point2.position.x), average(point1.position.y, point2.position.y), average(point1.position.z, point2.position.z));
        this.transform.position = middle;
	}


    private float average(float a, float b)
    {
        return (a + b) / 2.0f;
    }


    /*
     * Transform temp = waypoints[0];
        Transform temp2 = waypoints[waypoints.Length - 1];
        foreach(Transform t in waypoints)
        {
            if (Vector3.Distance(t.position, player.position) < Vector3.Distance(temp.position, player.position))
                temp = t;
            if (Vector3.Distance(temp.position, player.position) < Vector3.Distance(t.position, player.position) &&
                Vector3.Distance(t.position, player.position) < Vector3.Distance(temp2.position, player.position))
                temp2 = t;
        }
        Vector3 middle = new Vector3(average(temp.position.x, temp2.position.x), average(temp.position.y, temp2.position.y), average(temp.position.z, temp2.position.z));
        this.transform.position = middle;
     */
}
