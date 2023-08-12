using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationRestriction : MonoBehaviour
{
    public bool restrictX;
    public bool restrictY;
    public bool restrictZ;

    // Minimum allowed rotation in degrees (must be greater than 0)
    // Maximum allowed rotation in degrees (must be less than 360)
    public float minRotationX;
    public float maxRotationX;
    public float minRotationY;
    public float maxRotationY;
    public float minRotationZ;
    public float maxRotationZ;

    void Update()
    {
        Vector3 eulerRotation = transform.localEulerAngles;

        if (restrictX)
        {
            eulerRotation.x = Mathf.Clamp(eulerRotation.x, minRotationX, maxRotationX);
            if (eulerRotation.x < minRotationX)
                eulerRotation.x = minRotationX;
            if (eulerRotation.x > maxRotationX)
                eulerRotation.x = maxRotationX;
        }

        if (restrictY)
        {
            eulerRotation.y = Mathf.Clamp(eulerRotation.y, minRotationY, maxRotationY);
            if (eulerRotation.y < minRotationY)
                eulerRotation.y = minRotationY;
            if (eulerRotation.y > maxRotationY)
                eulerRotation.y = maxRotationY;
        }

        if (restrictZ)
        {
            eulerRotation.z = Mathf.Clamp(eulerRotation.z, minRotationZ, maxRotationZ);
            if (eulerRotation.z < minRotationZ)
                eulerRotation.z = minRotationZ;
            if (eulerRotation.z > maxRotationZ)
                eulerRotation.z = maxRotationZ;
        }

        transform.localEulerAngles = eulerRotation;
    }
}