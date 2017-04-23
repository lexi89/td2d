using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Creep : MonoBehaviour {

	NavMeshAgent agent;

	void Awake(){
		agent = GetComponent <NavMeshAgent> ();
		agent.SetDestination (GameController.instance.Castle.position);
	}

	void OnDestroy(){
		GameController.instance.onCreepKilled ();
	}
}
