using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackController : MonoBehaviour
{
    public float GetCurrentRotation()
    {
        return this.transform.rotation.eulerAngles.z;
    }
}
