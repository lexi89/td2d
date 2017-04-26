using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Tower : MonoBehaviour {

	public static int widthInUnits;
	public static int heightInUnits;

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

	void Awake(){
		material = GetComponent <MeshRenderer> ().material;
		originalColor = material.color;
		potentialTargets = new List<Transform> ();
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
			print ("shooting a target at " + target.position);
			newProjectile.GetComponent <Projectile>().fire (target);
		}
	}

	public void place(){
		GetComponent <NavMeshObstacle>().enabled = true;
	}

	public void cantPlace(){
		material.color = Color.red;
	}

	public void canPlace(){
		material.color = originalColor;
	}
	
}
