using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Creep : MonoBehaviour, IDamageable
{
	public WaveManager WaveManager;
	public int WaveID;
	public bool CanBeAttacked{get{return health > 0;}}
	public float health;
	[SerializeField] Animator anim;
	[SerializeField] float _damageTimeInAnimation;
	[SerializeField] float _attackAnimTotalTime;
	[SerializeField] int _attackDamage;
	[SerializeField] float _timeBetweenAttacks;
	[SerializeField] BoxCollider _boxCollider;
	public CBTManager CbtManager;
	NavMeshAgent agent;
	bool _canAttack;
	float _lastAttackTime;

	void Awake(){
		agent = GetComponent <NavMeshAgent> ();
	}

	void Start(){
		agent.SetDestination (GameController.Instance.Castle.position);
		anim.SetBool("Run", true);
	}

	IEnumerator AttackTheCastle()
	{
		agent.speed = 0f;
		anim.SetBool("Run", false);
		while (GameController.Instance.Castle.gameObject.activeSelf && _canAttack)
		{
			anim.SetTrigger("Melee Right Attack 01");
			yield return new WaitForSeconds(_damageTimeInAnimation);
			if (_canAttack)
			{
				GameController.Instance.Castle.GetComponent<Castle>().TakeDamage(_attackDamage);	
			}
			yield return new WaitForSeconds(_attackAnimTotalTime - _damageTimeInAnimation + _timeBetweenAttacks);
		}
	}
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.A))
		{
			TakeDamage(10);
		}
	}

	public void TakeDamage(float damageTaken){
		health -= damageTaken;
		if (health < 0)
		{
			StartCoroutine(Die());
		} else{
			anim.SetBool("Run", false);
			anim.SetTrigger("Take Damage");
//			anim.SetBool("Run", true);
//			GameController.Instance.DisplayCBT(damageTaken.ToString(), transform.position);
			CbtManager.ShowCombatText(damageTaken.ToString());
		}
	}
		
	IEnumerator Die(){
		//  play die animation.
		_canAttack = false;
		agent.enabled = false;
		WaveManager.OnCreepDeath(this);
		_boxCollider.enabled = false;
//		yield return new WaitForEndOfFrame();
		anim.SetTrigger("Die");
		yield return new WaitForSeconds(2f);
//		GameController.Instance.onCreepKilled ();

//		GetComponent <MeshRenderer>().enabled = false;
//		DropsController.instance.DropCoins (Camera.main.WorldToScreenPoint (transform.position), _numberOfCoinsToDrop);
		Destroy (gameObject);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Castle"))
		{
			_canAttack = true;
			StartCoroutine(AttackTheCastle());
		}
	}
}