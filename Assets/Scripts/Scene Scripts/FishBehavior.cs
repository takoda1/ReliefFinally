using UnityEngine;

public class FishBehavior : Wander {

    public float FleeDistance;
    private GameObject player;
    private Vector3[] playerPositions = new Vector3[2];

    // Use this for initialization
    override public void Start () {
        player = GameObject.FindWithTag("Player");
        playerPositions[0] = player.transform.position;
        playerPositions[1] = player.transform.position;
        base.Start();
    }

    // Update is called once per frame
    override public void Update () {
        playerPositions[1] = playerPositions[0];
        playerPositions[0] = player.transform.position;
        Vector3 playerVelocity = playerPositions[0] - playerPositions[1];
        Vector3 playerFishDistance = playerPositions[0] - this.transform.position;
		if(playerFishDistance.magnitude < FleeDistance)
        {
            playerVelocity = playerVelocity.normalized * 50; //big burst of velocity for fish to flee
            
            rigidBody.velocity = new Vector3(playerVelocity.z, 0, -1 * playerVelocity.x);
            /*
             * only player movements can bring player within flee range, and as such the fish will always flee
             * to the right by 90 degrees perpendicular to the players orientation due to the above Vector formula,
             * so the rotation will always just be the player's rotations plus 90 degrees
             */
            float deg = Mathf.Rad2Deg * Mathf.Atan(rigidBody.velocity.x / rigidBody.velocity.z); //i feel like this should be z/x but i couldn't get z/x to work
            if (rigidBody.velocity.z < 0)
            {
                deg += 180;
            }
            this.transform.rotation = Quaternion.Euler(0, deg, 0);
            //this.transform.rotation = Quaternion.Euler(0, player.transform.rotation.y + 90, 0); 
        }
        else
        {
            base.Update();
        }
	}
}
