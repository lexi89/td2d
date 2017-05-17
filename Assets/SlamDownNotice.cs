using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class SlamDownNotice : MonoBehaviour {

	Text text;
	[SerializeField] float displayDuration;

	void Awake(){
		text = GetComponent <Text> ();
	}

	void OnEnable(){
		text.enabled = true;
		iTween.ScaleFrom (gameObject, iTween.Hash ("scale", new Vector3 (4f,4f,4f), "time", 0.2f, "oncomplete", "OnDone", "easetype", iTween.EaseType.easeInCirc));
	}

	void OnDone(){
		StartCoroutine ("HangBeforeDestroying");
	}

	IEnumerator HangBeforeDestroying(){
		yield return new WaitForSeconds (displayDuration);
		text.enabled = false;
		enabled = false;
	}
}
