using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBuilder : MonoBehaviour
{
    public GameObject parent;

    public TrackController TrackA;
    public TrackController TrackB;
    public TrackController TrackC;
    public TrackController TrackD;
    private TrackController[] trackList;

    public Hull SmallHullA;
    public Hull SmallHullB;
    public Hull SmallHullC;
    public Hull MediumHullA;
    public Hull MediumHullB;
    public Hull MediumHullC;
    public Hull LargeHullA;
    public Hull LargeHullB;
    public Hull LargeHullC;
    private Hull[] hullList;

    public Tower SmallTowerA;
    public Tower SmallTowerB;
    public Tower SmallTowerC;
    public Tower MediumTowerA;
    public Tower MediumTowerB;
    public Tower MediumTowerC;
    public Tower LargeTowerA;
    public Tower LargeTowerB;
    public Tower LargeTowerC;
    private Tower[] towerList;

    public Gun SmallGunA;
    public Gun SmallGunB;
    public Gun SmallGunC;
    public Gun MediumGunA;
    public Gun MediumGunB;
    public Gun MediumGunC;
    public Gun LargeGunA;
    public Gun LargeGunB;
    public Gun LargeGunC;
    private Gun[] gunList;

    public Projectile LightShellA;
    private Projectile[] projectileList;

    private TrackController tracks;
    private Hull hull;
    private Tower tower;
    private Gun gun;

    private Projectile projectile;

    private Transform towerMount;
    private Transform gunMount;

    void Awake()
    {
        ReBuild();
    }

    //Instantiates prefabs into tank
    public void ReBuild()
    {
        //Clear any existing prefabs
        Clear();

        //Create arrays of prefabs to be indexed
        trackList = new TrackController[] { TrackA, TrackB, TrackC, TrackD };
        hullList = new Hull[] { SmallHullA, SmallHullB, SmallHullC, MediumHullA, MediumHullB, MediumHullC, LargeHullA, LargeHullB, LargeHullC };
        towerList = new Tower[] { SmallTowerA, SmallTowerB, SmallTowerC, MediumTowerA, MediumTowerB, MediumTowerC, LargeTowerA, LargeTowerB, LargeTowerC };
        gunList = new Gun[] { SmallGunA, SmallGunB, SmallGunC, MediumGunA, MediumGunB, MediumGunC, LargeGunA, LargeGunB, LargeGunC };
        projectileList = new Projectile[] { LightShellA };

        //Instantiate the tracks
        tracks = Instantiate(trackList[SavedData.GetTracks()], parent.transform) as TrackController;

        //Instantiate the hull
        hull = Instantiate(hullList[SavedData.GetHull()], parent.transform) as Hull;

        //Instantiate the tower
        towerMount = hull.GetTowerMount();
        tower = Instantiate(towerList[SavedData.GetTower()], towerMount.position, tracks.transform.rotation, parent.transform) as Tower;

        //Instantiate the gun
        gunMount = tower.GetGunMount();
        gun = Instantiate(gunList[SavedData.GetGun()], gunMount.position, tracks.transform.rotation, tower.transform) as Gun;
        
        //tower.gun = gun;
        tower.SetGun(gun);

        projectile = projectileList[0]; //TODO implement real projectile indexes from saveddata
        gun.SetProjectile(projectile);
    }

    public Gun GetGun() { return gun; }
    public Tower GetTower() { return tower; }

    public void Clear()
    {
        foreach(Transform child in parent.transform)
        {
            Destroy(child.gameObject);
        }

        if (tracks != null) { Destroy(tracks); }
        if (hull != null) { Destroy(hull); }
        if (tower != null) { Destroy(tower); }
        if (gun != null) { Destroy(gun); }
    }
}
