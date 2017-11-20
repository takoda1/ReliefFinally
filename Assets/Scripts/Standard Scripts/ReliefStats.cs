using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class ReliefStats : MonoBehaviour {

    //Static modifiers for pieces
    public static int snowyPieces = 5;
    public static int underwaterPieces = 7;

    //Used to hold data across scenes
    public static ReliefStats instance = null;

    //global statistics for game progress
    public int SNOWY_TERRAIN_MAX_COLLECT;
	public int UNDERWATER_TERRAIN_MAX_COLLECT;
    public bool HAS_ACCESS_TO_SNOWY_TERRAIN;
    public bool HAS_ACCESS_TO_UNDERWATER_TERRAIN;
	public int currentSnowyTerrainProgress;
    public int currentUnderwaterTerrainProgress;
    public bool[] snowyTerrainPiecesFound;
    public bool[] underwaterTerrainPiecesFound;


    //global strings for UI text displays
    public string SNOWY_TERRAIN_PIECE_RETRIEVED;
    public string UNDERWATER_TERRAIN_PIECE_RETRIEVED;
    public string SNOWY_TERRAIN_ALL_PIECES_RETRIEVED;
    public string UNDERWATER_TERRAIN_ALL_PIECES_RETRIEVED;
    public string NO_ACCESS_SNOWY_TERRAIN;
    public string NO_ACCESS_UNDERWATER_TERRAIN;
    public string ACCESS_SNOWY_TERRAIN;
    public string ACCESS_UNDERWATER_TERRAIN;

    //user specific credentials
    public string username = null; //nil for local-dev mode

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        VRSettings.enabled = true;
        SNOWY_TERRAIN_MAX_COLLECT = snowyPieces;

        UNDERWATER_TERRAIN_MAX_COLLECT = underwaterPieces;
        HAS_ACCESS_TO_SNOWY_TERRAIN = false;
        HAS_ACCESS_TO_UNDERWATER_TERRAIN = false;
        currentSnowyTerrainProgress = 0;
        currentUnderwaterTerrainProgress = 0;
        snowyTerrainPiecesFound = new bool[snowyPieces];
        underwaterTerrainPiecesFound = new bool[underwaterPieces];
        SNOWY_TERRAIN_PIECE_RETRIEVED = "Winter coat piece retrieved! {0} of " + snowyPieces.ToString();
        UNDERWATER_TERRAIN_PIECE_RETRIEVED = "Scuba gear piece retrieved! {0} of " + underwaterPieces.ToString();
        SNOWY_TERRAIN_ALL_PIECES_RETRIEVED = "Congratulations! You have collected all pieces of the winter coat. You can access Frigid Cliff.";
        UNDERWATER_TERRAIN_ALL_PIECES_RETRIEVED = "Congratulations! You have collected all pieces of the scuba gear. You can access Barnacle Waters.";
        NO_ACCESS_SNOWY_TERRAIN = "You have {0} winter coat pieces left before you can access Frigid Cliff.";
        NO_ACCESS_UNDERWATER_TERRAIN = "You have {0} scuba gear pieces left before you can access Barnacle Waters.";
        ACCESS_SNOWY_TERRAIN = "Access Granted! You are using your snow coat to access Frigid Cliff...";
        ACCESS_UNDERWATER_TERRAIN = "Access Granted! You are using your scuba gear to access Barnacle Waters...";
        
        DontDestroyOnLoad(instance);
    }
}
