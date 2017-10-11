using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.LookRotation(transform.position - GameController.Instance.MainCamera.transform.position);
		transform.LookAt(GameController.Instance.MainCamera.transform.position);
	}
}
