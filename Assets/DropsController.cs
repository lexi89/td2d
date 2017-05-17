using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropsController : MonoBehaviour {

	public static DropsController instance;
	public Canvas UICanvas;
	public GameObject CoinPrefab;
	public GameObject CoinCount;
	public int coinsPerCoin;


	void Awake(){
		instance = this;
	}

	void Start(){
		coinsPerCoin = 1;

	}

	public void DropCoins(Vector3 startLocation, int numberOfCoins){
		StartCoroutine (DropCoinsRoutine (startLocation, numberOfCoins));
	}

	public IEnumerator DropCoinsRoutine(Vector3 startLocation, int numberOfCoins){
		for (int i = 0; i < numberOfCoins; i++) {
			GameObject newCoinDrop = Instantiate (CoinPrefab, CoinCount.transform) as GameObject;
			newCoinDrop.transform.position = startLocation;
			newCoinDrop.GetComponentInParent <AutoCollectedDrop>().ThingToIncrement = CoinCount.GetComponent <ICounter>();
			newCoinDrop.GetComponentInParent <AutoCollectedDrop>().numberOfThingsToAdd = coinsPerCoin;
			yield return new WaitForSeconds (0.05f);
		}
	}
}
