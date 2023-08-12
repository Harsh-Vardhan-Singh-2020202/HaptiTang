using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMovement : MonoBehaviour
{
    public Transform wheel; // Reference to the SteeringWheel script attached to the wheel GameObject
    public float doorXPositionA = 0f; // The X position of the door when the wheel is at angle A
    public float doorXPositionB = 3f; // The X position of the door when the wheel is at angle B
    public float minAngle = 0f; // Minimum allowed rotation angle of the wheel in degrees
    public float maxAngle = 360f; // Maximum allowed rotation angle of the wheel in degrees

    void Update()
    {
        // Get the current rotation angle of the wheel around the Z-axis
        float wheelRotationAngle = wheel.localRotation.eulerAngles.z;

        // Map the wheel rotation angle (minAngle to maxAngle) to the desired X position of the door (doorXPositionA to doorXPositionB)
        float normalizedAngle = Mathf.InverseLerp(minAngle, maxAngle, wheelRotationAngle);
        float doorXPosition = Mathf.Lerp(doorXPositionA, doorXPositionB, normalizedAngle);

        // Update the door's position in the X-axis
        Vector3 newPosition = transform.localPosition;
        newPosition.x = doorXPosition;
        transform.localPosition = newPosition;
    }
}
