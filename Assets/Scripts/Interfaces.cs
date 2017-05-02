using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interfaces : MonoBehaviour {

}

public interface IKillable
{
	void Die();
}

public interface IDamageable
{
	void Damage(float damageTaken);
}
