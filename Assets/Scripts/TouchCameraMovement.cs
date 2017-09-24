
using UnityEngine;

public class TouchCameraMovement : MonoBehaviour
{
	[SerializeField] float _scrollSpeed;
	[SerializeField] float _fastSwipeThreshold;
	Vector3 oldPos;
	
	// Update is called once per frame
	void Update () {
//		if (Input.GetMouseButtonDown(0))
//		{
//			
//		}
//		if (TowerSelectController.instance.IsBuilding)
//			return;
//		#if UNITY_EDITOR
		if (Input.GetMouseButtonDown (0)){
//			print("mouse down!");
			oldPos = Input.mousePosition;
		} else if(Input.GetMouseButton (0)){
//			print("get mouse");
//			if (Input.mousePosition == oldPos) return;
			Vector3 posDifference = Input.mousePosition - oldPos;
			
			Vector3 AdjustedPosDifference = new Vector3(-posDifference.x * _scrollSpeed, -posDifference.y*_scrollSpeed, -posDifference.z*_scrollSpeed);
//			iTween.MoveBy(gameObject, iTween.Hash("x", -AdjustedPosDifference.x, "y", -AdjustedPosDifference.y, "z", -AdjustedPosDifference.z, "time", 1f, "easetype", iTween.EaseType.easeOutQuint));
			transform.Translate(AdjustedPosDifference);
//			transform.Translate(new Vector3());
//			transform.position = new Vector3 (transform.position.x, transform.position.y - (posDifference.y * scrollSpeed), transform.position.z - (posDifference.z * scrollSpeed));	
			oldPos = Input.mousePosition;
		}
//		#else
//		if(Input.touchCount > 0){
//			Touch touch = Input.GetTouch (0);
//			switch (touch.phase) {
//			case TouchPhase.Moved:
//				transform.position = new Vector3 (transform.position.x - (touch.deltaPosition.x * scrollSpeed), transform.position.y, transform.position.z);
//				
//				break;
//			default:
//				break;
//			}
//		}
//		#endif
	}
}

public delegate void SimpleEvent();