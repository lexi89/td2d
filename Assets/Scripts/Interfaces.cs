using System.Collections;

public interface IKillable
{
	void Die();
}

public interface IDamageable
{
	void TakeDamage(float damageTaken);
}

public interface ICounter{
	void Add (int numberToAdd);
	void Remove (int numberToRemove);
	void Update ();
}