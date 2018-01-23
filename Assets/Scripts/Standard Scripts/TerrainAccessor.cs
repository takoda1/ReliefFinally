using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TerrainAccessor : MonoBehaviour {
	
	public Text accessUpdate;

	private string currentEnvironment;
	private bool snowySceneLoaded = false;
    

    // Use this for initialization
    void Start () {
		accessUpdate.text = "";
        string environment = SceneManager.GetActiveScene().name;
        switch (environment)
        {
            case "TutorialScene":
                currentEnvironment = "tutorial";
                break;
            case "GrassyPlainsScene":
                currentEnvironment = "grassy";
                break;
            case "SnowyMountainScene":
                currentEnvironment = "snowy";
                DigitalRuby.RainMaker.RainScript.isSnowFalling = true;
                break;
            case "UnderwaterScene":
                currentEnvironment = "underwater";
                break;
        }
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
			if (ReliefStats.instance.HAS_ACCESS_TO_SNOWY_TERRAIN) {
                SceneManager.LoadScene ("SnowyMountainScene", LoadSceneMode.Single);
				DigitalRuby.RainMaker.RainScript.isSnowFalling = true;
				currentEnvironment = "snowy";
			} else {
				int delta = ReliefStats.instance.SNOWY_TERRAIN_MAX_COLLECT - ReliefStats.instance.currentSnowyTerrainProgress;
				accessUpdate.text = System.String.Format (ReliefStats.instance.NO_ACCESS_SNOWY_TERRAIN, delta);
                StartCoroutine(eraseCurrentStatus());
			}
			break;
		case "underwaterTerrainAccessor":
			if (ReliefStats.instance.HAS_ACCESS_TO_UNDERWATER_TERRAIN) {
				currentEnvironment = "underwater";
				SceneManager.LoadScene ("UnderwaterScene", LoadSceneMode.Single);
				DigitalRuby.RainMaker.RainScript.isSnowFalling = false;
			}
            else {
				int delta = ReliefStats.instance.UNDERWATER_TERRAIN_MAX_COLLECT - ReliefStats.instance.currentUnderwaterTerrainProgress;
				accessUpdate.text = System.String.Format (ReliefStats.instance.NO_ACCESS_UNDERWATER_TERRAIN, delta);
                StartCoroutine(eraseCurrentStatus());
                }
			break;
		
		case "grassyTerrainAccessor":
			currentEnvironment = "grassy";
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
