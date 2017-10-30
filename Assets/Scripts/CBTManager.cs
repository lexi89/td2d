using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class CBTManager : MonoBehaviour
{
	public GameObject CBTprefab;
	public Transform CombatTextPoint;
	public int PoolSize;
	List<GameObject> CombatTextPool;
	TMP_Text _combatText;

	void Awake()
	{
		CombatTextPool = new List<GameObject>();
		for (int i = 0; i < PoolSize; i++)
		{
			GameObject newCBT = Instantiate(CBTprefab, transform, false);
			newCBT.SetActive(false);
			newCBT.GetComponent<CombatText>().SetStartingPos(CombatTextPoint);
			CombatTextPool.Add(newCBT);
		}
	}

	GameObject GetCBT()
	{
		for (int i = 0; i < CombatTextPool.Count; i++)
		{
			if (!CombatTextPool[i].activeSelf)
			{
				return CombatTextPool[i];
			}
		}
		print("no cbt found");
		return null;
	}
	
	public void ShowCombatText(string text)
	{
		GameObject newCBT = GetCBT();
		if (newCBT == null) return;
		newCBT.GetComponent<TMP_Text>().text = text;
		newCBT.SetActive(true);
		newCBT.GetComponent<CombatText>().Animate();
	}
}
