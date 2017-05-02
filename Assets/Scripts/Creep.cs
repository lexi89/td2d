using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Creep : MonoBehaviour, IDamageable, IKillable {

	NavMeshAgent agent;
	public float health;

	void Awake(){
		agent = GetComponent <NavMeshAgent> ();
		agent.SetDestination (GameController.instance.Castle.position);
	}

	void OnDestroy(){
		GameController.instance.onCreepKilled ();
	}

	public void Damage(float damageTaken){
		health -= damageTaken;
		if (health < 0){
			Die ();
		}
	}

	public void Die(){
		Destroy (gameObject);
	}
		
}