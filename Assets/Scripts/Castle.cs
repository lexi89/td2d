using UnityEngine;

public class Castle : MonoBehaviour
{

	[SerializeField] int _health;
	public CBTManager CBTManager;
	void OnTriggerEnter(Collider other){
		Destroy (other.gameObject);
	}

	

	public void TakeDamage(int damage)
	{
//		print("take damage!");
		if (_health > 0)
		{
			_health -= damage;
			CBTManager.ShowCombatText(damage.ToString());
			if (_health <= 0)
			{
				GameController.Instance.OnCastleDestroyed();
				gameObject.SetActive(false);
			}	
		}
	}
}
