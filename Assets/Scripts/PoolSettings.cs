using UnityEngine;

[CreateAssetMenu(fileName = "PoolSetting", menuName = "Pooler/NewSetting", order = 1)] 
public class PoolSettings : ScriptableObject
{
	public GameObject Prefab;
	public int Poolsize;
	public bool CanGrow;
}
