using UnityEngine;
using UnityEngine.EventSystems;


public class BuildPlace : MonoBehaviour, IPointerClickHandler{
    public bool CanBuild{get { return _canBuildHere; }}
    [SerializeField] bool _canBuildHere;
//    [SerializeField] BuildLayerManager _buildLayerManager;
    [SerializeField] Material _highlightMaterial;
    [SerializeField] Material _groundMaterial;

    public void OnPointerClick(PointerEventData eventData)
    {
        BuildLayerManager.instance.OnEmptyBuildPlaceClicked();
    }

    public void SetHighlightActive(bool _isActive)
    {
        GetComponent<MeshRenderer>().material = _isActive ? _highlightMaterial : _groundMaterial;
    }
}
