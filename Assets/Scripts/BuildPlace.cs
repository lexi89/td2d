using System.Runtime.InteropServices;
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

public class BuildplaceData
{
    public bool IsEmpty;
    public Vector3 WorldPos;

    public BuildplaceData(bool _isEmpty, Vector3 _worldPos)
    {
        IsEmpty = _isEmpty;
        WorldPos = _worldPos;
        
    }
}
