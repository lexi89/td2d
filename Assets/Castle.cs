using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour {

	void OnTriggerEnter(Collider other){
		print ("trigger enter");
		Destroy (other.gameObject);
		GameController.instance.onCastleHit ();
	}
}
