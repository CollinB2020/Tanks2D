using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //Muzzle transforms
    public Transform muzzle;
    public Transform secondaryMuzzle;
    
    //Used to make projectile a child of entity
    //public Transform Entities;

    //Projectile properties
    private Projectile projectile;
    public float msBetweenShots = 100;
    public float muzzleVelocity = 5;

    float nextShotTime;

    public void Shoot()
    {
        //Debug.Log("Shoot");
        if (Time.time > nextShotTime)
        {
            nextShotTime = Time.time + msBetweenShots / 1000;
            Projectile newProjectile = Instantiate(projectile, muzzle.position, muzzle.rotation, this.gameObject.transform.parent.parent.parent.parent) as Projectile;
            //newProjectile.SetSpeed(muzzleVelocity);
        }
    }

    public void SetProjectile(Projectile _projectile)
    {
        projectile = _projectile;
    }

    public Transform GetMuzzle() { return muzzle; }
}
