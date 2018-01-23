using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : MonoBehaviour {

    public float x, y, z;
    public Vector3 CircleDistance;
    public Vector3 CircleRadius;
    public float AngleChange;

    private float wanderAngle;
    protected Rigidbody rigidBody;

    virtual public void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        wanderAngle = .1f;
        CircleRadius /= 100;
    }

    // Update is called once per frame
    virtual public void Update() {
        Vector3 circleCenter = rigidBody.velocity;
        circleCenter.Normalize();
        circleCenter.Scale(CircleDistance);

        Vector3 displacement = new Vector3(x, y, z);
        displacement.Scale(CircleRadius);
        displacement = setAngle(displacement, wanderAngle);

        wanderAngle += Random.value * AngleChange - AngleChange * .5f;

        Vector3 wanderForce = circleCenter + displacement;
        rigidBody.velocity = wanderForce;
        float deg = Mathf.Rad2Deg * Mathf.Atan(wanderForce.x / wanderForce.z); //i feel like this should be z/x but i couldn't get z/x to work
        if(wanderForce.z < 0) {
            deg += 180;
        }
        this.transform.rotation = Quaternion.Euler(0, deg, 0);
    }

    public Vector3 setAngle(Vector3 vector, float angle)
    {
        var length = vector.magnitude;
        vector.x = Mathf.Cos(angle) * length;
        vector.z = Mathf.Sin(angle) * length;
        return vector;
    }
}
