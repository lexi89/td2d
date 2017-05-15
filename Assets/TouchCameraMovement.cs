using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchCameraMovement : MonoBehaviour {

	public float scrollSpeed;
	Vector3 oldPos;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (TowerSelectController.instance.IsBuilding)
			return;
		#if UNITY_EDITOR
		if (Input.GetMouseButtonDown (0)){
			oldPos = Input.mousePosition;
		} else if(Input.GetMouseButton (0)){
			Vector3 posDifference = Input.mousePosition - oldPos;
			transform.position = new Vector3 (transform.position.x - (posDifference.x * scrollSpeed), transform.position.y, transform.position.z);	
			oldPos = Input.mousePosition;
		}
		#else
		if(Input.touchCount > 0){
			Touch touch = Input.GetTouch (0);
			switch (touch.phase) {
			case TouchPhase.Moved:
				transform.position = new Vector3 (transform.position.x - (touch.deltaPosition.x * scrollSpeed), transform.position.y, transform.position.z);
				
				break;
			default:
				break;
			}
		}
		#endif
	}
}
