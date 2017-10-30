using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{

	[SerializeField] TMP_Text _coinCountText;
	int _coinCountDisplayed;
	
	void Start ()
	{
		GameController.Instance.OnCoinChange += UpdateCoins;
		UpdateCoins();
	}

	void UpdateCoins()
	{
		StartCoroutine(CountToNewAmount());
	}

	IEnumerator CountToNewAmount()
	{
		iTween.PunchScale(transform.parent.gameObject, iTween.Hash("x", 0.2f, "y", 0.2f, "time", 1f));
		for (int i = 0; i < GameController.Instance.CoinCount - _coinCountDisplayed; i++)
		{
			_coinCountDisplayed++;
			_coinCountText.text = _coinCountDisplayed.ToString();
			yield return new WaitForSeconds(0.01f);
		}
		
	}
}
