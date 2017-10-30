using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextAppear : MonoBehaviour
{

	public TMP_Text Text;
	public float yOffset;

	void OnEnable()
	{
		transform.position = transform.position + new Vector3(0f, -yOffset, 0f);
		Appear();
	}

	public void Appear()
	{
		iTween.MoveBy(gameObject, iTween.Hash("y", yOffset, "time", 1f));
	}

	void OnDisable()
	{
		iTween.Stop(gameObject);
	}

	public void Disappear()
	{
		
	}

	void SetNewTextAlpha(float newAlpha)
	{
		
	}
}
