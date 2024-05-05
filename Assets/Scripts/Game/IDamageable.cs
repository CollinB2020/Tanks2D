using UnityEngine;

public interface IDamageable
{

	void TakeHit(float damage);
	void Explode(Vector3 loc);

}