using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TerrainAccessor : MonoBehaviour {
	
	public Text accessUpdate;
	public GameObject fpc;
	public AudioClip barnacleWatersBackgroundMusic = null;
	public AudioClip grassyPlainsBackgroundMusic = null;

	private string currentEnvironment;
	private bool isUnderwater;
	private Color normalAtmosphereColor;
	private Color underwaterAtmosphereColor;
	private float normalFogDensity;
	private float underwaterFogDensity;
	private Material noSkybox = null;
	private Material defaultSkybox;
	private GameObject waterLine;
	private string[] particleSystems = new string[0];
	private bool snowySceneLoaded = false;

	// Use this for initialization
	void Start () {
		accessUpdate.text = "";
        string environment = SceneManager.GetActiveScene().name;
        switch (environment)
        {
            case "DefaultPlaneScene":
                currentEnvironment = "grassy";
                break;
            case "SnowyMountainScene":
                currentEnvironment = "snowy";
                break;
            case "UnderwaterScene":
                currentEnvironment = "underwater";
                break;
        }
		isUnderwater = false;
		normalAtmosphereColor = new Color (0.5f, 0.5f, 0.5f, 0.5f);
		underwaterAtmosphereColor = new Color(0.4f, 0.6f, 1.0f, 0.8f);
		normalFogDensity = 0.002f;
		underwaterFogDensity = 0.01f;
		waterLine = GameObject.Find ("TopWaterLayer");
		defaultSkybox = RenderSettings.skybox;
		particleSystems = new string[] {
			"UnderwaterParticleSystem",
			"UnderwaterParticleSystem-1",
			"UnderwaterParticleSystem-2"
		};
		muteOceanParticleSystems ();
		//fpc.transform.position = new Vector3 (1505.9f, 488.6f, 1197.0f);
		//setUnderwaterAtmosphere ();
	}

	void Update() {

        //If the player is in the underwater environment and happens to fall below the water line, the atmosphere
        //will change to mimic an underwater experience.
        if (currentEnvironment.Equals("underwater")){
		    if (!isUnderwater) {
			    if (waterLine != null && transform.position.y < waterLine.transform.position.y) {
				    setUnderwaterAtmosphere ();
				    isUnderwater = true;
			    }
		    } else if(isUnderwater) {
			    if (waterLine != null && transform.position.y > waterLine.transform.position.y) {
				    resetAtmosphere ();
				    isUnderwater = false;
			    }
		    }
        }
    }

	void setUnderwaterAtmosphere() {
		RenderSettings.fogColor = underwaterAtmosphereColor;
		RenderSettings.fogDensity = underwaterFogDensity;
		RenderSettings.skybox = noSkybox;
		Camera.main.backgroundColor = new Color(0, 0.4f, 0.7f, 1);
		if (waterLine != null) {
			waterLine.gameObject.SetActive (false);
		}
		GameObject.Find ("PlaneUniverse").GetComponent<AudioSource> ().Stop ();
		GameObject.Find ("RowBoat").GetComponent<AudioSource> ().Play ();
		if (barnacleWatersBackgroundMusic != null) {
			AudioSource audio = GetComponent<AudioSource>();
			audio.clip = barnacleWatersBackgroundMusic;
			audio.Play();
		}

		RenderSettings.fog = true;
		waterLine = GameObject.Find ("TopWaterLayer");
		waterLine.SetActive (true);
		oceanParticleSystemsTurnUp ();

	}

	void resetAtmosphere() {
		RenderSettings.fogColor = normalAtmosphereColor;
		RenderSettings.fogDensity = normalFogDensity;
		RenderSettings.skybox = defaultSkybox;
		RenderSettings.fog = false;
		if (waterLine != null) {
			waterLine.gameObject.SetActive (true);
		}
		GameObject.Find ("RowBoat").GetComponent<AudioSource> ().Stop ();
		GameObject.Find ("PlaneUniverse").GetComponent<AudioSource> ().Play ();
		if (grassyPlainsBackgroundMusic != null) {
			AudioSource audio = GetComponent<AudioSource>();
			audio.clip = grassyPlainsBackgroundMusic;
			audio.Play();
		}
		muteOceanParticleSystems ();
	}

	void muteOceanParticleSystems() {
		waterLine = GameObject.Find ("TopWaterLayer");
		if (waterLine != null) {
			waterLine.SetActive (false);
		}
		foreach (string system in this.particleSystems) {
			GameObject sys = GameObject.Find (system);
			if(sys!=null) {
				sys.SetActive(false);
			}
		}
	}


	void oceanParticleSystemsTurnUp () 
	{
		foreach (string system in particleSystems) {
			GameObject sys = GameObject.Find (system);
			if (sys != null) {
				sys.SetActive (true);
			}
		}
	}
	
	void OnControllerColliderHit(ControllerColliderHit hit) {
		attemptTravel (hit.collider.tag);
	}
		
	void OnTriggerStay(Collider hit) {
		attemptTravel (hit.tag);
	}

	void OnTriggerExit(Collider hit) {
		accessUpdate.text = "";
	}
		
	void attemptTravel(string accessor) {
		switch (accessor) {
		case "snowyTerrainAccessor":
			if (ReliefStats.instance.HAS_ACCESS_TO_SNOWY_TERRAIN) {
				if (!snowySceneLoaded) {
					SceneManager.LoadScene ("SnowyMountainScene", LoadSceneMode.Single);
					snowySceneLoaded = true;
				}
				DigitalRuby.RainMaker.RainScript.isSnowFalling = true;
				currentEnvironment = "snowy";
				isUnderwater = false;
//				GameObject.Find ("SouthSnowWall").SetActive (true);
				resetAtmosphere ();
				fpc.transform.position = new Vector3 (1455.0f, 161.0f, 11.5f);
			} else {
				int delta = ReliefStats.instance.SNOWY_TERRAIN_MAX_COLLECT - ReliefStats.instance.currentSnowyTerrainProgress;
				accessUpdate.text = System.String.Format (ReliefStats.instance.NO_ACCESS_SNOWY_TERRAIN, delta);
			}
			break;
		case "underwaterTerrainAccessor":
			if (ReliefStats.instance.HAS_ACCESS_TO_UNDERWATER_TERRAIN) {
				currentEnvironment = "underwater";
				SceneManager.LoadScene ("UnderwaterScene", LoadSceneMode.Single);
				DigitalRuby.RainMaker.RainScript.isSnowFalling = false;
				fpc.transform.position = new Vector3 (1505.9f, 488.6f, 1197.0f);
			} else {
				int delta = ReliefStats.instance.UNDERWATER_TERRAIN_MAX_COLLECT - ReliefStats.instance.currentUnderwaterTerrainProgress;
				accessUpdate.text = System.String.Format (ReliefStats.instance.NO_ACCESS_UNDERWATER_TERRAIN, delta);
			}
			break;
		
		case "grassyTerrainAccessor":
			currentEnvironment = "grassy";
			DigitalRuby.RainMaker.RainScript.isSnowFalling = false;
                isUnderwater = false;
			resetAtmosphere ();
                SceneManager.LoadScene("DefaultPlaneScene", LoadSceneMode.Single);
                fpc.transform.position = new Vector3 (0.0f, 3.0f, 0.0f);
			break;
		}
	}
		
}
