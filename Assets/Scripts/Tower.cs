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
	public float attackDamage;
	float nextAttackTime;
	Transform currentTarget;
	List<Transform> potentialTargets;
	bool _isSelected;
	bool _isBuilt;	

	void Awake()
	{
		_attackDetector.enabled = false;
		potentialTargets = new List<Transform> ();
	}

	void OnEnable(){
//		GetComponent <NavMeshObstacle>().enabled = true;
	}

	public void OnEnemyRangeEnter(Transform enemy)
	{
		if (_isBuilt)
		{
			potentialTargets.Add(enemy);	
		}
	}

	public void OnEnemyRangeLeave(Transform enemy)
	{
		if (_isBuilt)
		{
			potentialTargets.Remove(enemy);	
		}
	}

	void Update(){
		if (!_isBuilt) return;
		if (potentialTargets.Count <= 0) return;
		if (potentialTargets[0] != null)
		{
			iTween.LookUpdate(_turret.gameObject, potentialTargets[0].transform.position, 1f);		
		}
//			iTween.LookTo(_turret.gameObject, potentialTargets[0].transform.position, 0.5f);
//			_turret.LookAt(potentialTargets[0]);
		for (int i = 0; i < potentialTargets.Count; i++) {
			if(potentialTargets[i] != null){
				Attack (potentialTargets[i]);
			} else {
				// target died in the previous frame.
				potentialTargets.RemoveAt (i);
			}
		}
	}
		
	public void Attack(Transform target){
		if(Time.time > nextAttackTime){
			nextAttackTime = Time.time + attackSpeedInSeconds;
			// TODO: make the projectiles look at the target...
			GameObject newProjectile = Instantiate (attackProjectile, transform.position, Quaternion.identity);
			newProjectile.transform.LookAt(potentialTargets[0]);
//			newProjectile.transform.LookAt();
			newProjectile.transform.position = new Vector3 (transform.position.x, 1f, transform.position.z);
			newProjectile.GetComponent <Projectile>().fire (target);
		}
	}

	public void SetSelected(bool isSelected)
	{
		if (isSelected)
		{
			_isSelected = true;
			_highlight.SetActive(true);
			// animate a little.
		}
		else
		{
			_isSelected = false;
			_highlight.SetActive(false);
		}
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
		if (_isSelected)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			Physics.Raycast(ray, out hit, Mathf.Infinity, _buildpLayerMask);
			if (hit.collider != null)
			{
				if (transform.position == hit.collider.transform.position) return;
				transform.position = hit.collider.transform.position;
			}
			// ray onto the buildplaces.
		}
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
		_attackDetector.enabled = true;
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
