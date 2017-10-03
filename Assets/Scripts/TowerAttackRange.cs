using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttackRange : MonoBehaviour
{
	[SerializeField] Tower towerManager;
	
	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Creep"){
			towerManager.OnEnemyRangeEnter(other.transform);
		}
	}

	void OnTriggerExit(Collider other){
		if(other.gameObject.tag == "Creep"){
			towerManager.OnEnemyRangeLeave(other.transform);
		}
	}
}
