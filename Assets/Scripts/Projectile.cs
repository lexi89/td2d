using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public float speed;
	public float damage;
	public GameObject ExplosionPrefab;
	public bool _isFlying;
	Transform _target;
	Vector3 _targetOrginalPos;

	public void fire(Transform newTarget){
		_target = newTarget;
		_targetOrginalPos = newTarget.position;
		_isFlying = true;
	}

	void Update(){
		if(_isFlying){
			if(_target != null){
				transform.LookAt (_target.position);
				transform.position = Vector3.Lerp (transform.position, _target.position, speed);	
			} else {
				if(Vector3.Distance (transform.position, _targetOrginalPos) < 0.5f){
					Destroy (gameObject);
				} else{
					transform.position = Vector3.Lerp (transform.position, _targetOrginalPos, speed);	
				}
			}
		}
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag("Creep"))
		{
			other.gameObject.GetComponent<Creep>().TakeDamage(damage);
		}
		Destroy (gameObject);
	}

	void OnTriggerEnter(Collider col){
		_isFlying = false;
		if(col.tag == "Creep" && col.gameObject.activeSelf){
			if(ExplosionPrefab != null){
				GetComponent <MeshRenderer>().enabled = false;
				GetComponent <BoxCollider>().enabled = false;
				Instantiate (ExplosionPrefab, transform, false);
			} else{
				Destroy (gameObject);	
			}
			col.gameObject.GetComponent <Creep> ().TakeDamage (damage);
		}
	}
}
