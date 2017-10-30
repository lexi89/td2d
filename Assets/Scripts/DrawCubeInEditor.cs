#if UNITY_EDITOR
using UnityEngine;

public class DrawCubeInEditor : MonoBehaviour
{
	public bool Draw;
	void OnDrawGizmos()
	{
		if (Draw)
		{
			Gizmos.color = Color.blue;
			Gizmos.DrawCube(transform.position, Vector3.one * 0.5f);	
		}
	}
}

#endif
