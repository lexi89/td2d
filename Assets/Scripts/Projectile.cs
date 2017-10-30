using UnityEngine;

public class Projectile : MonoBehaviour {

	public float SecondsToTarget;
	public float Damage;
//	public GameObject ExplosionPrefab;
	public bool _isFlying;
	Transform _target;
	Vector3 _targetOrginalPos;
	

	public void Fire(Transform newTarget)
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
				iTween.MoveUpdate(gameObject, new Vector3(_target.position.x, _target.position.y + 1f, _target.position.z), SecondsToTarget);
//				transform.position = Vector3.Lerp (transform.position, _target.position, speed);	
			}
			else
			{
				// fly to the target original pos and self destruct
//				gameObject.AddComponent<Rigidbody>();
				transform.LookAt(_targetOrginalPos);
				iTween.MoveUpdate(gameObject, new Vector3(_targetOrginalPos.x, _targetOrginalPos.y + 1f, _targetOrginalPos.z), SecondsToTarget);
				if (Vector3.Distance(transform.position, _targetOrginalPos) < 1f)
				{
					Destroy(gameObject);
				}
			}
		}
	}

	void OnCollisionEnter(Collision other)
	{
//		print("on collision enter");
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
