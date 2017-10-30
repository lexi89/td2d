using TMPro;
using UnityEngine;

public class BuildLayerManager : MonoBehaviour {

	public static BuildLayerManager instance;
	public bool IsBuilding{get { return _isBuilding; }}
	public bool IsMovingTower{get { return _isMovingTower; }}
	[SerializeField] Camera MainCam;
	[SerializeField] LayerMask _buildLayerMask;
	[SerializeField] GameObject _buildPopup;
	[SerializeField] Vector3 BuildpoupOffset;
	[SerializeField] GameObject _buildArrows;
	
//	[SerializeField] GameObject _buildArrows;
	Tower _currentSelectedTower;
	GameObject newTowerGO;
	Vector3 _currentBuildPos;
	bool _isBuilding;
	bool _isOnBuildConfirm;
	bool _isMovingTower;
	bool _isInBuildConfirm;

	void Awake(){
		instance = this;
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
		}
		newTowerGO = Instantiate(TowerPrefab);
		newTowerGO.name = TowerPrefab.name;
		newTowerGO.transform.position = _buildPos;
		Tower newTower = newTowerGO.GetComponent<Tower>();
		_currentSelectedTower = newTower;
		MoveBuildArrowsToTower();
//		OnTowerSelected(newTower);
		newTower.SetGhostMode(true);
		_currentSelectedTower = newTower;
//		newTower.SetSelected(true);
		newTower.ShowBuildConfirmUI();
		_currentBuildPos = _buildPos;
		SetIsInBuildConfirm(true);
	}

	public void OnBuildConfirm(Tower tower)
	{
		SetIsInBuildConfirm(false);
		_buildArrows.SetActive(false);
		_buildArrows.transform.SetParent(transform.parent, true);
		GameController.Instance.SpendCoins(tower.Level1Cost);
		Grid.instance.BuildAt(tower.transform.position);
	}

//	public void SetIsBuilding(bool isBuilding)
//	{
//		_isBuilding = isBuilding;
//		if (!isBuilding)
//		{
//			_currentSelectedTower = null;
//		}
//	}

	

	public void OnEmptyBuildPlaceClicked()
	{
		if (_isOnBuildConfirm)
		{
			return;
		}
		_isBuilding = false;
		if (_currentSelectedTower != null)
		{
//			_currentSelectedTower.SetGhostMode(false);
			_currentSelectedTower.SetSelected(false);
		}
		else
		{
			print("no current tower..");
		}
		_buildArrows.SetActive(false);
		_buildArrows.transform.SetParent(transform.parent, true);
	}

	public void OnTowerSelected(Tower newTowerSelected)
	{
		if (CanSelectSomething())
		{
			if (_currentSelectedTower != null)
			{
				_currentSelectedTower.SetSelected(false);
			}
			_currentSelectedTower = newTowerSelected;
			_currentSelectedTower.SetSelected(true);
			MoveBuildArrowsToTower();	
		}
	}

	void MoveBuildArrowsToTower()
	{
		_buildArrows.transform.position = new Vector3(_currentSelectedTower.transform.position.x, 0.01f, _currentSelectedTower.transform.position.z);
		_buildArrows.transform.SetParent(_currentSelectedTower.transform, true);
		_buildArrows.SetActive(true);
	}
	
	public void SetIsInBuildConfirm(bool isInBuildConfirm)
	{
		_isOnBuildConfirm = isInBuildConfirm;
	}

	public void SetIsMoving(bool isMoving)
	{
		_isMovingTower = isMoving;
	}

	public bool CanSelectSomething()
	{
		return !_isOnBuildConfirm;
	}

	void Update()
	{
		// if building, listen for clicks on the current building.
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
