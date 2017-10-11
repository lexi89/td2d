using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Creep : MonoBehaviour, IDamageable, IKillable {

	NavMeshAgent agent;
	public float health;
	public int WaveID;
	public GameObject DamageNoticePrefab;
	[SerializeField] Canvas _uiCanvas;

	public float minCoins;
	public float maxCoins;
	int _numberOfCoinsToDrop;

	void Awake(){
		agent = GetComponent <NavMeshAgent> ();
	}

	void Start(){
		agent.SetDestination (GameController.Instance.Castle.position);
		_numberOfCoinsToDrop = (int)Random.Range (minCoins, maxCoins);
	}

//	void OnDestroy(){
//		GameController.instance.onCreepKilled ();
//	}

	public void TakeDamage(float damageTaken){
		health -= damageTaken;
		if (health < 0){
			Die ();
		} else{
			GameController.Instance.DisplayCBT(damageTaken.ToString(), transform.position);
//			GameObject newNotice = Instantiate (DamageNoticePrefab, _uiCanvas.transform, false);
//			newNotice.GetComponent <EffectNotice>().showDamageTaken (damageTaken);
		}

	}
		
	public void Die(){
		//  play die animation.
		agent.enabled = false;
//		GameController.Instance.onCreepKilled ();
//		GetComponent <BoxCollider>().enabled = false;
//		GetComponent <MeshRenderer>().enabled = false;
//		DropsController.instance.DropCoins (Camera.main.WorldToScreenPoint (transform.position), _numberOfCoinsToDrop);
		Destroy (gameObject);
	}
		
}