using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public Transform gunMount;
    private Gun gun;

    public Transform GetGunMount() { return gunMount; }

    public float GetCurrentRotation() { return this.transform.rotation.eulerAngles.z + 90; }

    public void SetRotation(float _angle) { this.transform.rotation = Quaternion.AngleAxis(_angle - 90, Vector3.forward); }

    //Rotates towards target angle with some velocity
    public void RotateTowards(float _target, float _velocity)
    {
        //Find angle between current rotation and target
        float angle = _target - GetCurrentRotation(); //Target - Current
                                                            //Then make standard (-180, 180)
        while (angle <= -180) { angle += 360f; }
        while (angle >= 180) { angle -= 360f; }

        //Replace angle with max rotation allowed by the angular velocity parameter
        if (Mathf.Abs(angle) > _velocity * Time.deltaTime) { angle = _velocity * Time.deltaTime * Mathf.Sign(angle); }

        //Set the rotation
        SetRotation(GetCurrentRotation() + angle);
    }

    public void ShootGun()
    {
        gun.Shoot();
    }

    public void SetGun(Gun _gun)
    {
        gun = _gun;
    }

}
