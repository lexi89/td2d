using UnityEngine;
using UnityEngine.EventSystems;

public class BuildMenuBtn : MonoBehaviour,IPointerClickHandler
{

	[SerializeField] UIManager _uiManager;
	[SerializeField] GameObject _towerPrefab;
	public void OnPointerClick(PointerEventData eventData)
	{
		_uiManager.OnBuildingSelected(_towerPrefab);
	}
}
