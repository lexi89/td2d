using System.Collections.Generic;
using NUnit.Framework.Interfaces;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Tower : MonoBehaviour, IPointerDownHandler, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler {

	public static int widthInUnits;
	public static int heightInUnits;
	[SerializeField] GameObject _highlight;
	[SerializeField] GameObject _buildConfirmUI;
	[SerializeField] LayerMask _buildpLayerMask;
	[SerializeField] GameObject attackProjectile;
	[SerializeField] Transform _turret;
	[SerializeField] SphereCollider _attackDetector;
	public Sprite menuImage;
	public Sprite level1;
	public Sprite level2;
	public Sprite level3;
	[Range(0f, 5f)]
	public float attackSpeedInSeconds;
	public float AttackDamage;
	float nextAttackTime;
	Transform currentTarget;
	List<Transform> potentialTargets;
	bool _isSelected;
	[SerializeField] bool _isBuilt;
	[SerializeField] Animator _anim;
	[SerializeField] GameObject _towerObj;
	[SerializeField] GameObject _turretBaseObj;
	[SerializeField] GameObject _turretObj;
	[SerializeField] Color _selectedBrightnessMax;
	Material _rendMat;
	Material _turretMat;
	Material _turretBaseObjMat;

	void Awake()
	{
		potentialTargets = new List<Transform> ();
		_rendMat = _towerObj.GetComponent<Renderer>().material;
		_turretMat = _turretObj.GetComponent<Renderer>().material;
		_turretBaseObjMat = _turretBaseObj.GetComponent<Renderer>().material;
	}

	public void SetGhostMode(bool isEnabled)
	{
		_attackDetector.enabled = !isEnabled;
		GetComponent<NavMeshObstacle>().enabled = !isEnabled;				
	}

	public void OnEnemyRangeEnter(Transform enemy)
	{
		if (!_isBuilt) return;
		potentialTargets.Add(enemy);
	}

	public void OnEnemyRangeLeave(Transform enemy)
	{
		if (!_isBuilt) return;
		potentialTargets.Remove(enemy);
	}

	void Update(){
		if (!_isBuilt || potentialTargets.Count <= 0) return;
		if (potentialTargets[0] != null)
		{
			iTween.LookUpdate(_turret.gameObject, potentialTargets[0].transform.position, 1f);		
		}
		for (int i = 0; i < potentialTargets.Count; i++) {
			if(potentialTargets[i] != null){
				Attack (potentialTargets[i]);
			} else {
				potentialTargets.RemoveAt (i);
			}
		}
	}
		
	public void Attack(Transform target){
		if (!(Time.time > nextAttackTime)) return;
		nextAttackTime = Time.time + attackSpeedInSeconds;
		GameObject newProjectile = Instantiate (attackProjectile, transform.position, Quaternion.identity);
		newProjectile.transform.LookAt(potentialTargets[0]);
		newProjectile.transform.position = new Vector3 (transform.position.x, 1f, transform.position.z);
		newProjectile.GetComponent <Projectile>().Damage = AttackDamage;
		newProjectile.GetComponent <Projectile>().fire (target);
	}

	public void SetSelected(bool isSelected)
	{
		if (isSelected)
		{
			_isSelected = true;
			_highlight.SetActive(true);
			_anim.SetTrigger("Selected");
			iTween.ValueTo(gameObject, iTween.Hash(
				"name", "pulsing",
				"from", Color.black, 
				"to", _selectedBrightnessMax, 
				"onupdate", "SetNewEmissionColor", 
				"looptype", iTween.LoopType.pingPong, 
				"time", 0.5f,
				"easetype", iTween.EaseType.linear));
		}
		else
		{
			_isSelected = false;
			_highlight.SetActive(false);
			iTween.StopByName(gameObject, "pulsing");
			SetNewEmissionColor(Color.black);
		}
	}

	void SetNewEmissionColor(Color newColor)
	{
		_rendMat.SetColor("_EmissionColor", newColor);
		_turretMat.SetColor("_EmissionColor", newColor);
		_turretBaseObjMat.SetColor("_EmissionColor", newColor);
	}
	
	public void OnBeginDrag(PointerEventData eventData)
	{
		if (_isSelected)
		{
			BuildLayerManager.instance.SetIsBuilding(true);
		}
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (!_isSelected) return;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		Physics.Raycast(ray, out hit, Mathf.Infinity, _buildpLayerMask);
		if (hit.collider == null) return;
		if (transform.position == hit.collider.transform.position) return;
		transform.position = hit.collider.transform.position;
		// ray onto the buildplaces.
	}
	
	public void OnPointerClick(PointerEventData eventData)
	{
		BuildLayerManager.instance.OnTowerSelected(this);		
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		BuildLayerManager.instance.SetIsBuilding(false);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
//		BuildLayerManager.instance.SetIsBuilding(true);
	}
    
	public void ShowBuildConfirmUI()
	{
		_buildConfirmUI.SetActive(true);
		BuildLayerManager.instance.SetIsBuilding(true);
	}

	public void OnBuildConfirm()
	{
		SetGhostMode(false);
		SetSelected(false);
		_buildConfirmUI.SetActive(false);
		_isBuilt = true;
		BuildLayerManager.instance.SetIsBuilding(false);
		BuildLayerManager.instance.OnBuildConfirm();
	}

	public void OnBuildCancel()
	{
		BuildLayerManager.instance.SetIsBuilding(false);
		Destroy(gameObject);
	}
}
