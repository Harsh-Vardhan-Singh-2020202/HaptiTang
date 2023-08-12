using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRestriction : MonoBehaviour
{
    public bool restrictX;
    public bool restrictY;
    public bool restrictZ;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public float minZ;
    public float maxZ;

    Vector3 Position;

    void Update()
    {
        Position = transform.position;

        // Apply restriction for X-axis
        if (restrictX)
            Position.x = Mathf.Clamp(Position.x, minX, maxX);

        // Apply restriction for Y-axis
        if (restrictY)
            Position.y = Mathf.Clamp(Position.y, minY, maxY);

        // Apply restriction for Z-axis
        if (restrictZ)
            Position.z = Mathf.Clamp(Position.z, minZ, maxZ);

        // Update the local position of the object
        transform.position = Position;
    }
}