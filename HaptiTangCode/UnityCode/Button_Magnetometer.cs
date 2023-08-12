using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Magnetometer : MonoBehaviour
{
    public SensorDataReceiver sensorData;
    public ButtonInteract buttonInteract;
    
    public float threshold;

    private bool blasted;

    // Start is called before the first frame update
    void Start()
    {
        blasted = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Get magnetometer data from SensorDataReceiver
        float magnetometerX = sensorData.magnetometerX;
        float magnetometerY = sensorData.magnetometerY;
        float magnetometerZ = sensorData.magnetometerZ;

        // Calculate the magnitude of magnetometer data
        float absMagnetometerValue = Mathf.Sqrt(magnetometerX * magnetometerX + magnetometerY * magnetometerY + magnetometerZ * magnetometerZ);

        if (absMagnetometerValue >= threshold && !blasted)
        {
            blasted = true;
            buttonInteract.Interact();
        }
        else if (absMagnetometerValue < threshold && blasted )
        {
            blasted = false;
        }
    }
}
