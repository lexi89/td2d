using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildLayerManager : MonoBehaviour {

	public GameObject BuildPlacePrefab;
	public Transform PlayArea;
	public int widthInUnits;
	public int heightInUnits;

	void Start(){
		for (int x = -(widthInUnits/2); x < (widthInUnits/2); x++) {
			for (int y = -(heightInUnits / 2); y < (heightInUnits/2); y++) {
				GameObject newBuildplace = Instantiate (BuildPlacePrefab) as GameObject;
				newBuildplace.transform.SetParent (PlayArea);
				newBuildplace.transform.position = new Vector2 ((float)x, (float)y);
			}
		}
	}
}
