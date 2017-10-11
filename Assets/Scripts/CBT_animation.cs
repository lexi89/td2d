using TMPro;
using UnityEngine;

public class CBT_animation : MonoBehaviour
{

	[SerializeField] float _animationDuration;
	[SerializeField] TMP_Text text;
	
	void OnEnable()
	{
		iTween.ValueTo(gameObject, iTween.Hash("from", 1f, "to", 0f, "time", _animationDuration, "onupdate", "SetTextAlpha"));
		iTween.MoveBy(gameObject, iTween.Hash("y", 20f, "time", _animationDuration, "oncomplete", "SelfDestruct"));
	}

	public void SetTextAlpha(float newAlpha)
	{
		text.color = new Color(text.color.r, text.color.g, text.color.b, newAlpha);
	}

	public void SelfDestruct()
	{
		Destroy(gameObject);
	}
}
