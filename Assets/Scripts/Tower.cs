using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Tower : MonoBehaviour {

	public static int widthInUnits;
	public static int heightInUnits;

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
			Attack ();
		}
	}
		
	public void Attack(){
		if(Time.time > nextAttackTime){
			nextAttackTime = Time.time + attackSpeedInSeconds;
			print ("attack!");
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
