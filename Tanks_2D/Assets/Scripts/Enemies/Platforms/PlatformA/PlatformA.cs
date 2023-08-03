using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformA : MonoBehaviour
{
    public PlatformControllerA platformController;

    public float directionRefreshRate = 0.25f;
    public float fireRefreshRate = 0.15f;

    public float randomDirectionRate = 0.2f;
    public float lookAtPlayerRate = 0.5f;
    public float misfireRate = 0.02f;
    public float fireWhileAimedRate = 0.8f;

    private bool lockedOnLastUpdate = false;

    public bool debugMode = false;

    void Awake()
    {
        platformController.SetDebugMode(debugMode);
    }

    void Start()
    {
        StartCoroutine(UpdateDirection());
        StartCoroutine(UpdateShootDecision());
    }

    IEnumerator UpdateDirection()
    {

        while (true) //Loop endlessly
        {

            //Evaluate if it should look at player
            if (platformController.hasPlayerView())
            {
                //Debug.Log("Has line of sight");
                platformController.LookAtPlayer();
                lockedOnLastUpdate = true;
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
            }

            //Wait for refresh rate
            yield return new WaitForSeconds(directionRefreshRate);
        }
    }
    IEnumerator UpdateShootDecision()
    {
        while (true) //Loop endlessly
        {
            //Evaluate if it should look in random direction
            if (lockedOnLastUpdate) { if(Random.Range(0f, 1f) < fireWhileAimedRate && !platformController.IsRotating()) { platformController.ShootGun(); } }

            //Evaluate if it should look in random direction
            if (Random.Range(0f, 1f) < misfireRate)
            {
                //Goto a random rotation
                //platformController.SetTargetRotation(Random.Range(0f, 360f));
                platformController.ShootGun();
            }

            //Wait for refresh rate
            yield return new WaitForSeconds(fireRefreshRate);
        }
    }
}
