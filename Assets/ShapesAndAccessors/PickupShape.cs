using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Timers;
using GlobalConstants;

public class PickupShape : MonoBehaviour {
	
	public Text piecePickupProgress;
	private System.Timers.Timer aTimer;

	void Start () {
		piecePickupProgress.text = "";
		aTimer = new System.Timers.Timer(2000);
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
		aTimer.Enabled = false;
		piecePickupProgress.text = "";
	}
		
	void updateProgressWinterCoat(int id){
		if (id <= GlobalConstants.ReliefStats.Instance.SNOWY_TERRAIN_MAX_COLLECT) {
			if(GlobalConstants.ReliefStats.Instance.snowyTerrainPiecesFound [id-1] == false) {
				GlobalConstants.ReliefStats.Instance.currentSnowyTerrainProgress++;
				GlobalConstants.ReliefStats.Instance.snowyTerrainPiecesFound [id-1] = true;
			}
		}
			
		if (GlobalConstants.ReliefStats.Instance.currentSnowyTerrainProgress < GlobalConstants.ReliefStats.Instance.SNOWY_TERRAIN_MAX_COLLECT) {
			piecePickupProgress.text = System.String.Format(GlobalConstants.ReliefStats.Instance.SNOWY_TERRAIN_PIECE_RETRIEVED, GlobalConstants.ReliefStats.Instance.currentSnowyTerrainProgress);
			StartCoroutine(eraseCurrentStatus());
		} else {
			piecePickupProgress.text = GlobalConstants.ReliefStats.Instance.SNOWY_TERRAIN_ALL_PIECES_RETRIEVED;
			GlobalConstants.ReliefStats.Instance.HAS_ACCESS_TO_SNOWY_TERRAIN = true;
		}
	}


	void updateProgressScubaGear(int id) {
		if (id <= GlobalConstants.ReliefStats.Instance.UNDERWATER_TERRAIN_MAX_COLLECT) {
			if(GlobalConstants.ReliefStats.Instance.underwaterTerrainPiecesFound [id-1] == false){
				GlobalConstants.ReliefStats.Instance.currentUnderwaterTerrainProgress++;
				GlobalConstants.ReliefStats.Instance.underwaterTerrainPiecesFound [id-1] = true;
			}

		}


		if (GlobalConstants.ReliefStats.Instance.currentUnderwaterTerrainProgress < GlobalConstants.ReliefStats.Instance.UNDERWATER_TERRAIN_MAX_COLLECT) {
			piecePickupProgress.text = System.String.Format(GlobalConstants.ReliefStats.Instance.UNDERWATER_TERRAIN_PIECE_RETRIEVED, GlobalConstants.ReliefStats.Instance.currentUnderwaterTerrainProgress) ;
			StartCoroutine(eraseCurrentStatus());
		} else {
			piecePickupProgress.text = GlobalConstants.ReliefStats.Instance.UNDERWATER_TERRAIN_ALL_PIECES_RETRIEVED;
			GlobalConstants.ReliefStats.Instance.HAS_ACCESS_TO_UNDERWATER_TERRAIN = true;
		}
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
