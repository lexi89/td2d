using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class test : MonoBehaviour
{

	Data[,] _data;

	void Start()
	{
		CreateGrid();
	}

	void CreateGrid()
	{
		_data = new Data[10,10];
		for (int i = 0; i < 10; i++)
		{
			for (int j = 0; j < 10; j++)
			{
				_data[i,j] = new Data(new Vector3(i,j,0f));
			}
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawCube(Vector3.zero, Vector3.one * 0.8f);
//		foreach (var d in _data)
//		{
//			Gizmos.color = d.isTrue ? Color.green : Color.red;
//			Gizmos.DrawCube(d.worldPos, Vector3.one * 0.5f);
//		}
	}
}

public class Data
{
	public bool isTrue;
	public Vector3 worldPos;

	public Data(Vector3 _newWorldPos)
	{
		worldPos = _newWorldPos;
	}
}
