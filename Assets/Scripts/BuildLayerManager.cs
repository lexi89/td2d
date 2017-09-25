using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildLayerManager : MonoBehaviour {

	public static BuildLayerManager instance;
	[SerializeField] LayerMask _buildLayerMask;
	[SerializeField] GameObject _buildPopup;
	[SerializeField] GameObject _buildConfirmPopup;
	[SerializeField] Vector3 BuildpoupOffset;
	GameObject newTower;
	Vector3 _currentBuildPos;
	bool _isBuilding;
	
	void Awake(){
		instance = this;
	}
	
	public void OnBuildPlaceClicked(BuildPlace newBuildPlace)
	{
//		if(currentActiveBuildPlace != null) currentActiveBuildPlace.SetHighlightActive(false);
//		currentActiveBuildPlace = newBuildPlace;
//		newBuildPlace.SetHighlightActive(true);
//		_buildTargetPos = newBuildPlace.transform.position;
//		Vector3 screenPos = Camera.main.WorldToScreenPoint(_buildTargetPos);
//		screenPos += BuildpoupOffset;
//		_buildPopup.transform.position = new Vector3(screenPos.x, screenPos.y, screenPos.z);
//		_buildPopup.SetActive(true);
	}

	public void Build(GameObject TowerPrefab)
	{
		Vector3 _buildPos = Vector3.zero;
		Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
		RaycastHit hit;
		Physics.Raycast(ray, out hit, Mathf.Infinity, _buildLayerMask.value);
		if (hit.collider != null)
		{
			_buildPos = hit.collider.transform.position;
			hit.collider.GetComponent<BuildPlace>().SetHighlightActive(true);
		}
		newTower = Instantiate(TowerPrefab);
		newTower.transform.position = _buildPos;
		_currentBuildPos = _buildPos;
		ShowBuildConfirm(true);
	}

	void ShowBuildConfirm(bool isActive)
	{
		if (isActive)
		{
			_isBuilding = true;
			Vector3 Screenpos = Camera.main.WorldToScreenPoint(_currentBuildPos);
			_buildConfirmPopup.transform.position = Screenpos;
			_buildConfirmPopup.SetActive(true);
		}
		else
		{
			_isBuilding = false;
			_buildConfirmPopup.SetActive(false);
		}
	}

	public void OnBuildConfirm()
	{
		ShowBuildConfirm(false);
	}

	public void OnBuildCancel()
	{
		Destroy(newTower);
		ShowBuildConfirm(false);
	}
	
	

	void Start(){
//		for (int x = -(boardWidthInUnits/2); x < (boardWidthInUnits/2); x++) {
//			for (int z = -(boardHeightInUnits / 2); z < (boardHeightInUnits/2); z++) {
//				GameObject newBuildplace = Instantiate (BuildPlacePrefab);
//                buildPlaces.Add(newBuildplace.transform);
//				newBuildplace.transform.SetParent (PlayArea);
//				newBuildplace.transform.position = new Vector3 ((float)x, 1f, (float)z);
////				if(Debug){
////					newBuildplace.GetComponent <Material>().color = Color.blue;
////				}
//				//if(isInBounds (newBuildplace.transform.position)){
//				//	newBuildplace.GetComponent <SpriteRenderer>().color = Color.green;
//				//}
//			}
//		}
	}

    void OnGUI()
    {
//        if(GUILayout.Button("show build grid")){
//            if(!_isGridShowing){
//                showBuildLines();
//                _isGridShowing = true;
//            } else{
//                _isGridShowing = false;
//                hideBuildLines();
//            }
//        }
    }
//
//    void hideBuildLines(){
//		for (int i = 0; i < buildPlaces.Count; i++)
//		{
//			SpriteRenderer s = buildPlaces[i].GetComponent<SpriteRenderer>();
//            s.sprite = null;
//		} 
//    }
//
//
//    void showBuildLines(){
//        for (int i = 0; i < buildPlaces.Count; i++)
//        {
//            SpriteRenderer s = buildPlaces[i].GetComponent<SpriteRenderer>();
//            s.sprite = GridBoxSprite;
//            //if(placeData.ContainsValue(buildPlaces[i]){
//            //    s.color = cannotBuildColor;
//            //} else{
//            //    s.color = canBuildColor;
//            //}
//        }
//    }
//
//    public void placeTower(Vector3 placePoint, Transform towerPlaced){
//		for (float x = (placePoint.x - 1f); x <= (placePoint.x + 1f); x++) {
//			for (float z = (placePoint.z -1f); z <= (placePoint.z +1f); z++) {
//				//placeData [new Vector3 (x, placePoint.y, z)] = towerPlaced;
//			}
//		}
//	}
//
//	public bool canPlace(Vector3 placePoint){
//		Vector3 pointToCheck;
//		for (float x = (placePoint.x - 1f); x <= (placePoint.x + 1f); x++) {
//			for (float z = (placePoint.z -1f); z <= (placePoint.z +1f); z++) {
//				pointToCheck = new Vector3 (x, placePoint.y, z);
//				if(placeData.ContainsKey (pointToCheck) || !isInBounds (pointToCheck) ){
//					return false;
//				}
//			}
//		}
//		return true;
//	}
//
//	bool isInBounds(Vector3 placePoint){
//		int minX = -(_playSpaceWidthInUnits / 2);
//		int maxX = _playSpaceWidthInUnits / 2;
//		int maxZ = (_playSpaceHeightInUnits / 2);
//		int minZ = -(_playSpaceHeightInUnits / 2);
//		bool isOutOfBoundsX = (placePoint.x > minX && placePoint.x < maxX);
//		bool isOutOfBoundsZ = (placePoint.z > minZ && placePoint.z < maxZ);
//		return(isOutOfBoundsX && isOutOfBoundsZ);
//	}
}
