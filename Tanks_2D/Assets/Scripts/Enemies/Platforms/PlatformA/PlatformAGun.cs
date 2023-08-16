using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformAGun : MonoBehaviour
{
    //Muzzle transforms
    public Transform muzzle;
    public Transform secondaryMuzzle;

    //Used to make projectile a child of entity
    //public Transform Entities;

    //Projectile properties
    public Projectile projectile;
    public float msBetweenShots = 500;
    public float muzzleVelocity = 5;

    private float nextShotTime = 0f;

    public void Shoot()
    {
        if (Time.time > nextShotTime)
        {
            nextShotTime = Time.time + msBetweenShots / 1000;
            Projectile newProjectile = Instantiate(projectile, muzzle.position, muzzle.rotation, this.gameObject.transform.parent.parent.parent) as Projectile;
            //newProjectile.SetSpeed(muzzleVelocity);
        }
    }

    public Transform GetMuzzle() { return muzzle; }
}
