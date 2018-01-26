using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class ReliefStats : MonoBehaviour {

    //Static modifiers for pieces
    public static int snowyPieces = 5;
    public static int barnaclePieces = 7;

    //Used to hold data across scenes
    public static ReliefStats instance = null;

    //global statistics for game progress
    public int SNOWY_MAX_COLLECT;
	public int BARNACLE_MAX_COLLECT;
    public bool HAS_ACCESS_TO_SNOWY;
    public bool HAS_ACCESS_TO_BARNACLE;
	public int currentSnowyProgress;
    public int currentBarnacleProgress;
    public bool[] snowyPiecesFound;
    public bool[] barnaclePiecesFound;


    //global strings for UI text displays
    public string SNOWY_PIECE_RETRIEVED;
    public string BARNACLE_PIECE_RETRIEVED;
    public string SNOWY_ALL_PIECES_RETRIEVED;
    public string BARNACLE_ALL_PIECES_RETRIEVED;
    public string NO_ACCESS_SNOWY;
    public string NO_ACCESS_BARNACLE;
    public string ACCESS_SNOWY;
    public string ACCESS_BARNACLE;

    //user specific credentials
    public string username = null; //nil for local-dev mode

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        UnityEngine.XR.XRSettings.enabled = true;
        SNOWY_MAX_COLLECT = snowyPieces;

        BARNACLE_MAX_COLLECT = barnaclePieces;
        HAS_ACCESS_TO_SNOWY = false;
        HAS_ACCESS_TO_BARNACLE = false;
        currentSnowyProgress = 0;
        currentBarnacleProgress = 0;
        snowyPiecesFound = new bool[snowyPieces];
        barnaclePiecesFound = new bool[barnaclePieces];
        SNOWY_PIECE_RETRIEVED = "Winter coat piece retrieved! {0} of " + snowyPieces.ToString();
        BARNACLE_PIECE_RETRIEVED = "Scuba gear piece retrieved! {0} of " + barnaclePieces.ToString();
        SNOWY_ALL_PIECES_RETRIEVED = "Congratulations! You have collected all pieces of the winter coat. You can access Frigid Cliff.";
        BARNACLE_ALL_PIECES_RETRIEVED = "Congratulations! You have collected all pieces of the scuba gear. You can access Barnacle Waters.";
        NO_ACCESS_SNOWY = "You have {0} winter coat pieces left before you can access Frigid Cliff.";
        NO_ACCESS_BARNACLE = "You have {0} scuba gear pieces left before you can access Barnacle Waters.";
        ACCESS_SNOWY = "Access Granted! You are using your snow coat to access Frigid Cliff...";
        ACCESS_BARNACLE = "Access Granted! You are using your scuba gear to access Barnacle Waters...";
        
        DontDestroyOnLoad(instance);
    }
}
