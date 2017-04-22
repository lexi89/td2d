using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerSelectButton : MonoBehaviour, IPointerDownHandler {

	public GameObject tower;

	public void OnPointerDown(PointerEventData eventData){
		TowerSelectController.instance.onTowerSelected (tower);
	}

//	public void OnPointerUp(PointerEventData eventData){
//		TowerSelectController.instance.onTowerDeselected ();
//	}
}
