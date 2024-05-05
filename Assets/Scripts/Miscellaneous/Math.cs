using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Math
{
    public static float GetAngle(Vector2 direction)
    {
        return Mathf.Atan2(direction.x, direction.y);
    }
    public static float GetAngle(Vector3 direction)
    {
        return Mathf.Atan2(direction.x, direction.y);
    }
    public static float GetAngle(Quaternion direction)
    {
        return Mathf.Atan2(direction.eulerAngles.x, direction.eulerAngles.y); //* Mathf.Rad2Deg;
    }
    public static double Dot(Vector2 A, Vector2 B)
    {
        return A.x * B.x + A.y * B.y;
    }
    public static double Cross(Vector2 A, Vector2 B)
    {
        return A.x * B.y - A.y * B.x;
    }
    public static bool AreEqualAngles(float _A, float _B) //Has a precision of one decimal
    {
        if ((Mathf.Round(_A * 10.0f) / 10.0f - Mathf.Round(_B * 10.0f) / 10.0f) % 360 == 0) { return true; }
        return false;
    }
}
