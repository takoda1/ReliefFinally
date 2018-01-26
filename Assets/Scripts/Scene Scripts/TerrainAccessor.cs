using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TerrainAccessor : MonoBehaviour {
	
	public Text accessUpdate;
    

    // Use this for initialization
    void Start () {
		accessUpdate.text = "";
	}

	void Update() {

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
			if (ReliefStats.instance.HAS_ACCESS_TO_SNOWY) {
                SceneManager.LoadScene ("SnowyMountainScene", LoadSceneMode.Single);
				DigitalRuby.RainMaker.RainScript.isSnowFalling = true;
			} else {
				int delta = ReliefStats.instance.SNOWY_MAX_COLLECT - ReliefStats.instance.currentSnowyProgress;
				accessUpdate.text = System.String.Format (ReliefStats.instance.NO_ACCESS_SNOWY, delta);
                StartCoroutine(eraseCurrentStatus());
			}
			break;
		case "underwaterTerrainAccessor":
			if (ReliefStats.instance.HAS_ACCESS_TO_BARNACLE) {
				SceneManager.LoadScene ("UnderwaterScene", LoadSceneMode.Single);
				DigitalRuby.RainMaker.RainScript.isSnowFalling = false;
			}
            else {
				int delta = ReliefStats.instance.BARNACLE_MAX_COLLECT - ReliefStats.instance.currentBarnacleProgress;
				accessUpdate.text = System.String.Format (ReliefStats.instance.NO_ACCESS_BARNACLE, delta);
                StartCoroutine(eraseCurrentStatus());
                }
			break;
		
		case "grassyTerrainAccessor":
                SceneManager.LoadScene("GrassyPlainsScene", LoadSceneMode.Single);
                DigitalRuby.RainMaker.RainScript.isSnowFalling = false;
			break;
		}
	}

    IEnumerator eraseCurrentStatus()
    {
        yield return new WaitForSeconds(3);
        accessUpdate.text = "";
    }


}
