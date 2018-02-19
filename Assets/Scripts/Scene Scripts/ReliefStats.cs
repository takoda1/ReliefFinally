using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

/*
 * Created by: Morgan Howell (probably)
 * Modified by: Takoda Ren
 * 
 * Holds data on how many pieces a player has collected
 * across scenes. Attach to an empty gameobject in any
 * scene that has shape pickup functionality.
 */

public class ReliefStats : MonoBehaviour {

    //Static modifiers for pieces
    private static int snowyPieces = 5;
    private static int barnaclePieces = 7;
    private static int newPieces = 9;

    //Used to hold data across scenes
    public static ReliefStats instance = null;

    //global statistics for game progress
    public int SNOWY_MAX_COLLECT;
    public int BARNACLE_MAX_COLLECT;
    public int NEW_MAX_COLLECT;
    public int currentSnowyProgress;
    public int currentBarnacleProgress;
    public int currentNewProgress;


    //global strings for UI text displays

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
        NEW_MAX_COLLECT = newPieces;
        currentSnowyProgress = 0;
        currentBarnacleProgress = 0;
        currentNewProgress = 0;

        DontDestroyOnLoad(instance);
    }


    public void IncrementSnowyPiece()
    {
        currentSnowyProgress++;
    }

    public int SnowyPiecesCollected()
    {
        return currentSnowyProgress;
    }

    public bool HasAccessToSnowy()
    {
        return currentSnowyProgress >= SNOWY_MAX_COLLECT;
    }

    public void IncrementBarnaclePiece()
    {
        currentBarnacleProgress++;
    }

    public int BarnaclePiecesCollected()
    {
        return currentBarnacleProgress;
    }

    public bool HasAccessToBarnacle()
    {
        return currentBarnacleProgress >= BARNACLE_MAX_COLLECT;
    }

    public void IncrementNewPiece()
    {
        currentNewProgress++;
    }

    public int NewPiecesCollected()
    {
        return currentNewProgress;
    }

    public bool HasAccessToNew()
    {
        return currentNewProgress >= NEW_MAX_COLLECT;
    }
}
