using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour {

	void OnTriggerEnter(Collider other){
		Destroy (other.gameObject);
		GameController.Instance.onCastleHit ();
	}
}
