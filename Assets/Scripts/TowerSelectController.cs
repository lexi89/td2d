using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TowerSelectController : MonoBehaviour {

	public static TowerSelectController instance;

	public HorizontalLayoutGroup menuContent;
	public GameObject towerButtonPrefab;
	public Transform playArea;
	GameObject towerSelected;
	[SerializeField]
	List<GameObject> towers;

	void Awake(){
		instance = this;
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
		print ("pointer up!");
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
		RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, LayerMask.GetMask ("buildplaces"));
		if(hit.collider != null){
			towerSelected.transform.position = hit.collider.transform.position;
		}
	}

}
