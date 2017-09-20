using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;


namespace GlobalConstants {
	public class ReliefStats : MonoBehaviour {
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

		//static instance of this class to reset vars after load
		public static ReliefStats Instance;

		void Start()
		{

			//global deployment specific configuration
			VRSettings.enabled = true;
		
			ReliefStats.Instance = this;
			ReliefStats.Instance.SNOWY_TERRAIN_MAX_COLLECT = 5;
			ReliefStats.Instance.UNDERWATER_TERRAIN_MAX_COLLECT = 7;
			ReliefStats.Instance.snowyTerrainPiecesFound = new bool[5];
			ReliefStats.Instance.underwaterTerrainPiecesFound = new bool[7];
			ReliefStats.Instance.currentSnowyTerrainProgress = 0;
			ReliefStats.Instance.currentUnderwaterTerrainProgress = 0;
			ReliefStats.Instance.HAS_ACCESS_TO_UNDERWATER_TERRAIN = false;
			ReliefStats.Instance.HAS_ACCESS_TO_SNOWY_TERRAIN = true;
			ReliefStats.Instance.SNOWY_TERRAIN_PIECE_RETRIEVED = "Winter coat piece retrieved! {0} of " + ReliefStats.Instance.SNOWY_TERRAIN_MAX_COLLECT.ToString ();
			ReliefStats.Instance.UNDERWATER_TERRAIN_PIECE_RETRIEVED = "Scuba gear piece retrieved! {0} of " + ReliefStats.Instance.UNDERWATER_TERRAIN_MAX_COLLECT.ToString ();
			ReliefStats.Instance.SNOWY_TERRAIN_ALL_PIECES_RETRIEVED = "Congratulations! You have collected all pieces of the winter coat. You can access Frigid Cliff.";
			ReliefStats.Instance.UNDERWATER_TERRAIN_ALL_PIECES_RETRIEVED = "Congratulations! You have collected all pieces of the scuba gear. You can access Barnacle Waters.";
			ReliefStats.Instance.NO_ACCESS_SNOWY_TERRAIN = "You have {0} winter coat pieces left before you can access Frigid Cliff.";
			ReliefStats.Instance.NO_ACCESS_UNDERWATER_TERRAIN = "You have {0} scuba gear pieces left before you can access Barnacle Waters.";
			ReliefStats.Instance.ACCESS_SNOWY_TERRAIN = "Access Granted! You are using your snow coat to access Frigid Cliff...";
			ReliefStats.Instance.ACCESS_UNDERWATER_TERRAIN = "Access Granted! You are using your scuba gear to access Barnacle Waters...";

			//global game object preparation
			MeshRenderer[] children = null; 
			GameObject caveParts = GameObject.Find ("Cave parts");
			if(caveParts != null) {
				children = caveParts.GetComponentsInChildren<MeshRenderer>();
			}
				
			foreach(MeshRenderer comp in children) {
				GameObject child = comp.gameObject;
				child.AddComponent<Rigidbody> ();
				Rigidbody rb = child.GetComponent<Rigidbody> ();
				rb.isKinematic = true;
				rb.useGravity = true;
				child.AddComponent<MeshCollider> ();
			}
		}
	}

}
