using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Stores saved data like player preference in one class
public static class SavedData
{
    // Start is called before the first frame update
    public static void Initialize()
    {
        //Check for saved skull data
        if (!PlayerPrefs.HasKey("skulls")) PlayerPrefs.SetInt("skulls", 0);

        //Check for saved tracks data
        if (!PlayerPrefs.HasKey("tracks")) PlayerPrefs.SetInt("tracks", 0);

        //Check for saved hull data
        if (!PlayerPrefs.HasKey("hull")) PlayerPrefs.SetInt("hull", 0);

        //Check for saved tower data
        if (!PlayerPrefs.HasKey("tower")) PlayerPrefs.SetInt("tower", 0);

        //Check for saved gun data
        if (!PlayerPrefs.HasKey("gun")) PlayerPrefs.SetInt("gun", 0);

        //Check for saved Projectile data
        if (!PlayerPrefs.HasKey("projectile")) PlayerPrefs.SetInt("projectile", 3);
    }

    public static int GetSkulls() { return PlayerPrefs.GetInt("skulls"); }
    public static void AddSkulls(int num) { PlayerPrefs.SetInt("skulls", SavedData.GetSkulls() + num); }
    public static void RemoveSkulls(int num) { PlayerPrefs.SetInt("skulls", SavedData.GetSkulls() - num); }

    public static void SetTracks(int _tracks) //0-3 
    {
        if (_tracks < 0 || _tracks > 3) return;
        PlayerPrefs.SetInt("tracks", _tracks);
    }
    public static int GetTracks() { return PlayerPrefs.GetInt("tracks"); }

    public static void SetHull(int _hull) //0-8 small-large
    {
        if (_hull < 0 || _hull > 8) return;
        PlayerPrefs.SetInt("hull", _hull);
    }
    public static int GetHull() { return PlayerPrefs.GetInt("hull"); }

    public static void SetTower(int _tower) //0-8 small-large
    {
        if (_tower < 0 || _tower > 8) return;
        PlayerPrefs.SetInt("tower", _tower);
    }
    public static int GetTower() { return PlayerPrefs.GetInt("tower");  }

    public static void SetGun(int _gun)//0-8 small-large
    {
        if (_gun < 0 || _gun > 8) { return; }
        PlayerPrefs.SetInt("gun", _gun);
    }
    public static int GetGun() { return PlayerPrefs.GetInt("gun"); }

    public static void SetProjectile(int _projectile) //0-7
    {
        if (_projectile < 0 || _projectile > 7) { return; }
        PlayerPrefs.SetInt("projectile", _projectile);
    }
    public static int GetProjectile() { return PlayerPrefs.GetInt("projectile"); }
}