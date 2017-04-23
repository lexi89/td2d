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
	GameObject towerSelected;
	[SerializeField]
	List<GameObject> towers;
	BuildLayerManager _buildLayer;

	void Awake(){
		instance = this;
		_buildLayer = BuildLayerManager.instance;
	}

	void Start(){
		foreach (var tower in towers) {
			GameObject newTowerButton = Instantiate (towerButtonPrefab) as GameObject;
			newTowerButton.transform.SetParent (menuContent.transform);
			newTowerButton.GetComponent <Image>().sprite = tower.GetComponent<Tower> ().menuImage;
			newTowerButton.GetComponent <TowerSelectButton> ().tower = tower;
		}
	}

	public void onTowerSelected(GameObject newTowerSelected){
		if(towerSelected != null){
			return;
		}
		GameObject newTower = Instantiate (newTowerSelected) as GameObject;
		towerSelected = newTower;
		moveTowerToMousePos ();
	}

	public void onTowerDeselected(){
		if(_buildLayer.canPlace (towerSelected.transform.position)){
			_buildLayer.placeTower (towerSelected.transform.position, towerSelected.transform);
			towerSelected.GetComponent <Tower>().place ();
		} else{
			Destroy (towerSelected);
		}
		towerSelected = null;

	}

	void Update(){
		if(towerSelected != null){
			if(Input.GetMouseButtonUp(0)){
				onTowerDeselected ();
				return;
			}
			moveTowerToMousePos ();
		}
	}

	void moveTowerToMousePos(){
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit info;

		if(Physics.Raycast (ray, out info, 100, LayerMask.GetMask ("Buildplaces"))){
			if (BuildLayerManager.instance.canPlace (info.collider.transform.position)){
				towerSelected.GetComponent <Tower>().canPlace ();
			} else{
				towerSelected.GetComponent <Tower>().cantPlace ();
			}
			towerSelected.transform.position = info.collider.transform.position;
		}
	}


}
