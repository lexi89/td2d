using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{
	public List<PoolSettings> ObjsToPool;

	void Start()
	{
		CreatePools();
	}

	void CreatePools()
	{
		for (int i = 0; i < ObjsToPool.Count; i++)
		{
			for (int j = 0; j < ObjsToPool[i].Poolsize; j++)
			{
				GameObject GO = Instantiate(ObjsToPool[i].Prefab, transform);
				GO.SetActive(false);
			}
		}
	}
}
