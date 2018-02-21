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

    //Used to hold data across scenes
    public static ReliefStats instance = null;

    //global statistics for game progress
    private int SNOWY_MAX_COLLECT = 5;
    private int BARNACLE_MAX_COLLECT = 0;
    private int NEW_MAX_COLLECT = 9;
    private int currentSnowyProgress;
    private int currentBarnacleProgress;
    private int currentNewProgress;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        UnityEngine.XR.XRSettings.enabled = true;
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

    public int MaxSnowyPieces()
    {
        return SNOWY_MAX_COLLECT;
    }

    public int SnowyPiecesLeft()
    {
        return SNOWY_MAX_COLLECT - currentSnowyProgress;
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

    public int BarnaclePiecesLeft()
    {
        return BARNACLE_MAX_COLLECT - currentBarnacleProgress;
    }

    public int MaxBarnaclePieces()
    {
        return BARNACLE_MAX_COLLECT;
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
    
    public int NewPiecesLeft()
    {
        return NEW_MAX_COLLECT - currentNewProgress;
    }

    public int MaxNewPieces()
    {
        return NEW_MAX_COLLECT;
    }
}
