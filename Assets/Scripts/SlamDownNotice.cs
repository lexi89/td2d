using System.Collections;
using TMPro;
using UnityEngine;

public class SlamDownNotice : MonoBehaviour {

	public TMP_Text Text;
	public bool AnimateOnAwake;
	[SerializeField] float _displayDuration;

	void Awake()
	{
		if (AnimateOnAwake)
		{
			SlamDown();
		}
	}
	
	public void SlamDown()
	{
		iTween.ScaleFrom (gameObject, iTween.Hash ("scale", new Vector3 (4f,4f,4f), "time", 0.2f, "oncomplete", "OnSlamDown", "easetype", iTween.EaseType.easeInCirc));
	}
	
	void OnSlamDown(){
		StartCoroutine (PauseAndShrink());
	}

	IEnumerator PauseAndShrink(){
		yield return new WaitForSeconds (_displayDuration);
		iTween.ScaleTo(gameObject, iTween.Hash ("scale", new Vector3 (0f,0f,0f), "time", 0.2f, "oncomplete", "OnShrunk", "easetype", iTween.EaseType.easeInCirc));
	}
	
	void OnShrunk()
	{
		Destroy(gameObject);
	}
}
