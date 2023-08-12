using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSteering : MonoBehaviour
{
    public Transform wheel;
    public Transform car;
    public float maxSteerAngle = 25f; // Maximum angle the car can steer in degrees
    public float rotationSpeedMultiplier = 2f; // Adjust this value to control the rotation speed

    // Update is called once per frame
    void Update()
    {
        // Get the current rotation angle of the wheel around the Z-axis
        float wheelRotationAngle = wheel.localRotation.eulerAngles.z;

        // Calculate the rotation speed based on the difference between the current angle and the center position (160 degrees)
        float rotationSpeed = (160f - wheelRotationAngle) * rotationSpeedMultiplier; // Invert the rotation speed

        // Limit the rotation speed within the maximum steer angle
        rotationSpeed = Mathf.Clamp(rotationSpeed, -maxSteerAngle, maxSteerAngle);

        // Apply the rotation speed to the car's rotation in the y-axis
        Vector3 carRotation = car.localRotation.eulerAngles;
        carRotation.y += rotationSpeed * Time.deltaTime;
        car.localRotation = Quaternion.Euler(carRotation);
    }
}