using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformControllerA : MonoBehaviour, IDamageable
{
    public PlatformATower tower;

    public Transform playerTransform;
    public HealthManager healthManager;

    public float startingRotation = 180;
    public float angularVelocity = 50;
    private float targetRotation;
    private bool isRotating;

    //public LayerMask collisionMask;
    private int collisionLayer1 = 7;
    private int collisionLayerMask;

    private bool debugMode = false;

    public GameObject ExplosionObject;

    public void SetDebugMode(bool _mode) { debugMode = _mode; }

    void Awake()
    {

        //Setup ignore layer mask
        collisionLayerMask = 1 << collisionLayer1;

        targetRotation = startingRotation;
        isRotating = false;
    }

    void Start()
    {
        //Set rotation to starting rotation
        tower.SetRotation(startingRotation);
    }


    void Update()
    {
        //Rotate towards target if tower rotatoin ins't equal to target rotation
        if (!Math.AreEqualAngles(tower.GetCurrentRotation(), targetRotation)) { 
            tower.RotateTowards(targetRotation, angularVelocity);
            isRotating = true;
        } else { isRotating = false; }
    }

    public void TakeHit(float damage)
    {
        healthManager.TakeHealth(damage);
    }

    public void SetTargetRotation(float _target) {
        targetRotation = _target; 
    }

    public void ShootGun()
    {
        tower.ShootGun();
    }
    public bool hasPlayerView()
    {
        if (debugMode) { Debug.DrawLine(this.transform.position, playerTransform.position, Color.green, 0.25f); }
        return !Physics2D.Linecast(this.transform.position, playerTransform.position, collisionLayerMask);
    }
    public bool hasPointView(Vector3 _position)
    {
        if (debugMode) { Debug.DrawLine(this.transform.position, _position, Color.green, 0.25f); }
        return !Physics2D.Linecast(this.transform.position, _position, collisionLayerMask);
    }
    public void LookAtPlayer()
    {
        Vector3 dir = playerTransform.position - this.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * 180 / Mathf.PI;
        SetTargetRotation(angle);
    }
    public void LookAtLoc(Vector3 _location)
    {
        Vector3 dir = _location - this.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * 180 / Mathf.PI;
        SetTargetRotation(angle);
    }
    public bool IsRotating() { return isRotating; }

    public void Explode(Vector3 loc){

        //Spawn in explosion gameObject
		Instantiate(ExplosionObject, gameObject.transform.position, Quaternion.identity);

        Destroy(this.gameObject);
    }

    public Vector3 GetPlayerLocation()
    {
        return playerTransform.position;
    }

    public float DistanceToPlayer()
    {
        //Get player coordinates
        Vector3 playerLoc = playerTransform.position;

        //Get difference in coordinates
        Vector2 distance;
        distance.x = Mathf.Abs(gameObject.transform.position.x - playerLoc.x);
        distance.y = Mathf.Abs(gameObject.transform.position.y - playerLoc.y);

        return Mathf.Sqrt(Mathf.Pow(distance.x, 2) + Mathf.Pow(distance.y, 2));
      
    }

    public float GetPlayerRotation()
    {
        return playerTransform.eulerAngles.z;
    }
}
