using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCollectedDrop : MonoBehaviour {

	void Start () {
		springOutInRandomDirection ();
	}

	void springOutInRandomDirection(){
		float randomX = UnityEngine.Random.Range (-100f, 100f);
		float randomY = UnityEngine.Random.Range (-100f, 100f);
		iTween.MoveAdd (gameObject, iTween.Hash ("y", randomY, "x", randomX, "time", 0.5f, "oncomplete", "GoToOrigin"));
		// and go up and down.
	}

	void GoToOrigin(){
		iTween.MoveTo (gameObject, iTween.Hash ("position", transform.parent.position, "time", 0.5f, "easetype", iTween.EaseType.easeOutBack));
	}
}
