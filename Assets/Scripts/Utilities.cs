using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities {

	public static Vector3 roundPos(Vector3 newVector){
		return new Vector3 (Mathf.Round (newVector.x), Mathf.Round (newVector.y), Mathf.Round (newVector.z));
	}

}
