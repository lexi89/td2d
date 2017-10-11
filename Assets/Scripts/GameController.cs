using System.Collections;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour {

	public static GameController Instance;
	public Camera MainCamera;
	public Transform Castle;
	[SerializeField] Canvas UICanvas;
	[SerializeField] GameObject CBTprefab;
	[SerializeField] float CBTYoffset;
	[SerializeField] GameObject SlamDownNoticePrefab;
	[SerializeField] WaveManager _waveManager;

//	public Text waveCountUIText;
	public TMP_Text waveNoticeText;
	public int Difficulty{get { return _difficulty; }}
	int _difficulty;

	void Awake(){
		Instance = this;
	}

	void Start()
	{
		_difficulty = 0;
		StartCoroutine(NextWave());
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			StartCoroutine(NextWave());
		}
	}

	public void DisplayCBT(string text, Vector3 pos)
	{
		GameObject CBTGO = Instantiate(CBTprefab, UICanvas.transform);
		CBTGO.GetComponent<TMP_Text>().text = text;
		Vector3 screenPos = Camera.main.WorldToScreenPoint(pos);
		screenPos.y += CBTYoffset;
		CBTGO.transform.position = screenPos;
	}

	IEnumerator NextWave()
	{
		_difficulty++;
		GameObject newSlamNotice = Instantiate(SlamDownNoticePrefab, UICanvas.transform);
		SlamDownNotice sdn = newSlamNotice.GetComponent<SlamDownNotice>();
		sdn.Text.text = string.Concat ("Wave: ", _difficulty.ToString ());
		sdn.SlamDown();
		yield return new WaitForSeconds (2f);
		_waveManager.SpawnWave();		
	}

//	void SpawnCreep(){
//		if(numberOfCreepsSpawned < waves[waveCount].numberOfCreeps){			
//			Instantiate (CreepPrefab, SpawnPoint.position, Quaternion.Euler (0f,90f,0f));
//			numberOfCreepsSpawned++;
//		} else {
//			CancelInvoke ();
//		}
//	}
//
//	public void onCastleHit(){
//		numberOfCreepsKilled++;
//	}
//
//	public void onCreepKilled(){
//		numberOfCreepsKilled++;
//		if(numberOfCreepsKilled == numberOfCreepsToSpawn){
//			StartCoroutine ("WaveComplete");
//		}
//	}
//
//	IEnumerator WaveComplete(){
//		// show end of level rewards;
//		yield return new WaitForSeconds (2f);
//		nextWave ();
//	}

}
