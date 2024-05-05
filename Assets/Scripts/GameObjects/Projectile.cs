using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IDamageable
{
	//public LayerMask collisionMask;
	private int collisionLayer1 = 7;
	private int collisionLayer2 = 8;
	private int collisionMask;

	public float speed = 10;
	public float damage = 10;
	public int numBounces = 1;

	public float collisionOffset = 0.05f;

	public Transform RaycastPoint;

	public GameObject ExplosionObject;

	void Awake()
    {
		collisionMask = 1 << collisionLayer1;
		collisionMask |= (1 << collisionLayer2);

		ExplosionObject = (GameObject)Resources.Load("Prefabs/GameObjects/Explosion");
	}

	/*public void SetSpeed(float newSpeed)
	{
		speed = newSpeed;
	}*/

	void Update()
	{
		float moveDistance = speed * Time.deltaTime;
		CheckCollisions(moveDistance);
		transform.Translate(Vector3.up * moveDistance);
	}


	void CheckCollisions(float moveDistance)
	{
		//Ray ray = new Ray(transform.position, transform.forward);
		RaycastHit2D hit = Physics2D.Raycast(RaycastPoint.position, this.transform.up, moveDistance + collisionOffset, collisionMask);
		//Debug.DrawLine(this.transform.position, this.transform.position + new Vector3(this.transform.up.x, this.transform.up.y, 0) * (moveDistance + collisionOffset));

		if (hit.collider != null)
		{
			//OnHitObject(hit);
			//Debug.Log("Collision");
			IDamageable damageableObject = hit.collider.GetComponent<IDamageable>();
			if (damageableObject != null)
			{
				//Deal damage to damageable
				damageableObject.TakeHit(damage);

				this.Explode(hit.point);
			}
			else //Not IDamageable, so bounce
			{
				if (numBounces > 0)
				{
					//Bounce the projectile
					Vector2 direction = new Vector2(this.transform.up.x, this.transform.up.y);
					direction = Vector2.Reflect(direction, hit.normal);
					this.transform.up = new Vector3(direction.x, direction.y, 0);

					//Take away a bounce
					//Debug.Log("Bounce: " + numBounces.ToString()+" bounce location: "+this.transform.position.ToString()+" new dir: "+direction.ToString()+" normal:   "+hit.normal.ToString());
					numBounces--;
				}
				else
				{
					this.Explode(hit.point);
				}
			}
		}
	}

	public void TakeHit(float damage)
    {
		Destroy(this.gameObject);
    }

	public void Explode(Vector3 loc){

		//Spawn in explosion gameObject
		Instantiate(ExplosionObject, loc, Quaternion.identity);

		//Destroy self
		Destroy(this.gameObject);
	}
}
