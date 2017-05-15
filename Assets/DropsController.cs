using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropsController : MonoBehaviour {

	public static DropsController instance;
	public Canvas UICanvas;
	public GameObject CoinPrefab;
	public Transform CoinCountUI;


	void Awake(){
		instance = this;
	}

	public void DropCoins(Vector3 startLocation, int numberOfCoins){
		StartCoroutine (DropCoinsRoutine (startLocation, numberOfCoins));
	}

	public IEnumerator DropCoinsRoutine(Vector3 startLocation, int numberOfCoins){
		for (int i = 0; i < numberOfCoins; i++) {
			GameObject newCoinDrop = Instantiate (CoinPrefab, CoinCountUI) as GameObject;
			newCoinDrop.transform.position = startLocation;
			yield return new WaitForSeconds (0.05f);
		}
	}
}
