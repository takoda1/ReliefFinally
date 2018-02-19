using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Timers;

/*
 * Created by: Morgan Howell (probably)
 * Modified by: Takoda Ren
 * 
 * Placed on a player who picks up objects, which is then updated in the
 * data class accrodingly.
 */
public class PickupShape : MonoBehaviour {
	
	public Text piecePickupProgress;

    public string SNOWY_PIECE_RETRIEVED;
    public string BARNACLE_PIECE_RETRIEVED;
    public string SNOWY_ALL_PIECES_RETRIEVED;
    public string BARNACLE_ALL_PIECES_RETRIEVED;
    public string NEW_PIECE_RETRIEVED;
    public string NEW_ALL_PIECES_RETRIEVED;

    void Start()
    {
        SNOWY_PIECE_RETRIEVED = "Winter coat piece retrieved! {0} of " + ReliefStats.instance.SNOWY_MAX_COLLECT.ToString();
        BARNACLE_PIECE_RETRIEVED = "Scuba gear piece retrieved! {0} of " + ReliefStats.instance.BARNACLE_MAX_COLLECT.ToString();
        NEW_PIECE_RETRIEVED = "Mystical piece retrieved ! {0} of " + ReliefStats.instance.NEW_MAX_COLLECT.ToString();
        SNOWY_ALL_PIECES_RETRIEVED = "Congratulations! You have collected all pieces of the winter coat. You can access Frigid Cliff.";
        BARNACLE_ALL_PIECES_RETRIEVED = "Congratulations! You have collected all pieces of the scuba gear. You can access Barnacle Waters.";
        NEW_ALL_PIECES_RETRIEVED = "Congratulations! You have collected all mystical pieces.";

    }

	void OnControllerColliderHit(ControllerColliderHit hit) {
		UpdatePieceProgress (hit.gameObject.tag, hit.gameObject);
	}

    void UpdatePieceProgress(string pieceTag, GameObject piece)
    {
        switch (pieceTag)
        {
            case "snowyPiece":
                deactivatePiece(piece);
                ReliefStats.instance.IncrementSnowyPiece();
                if (ReliefStats.instance.HasAccessToSnowy())
                {
                    piecePickupProgress.text = SNOWY_ALL_PIECES_RETRIEVED;
                }
                else
                {
                    piecePickupProgress.text = string.Format(SNOWY_PIECE_RETRIEVED, ReliefStats.instance.SnowyPiecesCollected());
                }
                StartCoroutine(eraseCurrentStatus());
                break;
            case "barnaclePiece":
                deactivatePiece(piece);
                ReliefStats.instance.IncrementBarnaclePiece();
                if (ReliefStats.instance.HasAccessToBarnacle())
                {
                    piecePickupProgress.text = BARNACLE_ALL_PIECES_RETRIEVED;
                }
                else
                {
                    piecePickupProgress.text = string.Format(BARNACLE_PIECE_RETRIEVED, ReliefStats.instance.BarnaclePiecesCollected());
                }
                StartCoroutine(eraseCurrentStatus());
                break;
            case "newPiece":
                deactivatePiece(piece);
                ReliefStats.instance.IncrementNewPiece();
                if (ReliefStats.instance.HasAccessToNew())
                {
                    piecePickupProgress.text = NEW_ALL_PIECES_RETRIEVED;
                }
                else
                {
                    piecePickupProgress.text = string.Format(NEW_PIECE_RETRIEVED, ReliefStats.instance.NewPiecesCollected());
                }
                StartCoroutine(eraseCurrentStatus());
                break;
        }
    }

    IEnumerator eraseCurrentStatus()
    {
        yield return new WaitForSeconds(4);
        piecePickupProgress.text = "";
    }

    private void deactivatePiece(GameObject piece)
    {
        piece.SetActive(false);
    }


    /*void showPiecePickupProgress(string pieceType, GameObject piece) {
		switch (pieceType) {

		case "snowPiece":
			piece.SetActive (false);
			updateProgressWinterCoat (parseID (piece.name));
			break;

		case "underwaterPiece":
			piece.SetActive (false);
			updateProgressScubaGear (parseID (piece.name));
			break;
		}
			
	}
		
	void updateProgressWinterCoat(int id){
		if (id <= ReliefStats.instance.SNOWY_MAX_COLLECT) {
			if(ReliefStats.instance.snowyPiecesFound[id - 1] == false) {
                ReliefStats.instance.currentSnowyProgress++;
                ReliefStats.instance.snowyPiecesFound[id - 1] = true;
			}
		}
			
		if (ReliefStats.instance.currentSnowyProgress < ReliefStats.instance.SNOWY_MAX_COLLECT) {
			piecePickupProgress.text = System.String.Format(ReliefStats.instance.SNOWY_PIECE_RETRIEVED, ReliefStats.instance.currentSnowyProgress);
		} else {
			piecePickupProgress.text = ReliefStats.instance.SNOWY_ALL_PIECES_RETRIEVED;
			ReliefStats.instance.HAS_ACCESS_TO_SNOWY = true;
		}
        StartCoroutine(eraseCurrentStatus());
    }


	void updateProgressScubaGear(int id) {
		if (id <= ReliefStats.instance.BARNACLE_MAX_COLLECT) {
			if(ReliefStats.instance.barnaclePiecesFound [id-1] == false){
				ReliefStats.instance.currentBarnacleProgress++;
				ReliefStats.instance.barnaclePiecesFound [id-1] = true;
			}

		}
		if (ReliefStats.instance.currentBarnacleProgress < ReliefStats.instance.BARNACLE_MAX_COLLECT) {
			piecePickupProgress.text = System.String.Format(ReliefStats.instance.BARNACLE_PIECE_RETRIEVED, ReliefStats.instance.currentBarnacleProgress) ;
			
		} else {
			piecePickupProgress.text = ReliefStats.instance.BARNACLE_ALL_PIECES_RETRIEVED;
			ReliefStats.instance.HAS_ACCESS_TO_BARNACLE = true;
		}
        StartCoroutine(eraseCurrentStatus());
	}

	//Precondition: works for names of format: type#id
	string parseType(string name) {
		string type = "";
		foreach (char c in name) {
			if (c == '#') {
				break;
			} else {
				type += c;
			}
		}
		return type;
	}

	//Precondition: works for names of format: type#id
	int parseID(string name) {
		string id = "";
		int idInt = 0;
		bool separatorFound = false;
		foreach (char c in name) {
			if (c == '#') {
				separatorFound = true;
			} else if (separatorFound) {
				id += c;
			}
		}
		//int.TryParse(name, out idInt);
		idInt = System.Int32.Parse(id);
		return idInt;
	}*/
}
