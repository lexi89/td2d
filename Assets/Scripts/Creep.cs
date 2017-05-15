using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Creep : MonoBehaviour, IDamageable, IKillable {

	NavMeshAgent agent;
	public float health;
	public GameObject DamageNoticePrefab;
	public Canvas NoticeCanvas;

	public float minCoins;
	public float maxCoins;
	int _numberOfCoinsToDrop;

	void Awake(){
		agent = GetComponent <NavMeshAgent> ();
	}

	void Start(){
		agent.SetDestination (GameController.instance.Castle.position);
		_numberOfCoinsToDrop = (int)UnityEngine.Random.Range (minCoins, maxCoins);
	}

//	void OnDestroy(){
//		GameController.instance.onCreepKilled ();
//	}

	public void TakeDamage(float damageTaken){
		health -= damageTaken;
		if (health < 0){
			Die ();
		} else{
			GameObject newNotice = Instantiate (DamageNoticePrefab, NoticeCanvas.transform, false) as GameObject;
			newNotice.GetComponent <EffectNotice>().showDamageTaken (damageTaken);
		}

	}
		
	public void Die(){
		//  play die animation.

		DropsController.instance.DropCoins (Camera.main.WorldToScreenPoint (transform.position), _numberOfCoinsToDrop);
		agent.enabled = false;
		GetComponent <BoxCollider>().enabled = false;
		GetComponent <MeshRenderer>().enabled = false;
		Destroy (gameObject);
	}
		
}