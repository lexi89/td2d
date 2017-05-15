using System.Collections;

public interface IKillable
{
	void Die();
}

public interface IDamageable
{
	void TakeDamage(float damageTaken);
}
