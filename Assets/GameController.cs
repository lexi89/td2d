using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public static GameController instance;
	public Transform Castle;
	public Transform SpawnPoint;
	public GameObject CreepPrefab;
	public Text waveCountText;
	public List<Wave> waves;
	int waveCount;
	int numberOfCreepsSpawned;
	int numberOfCreepsKilled;

	void Awake(){
		instance = this;
	}

	void Start(){
		waveCount = -1;
		StartCoroutine ("nextWave");
	}

	IEnumerator nextWave(){
		waveCount++;
		setWaveText ();
		yield return new WaitForSeconds (2f);
		numberOfCreepsSpawned = 0;
		numberOfCreepsKilled = 0;
		InvokeRepeating ("SpawnCreep", 0f, waves[waveCount].delayBetweenSpawns);
	}

	void SpawnCreep(){
		if(numberOfCreepsSpawned < waves[waveCount].numberOfCreeps){			
			Instantiate (CreepPrefab, SpawnPoint.position, Quaternion.identity);
			numberOfCreepsSpawned++;
		} else {
			CancelInvoke ();
		}
	}

	public void onCastleHit(){
		numberOfCreepsKilled++;
	}

	public void onCreepKilled(){
		if(numberOfCreepsKilled == numberOfCreepsSpawned){
			StartCoroutine ("nextWave");
		}
	}

	void setWaveText(){
		waveCountText.text = "Wave: " + waveCount;

	}
}
