
using UnityEngine;

public class TouchCameraMovement : MonoBehaviour
{
	[SerializeField] float _scrollSpeed;
	[SerializeField] float _fastSwipeThreshold;
	Vector3 oldPos;
	
	void LateUpdate ()
	{
		if (BuildLayerManager.instance.IsBuilding)
		{
			return;
		}
		if (Input.GetMouseButtonDown (0)){
			
			oldPos = Input.mousePosition;
		} else if(Input.GetMouseButton (0)){
			Vector3 posDifference = Input.mousePosition - oldPos;
			
			Vector3 AdjustedPosDifference = new Vector3(-posDifference.x * _scrollSpeed, -posDifference.y*_scrollSpeed, -posDifference.z*_scrollSpeed);
			transform.Translate(AdjustedPosDifference);	
			oldPos = Input.mousePosition;
		}
	}
}

public delegate void SimpleEvent();