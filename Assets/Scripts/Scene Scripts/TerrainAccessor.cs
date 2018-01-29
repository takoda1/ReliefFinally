using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
 * Created by Morgan Howell (Probably)
 * Modified by: Takoda Ren
 * 
 * Whenever the player hits a portal trigger or collider,
 * loads the corresponding scene if the player has collected
 * enough pieces/treasures.
 */

public class TerrainAccessor : MonoBehaviour {
	
    //Text attached to canvas
	public Text accessUpdate;
    
    //UI strings
    public string NO_ACCESS_SNOWY;
    public string NO_ACCESS_BARNACLE;
    public string ACCESS_SNOWY;
    public string ACCESS_BARNACLE;

    // Use this for initialization
    void Start () {
		accessUpdate.text = "";

        NO_ACCESS_SNOWY = "You have {0} winter coat pieces left before you can access Frigid Cliff.";
        NO_ACCESS_BARNACLE = "You have {0} scuba gear pieces left before you can access Barnacle Waters.";
        ACCESS_SNOWY = "Access Granted! You are using your snow coat to access Frigid Cliff...";
        ACCESS_BARNACLE = "Access Granted! You are using your scuba gear to access Barnacle Waters...";
    }
	

    
	void OnControllerColliderHit(ControllerColliderHit hit) {
		attemptTravel (hit.collider.tag);
	}

    private void OnTriggerEnter(Collider hit)
    {
        attemptTravel(hit.tag);
    }


    void attemptTravel(string accessor) {
		switch (accessor) {
		    case "snowyTerrainAccessor":
                //if the player is able to access the scene, load the scene,
                //otherwise, update text informing player how many pieces they
                //still need to collect.
			    if (ReliefStats.instance.HasAccessToSnowy()) {
                    accessUpdate.text = ACCESS_SNOWY;
                    SceneManager.LoadScene ("SnowyMountainScene", LoadSceneMode.Single);
			    } else {
				    int delta = ReliefStats.instance.SNOWY_MAX_COLLECT - ReliefStats.instance.currentSnowyProgress;
				    accessUpdate.text = System.String.Format (NO_ACCESS_SNOWY, delta);
                    StartCoroutine(eraseCurrentStatus());
			    }
			    break;
		    case "underwaterTerrainAccessor":
			    if (ReliefStats.instance.HasAccessToBarnacle()) {
                    accessUpdate.text = ACCESS_BARNACLE;
                    SceneManager.LoadScene ("UnderwaterScene", LoadSceneMode.Single);
			    }
                else {
				    int delta = ReliefStats.instance.BARNACLE_MAX_COLLECT - ReliefStats.instance.currentBarnacleProgress;
				    accessUpdate.text = System.String.Format (NO_ACCESS_BARNACLE, delta);
                    StartCoroutine(eraseCurrentStatus());
                    }
			    break;
		
		    case "grassyTerrainAccessor":
                    SceneManager.LoadScene("GrassyPlainsScene", LoadSceneMode.Single);
			    break;
		    }
	}

    IEnumerator eraseCurrentStatus()
    {
        yield return new WaitForSeconds(3);
        accessUpdate.text = "";
    }


}
