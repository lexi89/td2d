using UnityEngine;
using UnityEngine.UI;

public class EffectNotice : MonoBehaviour {

	[SerializeField]float animationDuration;
	Text text;

	void Awake(){
		text = GetComponent <Text> ();
	}

	public void showDamageTaken(float damageTaken){
		text.text = Mathf.RoundToInt (damageTaken).ToString ();
		iTween.ScaleFrom (gameObject, iTween.Hash ("scale", new Vector3 (0f,0f,0f),"time", animationDuration, "oncomplete", "OnComplete", "easetype", iTween.EaseType.easeOutBack));
		iTween.MoveAdd (gameObject, new Vector3(0f,1f, 0f), animationDuration);
	}

	void OnComplete(){
		text.enabled = false;
	}
}