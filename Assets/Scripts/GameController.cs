using System.Collections;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour {

	public static GameController Instance;
	public event SimpleEvent OnCoinChange;
	public Camera MainCamera;
	public Transform Castle;
	public int CoinCount{get { return _coinCount; }}
	[SerializeField] Canvas UICanvas;
	[SerializeField] GameObject CBTprefab;
	[SerializeField] float CBTYoffset;
	[SerializeField] TMP_Text _coinText;
	[SerializeField] WaveManager _waveManager;
	public TMP_Text waveNoticeText;
	public int Difficulty{get { return _difficulty; }}
	public GameObject GameOverNotice;
	int _difficulty;
	[SerializeField]int _coinCount;

	void Awake(){
		Instance = this;
	}

	void Start()
	{
		_difficulty = 0;
//		StartCoroutine(_waveManager.NextWave());
	}

	public void DisplayCBT(string text, Vector3 pos)
	{
//		GameObject CBTGO = Instantiate(CBTprefab, UICanvas.transform);
//		CBTGO.GetComponent<TMP_Text>().text = text;
//		Vector3 screenPos = Camera.main.WorldToScreenPoint(pos);
//		screenPos.y += CBTYoffset;
//		CBTGO.transform.position = screenPos;
	}

	public void IncrementDifficulty()
	{
		_difficulty++;
	}

	public void SpendCoins(int coinsSpent)
	{
		_coinCount -= coinsSpent;
		if (OnCoinChange != null)
		{
			OnCoinChange();
		}
	}

	public void OnCastleDestroyed()
	{
		Instantiate(GameOverNotice, UICanvas.transform);
		
	}

}
