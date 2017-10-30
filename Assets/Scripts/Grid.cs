using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

	public static Grid instance;
	public int GridWidth;
	public int GridHeight;
	BuildplaceData[,] _grid;
	public bool ShowGrid;
	public Color BuildableColor;
	public Color NotBuildableColor;

	void Start()
	{
		instance = this;
		CreateGrid();
	}

	void CreateGrid()
	{
		_grid = new BuildplaceData[GridWidth,GridHeight];
		for (int z = 0; z < GridWidth; z++)
		{
			for (int x = 0; x < GridHeight; x++)
			{
				_grid[z,x] = new BuildplaceData(true, new Vector3(x, 1f, -z));
			}
		}
	}

	void OnDrawGizmos()
	{
		if (_grid != null && ShowGrid)
		{
			foreach (var block in _grid)
			{
				if (block != null)
				{
					Gizmos.color = block.IsEmpty ? BuildableColor : NotBuildableColor;	
					Gizmos.DrawCube(block.WorldPos, Vector3.one * 0.9f);
				}
			}
		}
	}
	
	public void BuildAt(Vector3 buildPos)
	{
		_grid[(int)Mathf.Abs(buildPos.z), (int)buildPos.x].IsEmpty = false;
	}

	public bool CanBuildAt(Vector3 buildPos)
	{
		return _grid[(int) Mathf.Abs(buildPos.z), (int) buildPos.x].IsEmpty;
	}
	
	
}
