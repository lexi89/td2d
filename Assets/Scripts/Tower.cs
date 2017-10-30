using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Tower : MonoBehaviour, IPointerDownHandler, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
	public int Level;
	public int Level1Cost;
	public int Level2Cost;
	public int Level3Cost;
	[SerializeField] GameObject _highlight;
	[SerializeField] GameObject _buildConfirmUI;
	[SerializeField] LayerMask _buildpLayerMask;
	[SerializeField] GameObject attackProjectile;
	[SerializeField] Transform _turret;
	[SerializeField] SphereCollider _attackDetector;
	[SerializeField] bool _isBuilt;
	[SerializeField] Animator _anim;
	[SerializeField] GameObject _towerObj;
	[SerializeField] GameObject _turretBaseObj;
	[SerializeField] GameObject _turretObj;
	[SerializeField] Color _selectedBrightnessMax;
	[SerializeField] TMP_Text TowerName;
	[SerializeField] TMP_Text TowerLevel;
	[Range(0f, 5f)]
	public float attackSpeedInSeconds;
	public float AttackDamage;
	float nextAttackTime;
	Transform currentTarget;
	List<Transform> potentialTargets;
	bool _isSelected;
	Material _rendMat;
	Material _turretMat;
	Material _turretBaseObjMat;
	bool _isInGhostMode;
	
	
	void Awake()
	{
		potentialTargets = new List<Transform> ();
		_rendMat = _towerObj.GetComponent<Renderer>().material;
		_turretMat = _turretObj.GetComponent<Renderer>().material;
		_turretBaseObjMat = _turretBaseObj.GetComponent<Renderer>().material;
	}

	public void SetGhostMode(bool isEnabled)
	{
		_isSelected = true;
		_isInGhostMode = isEnabled;
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
		if (target.GetComponent<Creep>().CanBeAttacked)
		{
			nextAttackTime = Time.time + attackSpeedInSeconds;
			GameObject newProjectile = Instantiate (attackProjectile, transform.position, Quaternion.identity);
			newProjectile.transform.LookAt(potentialTargets[0]);
			newProjectile.transform.position = new Vector3 (transform.position.x, 1f, transform.position.z);
			newProjectile.GetComponent <Projectile>().Damage = AttackDamage;
			newProjectile.GetComponent <Projectile>().Fire (target);	
		}
	}

	public void SetSelected(bool isSelected)
	{
		if (_isInGhostMode) return;
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
			SetTowerInfoActive(true);
		}
		else
		{
			_isSelected = false;
			_highlight.SetActive(false);
			iTween.StopByName(gameObject, "pulsing");
			SetNewEmissionColor(Color.black);
			SetTowerInfoActive(false);
		}
	}

	void SetNewEmissionColor(Color newColor)
	{
		_rendMat.SetColor("_EmissionColor", newColor);
		_turretMat.SetColor("_EmissionColor", newColor);
		_turretBaseObjMat.SetColor("_EmissionColor", newColor);
	}

	void SetTowerInfoActive(bool isActive)
	{
		if (isActive)
		{
			TowerLevel.text = string.Concat("Level ", Level.ToString());
			TowerName.text = name;	
		}
		TowerLevel.gameObject.SetActive(isActive);
		TowerName.gameObject.SetActive(isActive);
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		SetSelected(true);
		BuildLayerManager.instance.SetIsMoving(true);
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (!_isSelected) return;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		Physics.Raycast(ray, out hit, Mathf.Infinity, _buildpLayerMask);
		if (hit.collider == null) return;
		if (transform.position == hit.collider.transform.position) return;
		if (Grid.instance.CanBuildAt(hit.collider.transform.position))
		{
			transform.position = hit.collider.transform.position;
		}
		else
		{
			print("cannot build here.");
		}
		
	}
	
	public void OnPointerClick(PointerEventData eventData)
	{
		if (!_isSelected)
		{
			BuildLayerManager.instance.OnTowerSelected(this);	
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
//		if (!_isSelected)
//		{
//			BuildLayerManager.instance.SetIsBuilding(false);		
//		}
		BuildLayerManager.instance.SetIsMoving(false);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
//		BuildLayerManager.instance.SetIsBuilding(true);
	}
    
	public void ShowBuildConfirmUI()
	{
		_buildConfirmUI.SetActive(true);
		BuildLayerManager.instance.SetIsInBuildConfirm(true);
	}

	public void OnBuildConfirm()
	{
		SetGhostMode(false);
		SetSelected(false);
		_buildConfirmUI.SetActive(false);
		_isBuilt = true;
		Grid.instance.BuildAt(transform.position);
		BuildLayerManager.instance.SetIsInBuildConfirm(false);
		BuildLayerManager.instance.OnBuildConfirm(this);
	}

	public void OnBuildCancel()
	{
		BuildLayerManager.instance.SetIsInBuildConfirm(false);
		Destroy(gameObject);
	}
}
