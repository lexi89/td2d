using System.Collections;
using TMPro;
using UnityEngine;

public class CombatText : MonoBehaviour
{
	[SerializeField] float _animationDuration;
	[SerializeField] TMP_Text text;
	Vector3 startingPos;
	Transform _startingPos;

	void Awake()
	{
		ResetPosition();
	}

	public void Animate()
	{
		iTween.ValueTo(gameObject, iTween.Hash("from", 1f, "to", 0f, "time", _animationDuration, "onupdate", "SetTextAlpha"));
		iTween.MoveBy(gameObject, iTween.Hash("y", 0.5f, "time", _animationDuration, "oncomplete", "Deactivate"));
	}

	void OnEnable()
	{
		ResetPosition();
	}

	public void SetStartingPos(Transform startingPos)
	{
		_startingPos = startingPos;
	}

	void ResetPosition()
	{
		if (_startingPos != null)
		{
			transform.position = _startingPos.position;	
		}
	}

	public void SetTextAlpha(float newAlpha)
	{
		text.color = new Color(text.color.r, text.color.g, text.color.b, newAlpha);
	}

	public void Deactivate()
	{
		StartCoroutine(DeactivateDelayed());
	}

	IEnumerator DeactivateDelayed()
	{
		yield return new WaitForSeconds(0.1f);
		gameObject.SetActive(false);
		
	}
}
