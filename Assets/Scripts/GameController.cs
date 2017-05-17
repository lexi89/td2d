using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public static GameController instance;
	public Transform Castle;
	public Transform SpawnPoint;
	public GameObject CreepPrefab;
	public Text waveCountUIText;
	public Text waveNoticeText;
	public List<Wave> waves;
	int waveCount;
	int numberOfCreepsToSpawn;
	int numberOfCreepsSpawned;
	int numberOfCreepsKilled;

	void Awake(){
		instance = this;
	}

	void Start(){
		waveCount = -1;
		StartCoroutine ("nextWave");
	}

	void nextWave(){
		waveCount++;
		if(waveCount < waves.Count){
			StartCoroutine ("StartSpawningNextWave");
		} else{
			print ("game over");
		}
	}

	IEnumerator StartSpawningNextWave(){
		string waveCountString = string.Concat ("Wave: ", (waveCount + 1).ToString ());
		waveCountUIText.text = waveCountString;
		waveNoticeText.text = waveCountString;
		showWaveNotice ();
		yield return new WaitForSeconds (2f);
		numberOfCreepsToSpawn = waves [waveCount].numberOfCreeps;
		numberOfCreepsSpawned = 0;
		numberOfCreepsKilled = 0;
		InvokeRepeating ("SpawnCreep", 0f, waves[waveCount].delayBetweenSpawns);		
	}

	void SpawnCreep(){
		if(numberOfCreepsSpawned < waves[waveCount].numberOfCreeps){			
			Instantiate (CreepPrefab, SpawnPoint.position, Quaternion.Euler (0f,90f,0f));
			numberOfCreepsSpawned++;
		} else {
			CancelInvoke ();
		}
	}

	public void onCastleHit(){
		numberOfCreepsKilled++;
	}

	public void onCreepKilled(){
		numberOfCreepsKilled++;
		if(numberOfCreepsKilled == numberOfCreepsToSpawn){
			StartCoroutine ("WaveComplete");
		}
	}

	IEnumerator WaveComplete(){
		// show end of level rewards;
		yield return new WaitForSeconds (2f);
		nextWave ();
	}

	void showWaveNotice(){
		waveNoticeText.GetComponent <SlamDownNotice> ().enabled = true;
	}
}
