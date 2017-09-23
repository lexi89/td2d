using UnityEngine;
using UnityEngine.EventSystems;


public class BuildPlace : MonoBehaviour, IPointerDownHandler {
    public bool CanBuild{get { return _canBuildHere; }}
    [SerializeField] bool _canBuildHere;
    [SerializeField] BuildLayerManager _buildLayerManager;


    public void OnPointerDown(PointerEventData eventData)
    {
        if (_canBuildHere)
        {
            _buildLayerManager.OnBuildPlaceClicked(transform.position);    
        }
    }
}
