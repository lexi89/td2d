using UnityEngine;

public class Projectile : MonoBehaviour {

	public float SecondsToTarget;
	public float Damage;
//	public GameObject ExplosionPrefab;
	public bool _isFlying;
	Transform _target;
	Vector3 _targetOrginalPos;
	

	public void fire(Transform newTarget)
	{
		
		_target = newTarget;
		_targetOrginalPos = newTarget.position;
		_isFlying = true;
	}

	void Update(){
		if(_isFlying){
			if (_target != null)
			{
				transform.LookAt(_target.position);
				iTween.MoveUpdate(gameObject, _target.position, SecondsToTarget);
//				transform.position = Vector3.Lerp (transform.position, _target.position, speed);	
			}
			else
			{
				// fly to the target original pos and self destruct
//				gameObject.AddComponent<Rigidbody>();
				transform.LookAt(_targetOrginalPos);
				iTween.MoveUpdate(gameObject, _targetOrginalPos, SecondsToTarget);
				if (Vector3.Distance(transform.position, _targetOrginalPos) < 1f)
				{
					Destroy(gameObject);
				}
			}
		}
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag("Creep"))
		{
			other.gameObject.GetComponent<Creep>().TakeDamage(Damage);
		}
		Destroy (gameObject);
	}

	void OnTriggerEnter(Collider col){
		_isFlying = false;
		if(col.tag == "Creep" && col.gameObject.activeSelf){
//			if(ExplosionPrefab != null){
//				GetComponent <MeshRenderer>().enabled = false;
//				GetComponent <BoxCollider>().enabled = false;
//				Instantiate (ExplosionPrefab, transform, false);
//			} else{
//					
//			}
			Destroy (gameObject);
			col.gameObject.GetComponent <Creep> ().TakeDamage (Damage);
		}
	}
}
