using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public float speed;
	public float damage;
	bool _isFlying;
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

	void OnCollisionEnter(Collision other){
		if(other.collider.tag == "Creep" && other.collider.gameObject.activeSelf){
			other.collider.gameObject.GetComponent <Creep> ().Damage (damage);
		}
		Destroy (gameObject);
	}
}
