using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering_Magnetometer : MonoBehaviour
{
    public SensorDataReceiver sensorData;
    public float startValue = 25f; // Adjust this value as per your requirements
    public float endValue = 550f; // Adjust this value as per your requirements
    public float targetMinAngle = 0f; // The minimum angle in the custom range (e.g., 0 degrees)
    public float targetMaxAngle = 360; // The maximum angle in the custom range (e.g., 180 degrees)

    void Update()
    {
        // Get magnetometer data from SensorDataReceiver
        float magnetometerX = sensorData.magnetometerX;
        float magnetometerY = sensorData.magnetometerY;
        float magnetometerZ = sensorData.magnetometerZ;

        // Calculate the magnitude of magnetometer data
        float absMagnetometerValue = Mathf.Sqrt(magnetometerX * magnetometerX + magnetometerY * magnetometerY + magnetometerZ * magnetometerZ);

        // Map the magnetometer value from the range startValue to endValue to the custom range (targetMinAngle to targetMaxAngle)
        float mappedRotation = MapRange(absMagnetometerValue, startValue, endValue, targetMinAngle, targetMaxAngle);

        // Apply the rotation around the Z-axis
        transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, mappedRotation);
    }

    float MapRange(float value, float fromMin, float fromMax, float toMin, float toMax)
    {
        // Clamp the value within the range fromMin to fromMax
        value = Mathf.Clamp(value, fromMin, fromMax);

        // Map the value from the input range to the output range
        float mappedValue = (value - fromMin) / (fromMax - fromMin) * (toMax - toMin) + toMin;

        return mappedValue;
    }
}
