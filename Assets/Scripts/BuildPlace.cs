using UnityEngine;
using UnityEngine.EventSystems;

public class BuildPlace : MonoBehaviour, IPointerClickHandler{
    public bool CanBuild{get { return _canBuildHere; }}
    [SerializeField] bool _canBuildHere;

    public void OnPointerClick(PointerEventData eventData)
    {
        BuildLayerManager.instance.OnEmptyBuildPlaceClicked();
    }
}
