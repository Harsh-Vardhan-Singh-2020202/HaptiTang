using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider_Magnetometer : MonoBehaviour
{
    public SensorDataReceiver sensorData;
    public float startValue = 25f; // Adjust this value as per your requirements
    public float endValue = 550f; // Adjust this value as per your requirements
    public Vector3 targetMinPos; // The minimum position in the custom range (e.g., new Vector3(0, 0, 0) for the minimum position)
    public Vector3 targetMaxPos; // The maximum position in the custom range (e.g., new Vector3(0, 0, 1) for the maximum position)

    void Update()
    {
        // Get magnetometer data from SensorDataReceiver
        float magnetometerX = sensorData.magnetometerX;
        float magnetometerY = sensorData.magnetometerY;
        float magnetometerZ = sensorData.magnetometerZ;

        // Calculate the magnitude of magnetometer data
        float absMagnetometerValue = Mathf.Sqrt(magnetometerX * magnetometerX + magnetometerY * magnetometerY + magnetometerZ * magnetometerZ);

        // Map the magnetometer value from the range startValue to endValue to the custom range (targetMinPos to targetMaxPos)
        Vector3 mappedPosition = MapRange(absMagnetometerValue, startValue, endValue, targetMinPos, targetMaxPos);

        // Apply the position to the object
        transform.position = mappedPosition;
    }

    Vector3 MapRange(float value, float fromMin, float fromMax, Vector3 toMin, Vector3 toMax)
    {
        // Clamp the value within the range fromMin to fromMax
        value = Mathf.Clamp(value, fromMin, fromMax);

        // Map the value from the input range to the output range
        float t = (value - fromMin) / (fromMax - fromMin);
        Vector3 mappedValue = Vector3.Lerp(toMin, toMax, t);

        return mappedValue;
    }
}