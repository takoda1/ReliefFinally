using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Created by Takoda Ren
 * 1/25/2018
 * 
 * Description: To be attached to a Gameobject that is to move on a fixed
 * line between point1 and point2, maintaining as close a distance to the 
 * varyingPoint. Projection of varyingPoint onto the 3D line point1->point2
 * in essence.
 * 
 * Used http://www.nabla.hr/PC-LinePlaneIn3DSpB6.htm for reference while
 * writing equation code.
 * 
 */
public class ProjectionOntoLine : MonoBehaviour {

    //provide point1 and point2 that represent the line this object is to move between
    public Transform point1;
    public Transform point2;
    //provide the moving point that this object will be attracted to
    public Transform varyingPoint;

    private float x1, y1, z1;
    private float l, m, n;

    private void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        //equation of line passing through point1 and point 2 calculations
        //form being (x-x1)/l = (y-y1)/m = (z-z1)/n, x1, y1, z1 being point passed thru and 
        //l, m, and n being the components of the direciton vector passing thru point1 and point2.
        x1 = point1.position.x;
        y1 = point1.position.y;
        z1 = point1.position.z;
        Vector3 direction = new Vector3(point2.position.x - point1.position.x, point2.position.y - point1.position.y, point2.position.z - point1.position.z);
        direction.Normalize();
        l = direction.x; m = direction.y; n = direction.z;
    }
    
    // Update is called once per frame
    void Update () {
        float a, b, c; //components of player position
        a = varyingPoint.position.x; b = varyingPoint.position.y; c = varyingPoint.position.z;
        float d = (l * a + m * b + n * c) / -1f;
        float t = (-l * x1 - m * y1 - n * z1 - d) / (Mathf.Pow(l, 2) + Mathf.Pow(m, 2) + Mathf.Pow(n, 2));
        float x = l * t + x1; float y = m * t + y1; float z = n * t + z1;
        Vector3 temp1 = point1.position; Vector3 temp2 = point2.position;
        Debug.unityLogger.Log("ABC", varyingPoint.position.x + " " + varyingPoint.position.z);
        if (x > Mathf.Min(temp1.x, temp2.x) && z > Mathf.Min(temp1.z, temp2.z) && x < Mathf.Max(temp1.x, temp2.x) && z < Mathf.Max(temp1.z, temp2.z))
        {
            Vector3 middle = new Vector3(x, y, z);
            this.transform.position = middle;
        }
	}
    //vector12 = new Vector3(point2.position.x - point1.position.x, point2.position.y - point1.position.y, point2.position.z - point1.position.z);
    // Vector3 vector1Varying = new Vector3(varyingPoint.position.x - point1.position.x, varyingPoint.position.y - point1.position.y, varyingPoint.position.z - point1.position.z);
    // Vector3 point = vector12 + Vector3.Dot(vector1Varying, vector12) / vector12.magnitude * vector12;
}
