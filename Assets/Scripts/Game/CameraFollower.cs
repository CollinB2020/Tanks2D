using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    //Get transform of the player
    public Transform playerTransform;

    void FixedUpdate()
    {
        //Move this gameObject to player transform position every physics update
        this.transform.position = new Vector3(playerTransform.transform.position.x, playerTransform.transform.position.y, this.transform.position.z);
    }
}
