using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Tower : MonoBehaviour, IPointerDownHandler, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler {

	public static int widthInUnits;
	public static int heightInUnits;
	[SerializeField] GameObject _highlight;
	[SerializeField] GameObject _buildConfirmUI;
	[SerializeField] LayerMask _buildpLayerMask;

	public GameObject attackProjectile;
	public Sprite menuImage;
	public Sprite level1;
	public Sprite level2;
	public Sprite level3;
	[Range(0f, 5f)]
	public float attackSpeedInSeconds;
	public float attackDamage;
	float nextAttackTime = 0.0f;

	Color originalColor;
	Material material;
	Transform currentTarget;
	List<Transform> potentialTargets;
	bool _isSelected;

	void Awake(){
		material = GetComponent <MeshRenderer> ().material;
		originalColor = material.color;
		potentialTargets = new List<Transform> ();
	}

	void OnEnable(){
//		GetComponent <NavMeshObstacle>().enabled = true;
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Creep"){
			potentialTargets.Add (other.transform);
		}
	}

	void OnTriggerExit(Collider other){
		potentialTargets.Remove (other.transform);
	}

	void Update(){
		if(potentialTargets.Count > 0){
			for (int i = 0; i < potentialTargets.Count; i++) {
				if(potentialTargets[i] != null){
					Attack (potentialTargets[i]);
				} else {
					// target died in the previous frame.
					potentialTargets.RemoveAt (i);
				}
			}
		}
	}
		
	public void Attack(Transform target){
		if(Time.time > nextAttackTime){
			nextAttackTime = Time.time + attackSpeedInSeconds;
			GameObject newProjectile = Instantiate (attackProjectile, transform.position, Quaternion.identity) as GameObject;
			newProjectile.transform.position = new Vector3 (transform.position.x, 1.5f, transform.position.z);
			newProjectile.GetComponent <Projectile>().fire (target);
		}
	}

	public void cantPlace(){
		material.color = Color.red;
	}

	public void canPlace(){
		material.color = originalColor;
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
		if (!_isSelected)
		{
			SetSelected(true);
		}
		else
		{
			
		}
		BuildLayerManager.instance.SetIsBuilding(false);
	}


	public void OnEndDrag(PointerEventData eventData)
	{
		BuildLayerManager.instance.SetIsBuilding(false);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		BuildLayerManager.instance.SetIsBuilding(true);
	}

	public void ShowBuildConfirmUI()
	{
		_buildConfirmUI.SetActive(true);
	}
}
