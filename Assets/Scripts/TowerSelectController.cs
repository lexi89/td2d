using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TowerSelectController : MonoBehaviour {

	public static TowerSelectController instance;

	public HorizontalLayoutGroup menuContent;
	public GameObject towerButtonPrefab;
	public Plane ground;
	public float YOffset;
//	public bool IsBuilding{
//		get{ return _isBuilding;}
//	}
//	bool _isBuilding;
	GameObject towerSelected;
	[SerializeField]
	List<GameObject> towers;
	BuildLayerManager _buildLayer;
//	
//	void Awake(){
//		instance = this;
//		_buildLayer = BuildLayerManager.instance;
//	}
//
//	void Start(){
//		foreach (var tower in towers) {
//			GameObject newTowerButton = Instantiate (towerButtonPrefab) as GameObject;
//			newTowerButton.transform.SetParent (menuContent.transform);
//			newTowerButton.GetComponent <Image>().sprite = tower.GetComponent<Tower> ().menuImage;
//			newTowerButton.GetComponent <TowerSelectButton> ().tower = tower;
//		}
//	}
//
//	public void onTowerSelected(GameObject newTowerSelected){
//		ToggleMovingMode (true);
//		if(towerSelected != null){
//			return;
//		}
//		_isBuilding = true;
//		GameObject newTower = Instantiate (newTowerSelected) as GameObject;
//		towerSelected = newTower;
//		moveTowerToMousePos ();
//	}
//
//	public void onTowerDeselected(){
//		_isBuilding = false;
//		ToggleMovingMode (false);
//		if(_buildLayer.canPlace (towerSelected.transform.position)){
//			_buildLayer.placeTower (towerSelected.transform.position, towerSelected.transform);
//			towerSelected.GetComponent <EnableOnPlace>().OnPlace ();
//		} else{
//			Destroy (towerSelected);
//		}
//		towerSelected = null;
//
//	}
//
//	void Update(){
//		if(towerSelected != null){
//			if(Input.GetMouseButtonUp(0)){
//				onTowerDeselected ();
//				return;
//			}
//			moveTowerToMousePos ();
//		}
//	}
//
//	void moveTowerToMousePos(){
//		Vector3 offSetMousePos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y + YOffset, Input.mousePosition.z);
//		Ray ray = Camera.main.ScreenPointToRay (offSetMousePos);
//		RaycastHit buildPlaceHitInfo;
//		RaycastHit groundHitInfo;
//		if(Physics.Raycast (ray, out buildPlaceHitInfo, 100, LayerMask.GetMask ("Buildplaces"))){
//			if (BuildLayerManager.instance.canPlace (buildPlaceHitInfo.collider.transform.position)){
//				towerSelected.GetComponent <Tower>().canPlace ();
//			} else{
//				towerSelected.GetComponent <Tower>().cantPlace ();
//			}
//			towerSelected.transform.position = buildPlaceHitInfo.collider.transform.position;
//		} else{
//			if(Physics.Raycast (ray, out groundHitInfo, 100, LayerMask.GetMask ("Ground"))){
//				towerSelected.transform.position = groundHitInfo.point;
//			}
//		}
//	}
//
//	void ToggleMovingMode(bool isOn){
//        
//		// hide the tower menu and show delete zone
//	}


}
