using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Timers;

public class PickupShape : MonoBehaviour {
	
	public Text piecePickupProgress;

	void Start () {
	}

	void showPiecePickupProgress(string pieceType, GameObject piece) {
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
		
	IEnumerator eraseCurrentStatus() {
		yield return new WaitForSeconds (2);
		piecePickupProgress.text = "";
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
	}

	void OnControllerColliderHit(ControllerColliderHit hit) {
		showPiecePickupProgress (hit.gameObject.tag, hit.gameObject);
	}
}
