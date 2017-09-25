using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    [SerializeField] GameObject _buildMenuBtn;
    [SerializeField] GameObject _buildMenu;

    public void OnBuildMenuBtnPressed()
    {
        ToggleBuildMenu();
    }

    public void OnBuildingSelected(GameObject towerPrefab)
    {
        BuildLayerManager.instance.Build(towerPrefab);
        ToggleBuildMenu();
    }

    void ToggleBuildMenu()
    {
        _buildMenuBtn.SetActive(!_buildMenuBtn.activeSelf);
        _buildMenu.SetActive(!_buildMenu.activeSelf);
    }
}
