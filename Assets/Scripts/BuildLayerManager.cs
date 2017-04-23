using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildLayerManager : MonoBehaviour {

	public static BuildLayerManager instance;
	public GameObject BuildPlacePrefab;
	public Transform PlayArea;
	public int boardWidthInUnits;
	public int boardHeightInUnits;
	[SerializeField]
	int _playSpaceWidthInUnits;
	[SerializeField]
	int _playSpaceHeightInUnits;
	List<Transform> buildPlaces;
	Dictionary<Vector3, Transform> placeData;


	void Awake(){
		instance = this;
		placeData = new Dictionary<Vector3, Transform> ();
	}

	void Start(){
		for (int x = -(boardWidthInUnits/2); x < (boardWidthInUnits/2); x++) {
			for (int z = -(boardHeightInUnits / 2); z < (boardHeightInUnits/2); z++) {
				GameObject newBuildplace = Instantiate (BuildPlacePrefab) as GameObject;
				newBuildplace.transform.SetParent (PlayArea);
				newBuildplace.transform.position = new Vector3 ((float)x, 1f, (float)z);
				if(isInBounds (newBuildplace.transform.position)){
					newBuildplace.GetComponent <SpriteRenderer>().color = Color.green;
				}
			}
		}
	}

	public void placeTower(Vector3 placePoint, Transform towerPlaced){
		for (float x = (placePoint.x - 1f); x <= (placePoint.x + 1f); x++) {
			for (float z = (placePoint.z -1f); z <= (placePoint.z +1f); z++) {
				placeData [new Vector3 (x, placePoint.y, z)] = towerPlaced;
			}
		}
	}

	public bool canPlace(Vector3 placePoint){
		Vector3 pointToCheck;
		for (float x = (placePoint.x - 1f); x <= (placePoint.x + 1f); x++) {
			for (float z = (placePoint.z -1f); z <= (placePoint.z +1f); z++) {
				pointToCheck = new Vector3 (x, placePoint.y, z);
				if(placeData.ContainsKey (pointToCheck) || !isInBounds (pointToCheck) ){
					return false;
				}
			}
		}
		return true;
	}

	bool isInBounds(Vector3 placePoint){
		int minX = -(_playSpaceWidthInUnits / 2);
		int maxX = _playSpaceWidthInUnits / 2;
		int maxZ = (_playSpaceHeightInUnits / 2);
		int minZ = -(_playSpaceHeightInUnits / 2);
		bool isOutOfBoundsX = (placePoint.x > minX && placePoint.x < maxX);
		bool isOutOfBoundsZ = (placePoint.z > minZ && placePoint.z < maxZ);
		return(isOutOfBoundsX && isOutOfBoundsZ);
	}


}
