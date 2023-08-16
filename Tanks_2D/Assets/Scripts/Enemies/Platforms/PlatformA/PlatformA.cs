using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformA : MonoBehaviour
{

    /*                           PlatformA - outdated summary
     * This script basically has two main updates. UpdateDirection and UpdateShootDecision.
     * The purpose of UpdateShootDecision is to decide whether or not the fire the gun.
     * Update direction controls which direction the tower is rotating in. It
     * invokes TargetLocation() to see if a location is visible
     */

    //reference the platforms controller and the tanks controller
    public PlatformControllerA platformController;
    public TankController playerTankController;

    public float directionRefreshTime = 0.25f;
    //public float fireRefreshRate = 0.15f;

    //Different update rates
    public float randomDirectionRate = 0f;
    public float misfireRate = 0.02f;
    public float fireWhileAimedRate = 0.8f;
    public int aimSampleRate = 5;

    //TODO: Get references for projectile instead of hard code
    public float leadTargetAmount = 1.0f;
    public float projectileSpeed = 6.0f;
    private float tankSpeed;


    //Function UpdateDirection();
    private Vector3 playerLoc;
    private Vector3 vectorToTarget;

    //For TargetLocation()
    private bool lockedOnLastUpdate = false;

    //Debug Mode for drawing lines and logging :)
    public bool debugMode = false;

    void Awake()
    {
        //Apply the debug mode to the platform controller as well
        platformController.SetDebugMode(debugMode);
    }

    void Start()
    {
        //Get the tanks speed from the tank controller
        tankSpeed = playerTankController.moveSpeed;

        StartCoroutine(UpdateDirection());
        //StartCoroutine(UpdateShootDecision());
    }

    IEnumerator UpdateDirection()
    {

        while (true) //Loop endlessly
        {

            //Get players location
            playerLoc = platformController.GetPlayerLocation();

            //Get a 2d direction vector of the player in radians
            Vector2 directionVector = Vector2.zero;
            directionVector.Set(Mathf.Cos((Mathf.PI / 180.0f) * platformController.GetPlayerRotation()), Mathf.Sin((Mathf.PI / 180.0f) * platformController.GetPlayerRotation()));

            //Calculate the vector from the tank to its predicted location
            vectorToTarget.x = (platformController.DistanceToPlayer() / projectileSpeed) * (playerTankController.IsMoving() ? tankSpeed : 0.0f) * directionVector.x * leadTargetAmount;
            vectorToTarget.y = (platformController.DistanceToPlayer() / projectileSpeed) * (playerTankController.IsMoving() ? tankSpeed : 0.0f) * directionVector.y * leadTargetAmount;

            //Draw line if in debug mode where tank will be
            if(debugMode) Debug.DrawLine(platformController.GetPlayerLocation(), platformController.GetPlayerLocation() + vectorToTarget, Color.red, 0.25f);

            //Loop through sample points until one works
            for(int i = 0; i < aimSampleRate; i++)
            {
                //Trying targeting a point 
                if(TargetLocation(playerLoc + (vectorToTarget / (aimSampleRate - 1) * i))) break;

            }

            //Check if the tank should shoot
            UpdateShootDecision();

            //Wait for refresh rate
            yield return new WaitForSeconds(directionRefreshTime);
        }
    }

    private bool TargetLocation(Vector2 loc)
    {

        if(debugMode) Debug.Log("Trying to target point " + loc.ToString());

        //Evaluate if it has a line of sight
        if (platformController.hasPointView(loc))
        {
            //platformController.LookAtPlayer();
            platformController.LookAtLoc(loc);
            lockedOnLastUpdate = true;

            return true;
        }
        else
        {

            //Evaluate if it should look in random direction
            if (Random.Range(0f, 1f) < randomDirectionRate)
            {
                //Goto a random rotation
                platformController.SetTargetRotation(Random.Range(0f, 360f));

            }
            lockedOnLastUpdate = false;

            return false;
        }
    }

    private void UpdateShootDecision()
    {
        //Evaluate if it should shoot in the direction it is pointed
        if (lockedOnLastUpdate) { if(Random.Range(0f, 1f) < fireWhileAimedRate && !platformController.IsRotating()) { platformController.ShootGun(); } }

        //Evaluate if it should misfire
        if (Random.Range(0f, 1f) < misfireRate)
        {
            platformController.ShootGun();
        }
    }

    /*IEnumerator UpdateShootDecision()
    {
        while (true) //Loop endlessly
        {
            //Evaluate if it should shoot in the direction it is pointed
            if (lockedOnLastUpdate) { if(Random.Range(0f, 1f) < fireWhileAimedRate && !platformController.IsRotating()) { platformController.ShootGun(); } }

            //Evaluate if it should misfire
            if (Random.Range(0f, 1f) < misfireRate)
            {
                platformController.ShootGun();
            }

            //Wait for refresh rate
            yield return new WaitForSeconds(fireRefreshRate);
        }
    }*/
}
