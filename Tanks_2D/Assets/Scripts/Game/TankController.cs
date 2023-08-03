using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour, IDamageable
{
    private Tower tower;
    public HealthManager healthManager;

    public float startingTowerRotation = 0;
    private float targetTowerRotation;
    public float angularVelocityTower;

    public float startingTrackRotation = 0;
    private float targetTrackRotation;
    public float angularVelocityTracks;
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;

    Rigidbody2D rb;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    public TankBuilder playerTankBuilder;

    public GameObject ExplosionObject;

    // Start is called before the first frame update
    void Start()
    {
        tower = playerTankBuilder.GetTower();
        rb = GetComponent<Rigidbody2D>();

        //Set rotation to starting rotation
        SetTowerRotation(startingTowerRotation);
        //tower.SetRotation(startingTowerRotation);
        targetTowerRotation = startingTowerRotation;
    }


    void FixedUpdate()
    {
        //Rotate towards target if tower rotatoin ins't equal to target rotation
        //if (!Math.AreEqualAngles(tower.GetCurrentRotation(), targetTowerRotation)) { tower.RotateTowards(targetTowerRotation, angularVelocityTower); }

        
        if (!Math.AreEqualAngles(GetTankRotation(), targetTrackRotation)) {
            RotateTowards(targetTrackRotation, angularVelocityTracks); 
        }
        //Debug.Log(GetTankRotation());
    }
    //public void SetTargetTowerRotation(float _target) { targetTowerRotation = _target; }
    
    //public void ShootGun() { tower.ShootGun(); }
    public void ShootGun() { tower.ShootGun(); }
    public void TakeHit(float damage) { healthManager.TakeHealth(damage); }
    public float GetTankRotation() { return rb.rotation; }
    public Vector3 GetTankPosition() { return rb.position; }
    public void StopRotation() { targetTrackRotation = GetTankRotation(); }
    public void SetTowerRotation(float _target) { tower.SetRotation(_target); }
    public void SetTargetTrackRotation(float _target) { targetTrackRotation = _target; }

    public bool TryMove(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            // Check for potential collisions
            int count = rb.Cast(
                direction, // X and Y values between -1 and 1 that represent the direction from the body to look for collisions
                movementFilter, // The settings that determine where a collision can occur on such as layers to collide with
                castCollisions, // List of collisions to store the found collisions into after the Cast is finished
                moveSpeed * Time.fixedDeltaTime + collisionOffset); // The amount to cast equal to the movement plus an offset

            if (count == 0)
            {
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            // Can't move if there's no direction to move in
            return false;
        }

    }



    private void RotateTowards(float _target, float _velocity)
    {
        //Debug.Log("target: " + _target.ToString());
        //Find angle between current rotation and target
        float angle = _target - GetTankRotation(); //Target - Current
                                                   //Then make standard [-180, 180]
        //Debug.Log(GetTankRotation());
        while (angle < -180) { angle += 360f; }
        while (angle > 180) { angle -= 360f; }
        //Debug.Log(angle);

        //Replace angle with max rotation allowed by the angular velocity parameter
        if (Mathf.Abs(angle) > _velocity * Time.fixedDeltaTime) { angle = _velocity * Time.fixedDeltaTime * Mathf.Sign(angle); }

        //Set the rotation
        SetRotation(GetTankRotation() + angle);
    }
    private void SetRotation(float _angle) { this.transform.rotation = Quaternion.AngleAxis(_angle, Vector3.forward); }

    public void Explode(Vector3 loc){

        //Spawn in explosion gameObject
		Instantiate(ExplosionObject, gameObject.transform.position, Quaternion.identity);

        Destroy(this.gameObject);
    }
}
