using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShotStones_Magnetometer : MonoBehaviour
{

    public SensorDataReceiver sensorData;
    public float startValue = 100; // Adjust this value as per your requirements
    public float endValue = 600; // Adjust this value as per your requirements
    public Vector3 targetMinPos; // The minimum position in the custom range (e.g., new Vector3(0, 0, 0) for the minimum position)
    public Vector3 targetMaxPos; // The maximum position in the custom range (e.g., new Vector3(0, 0, 1) for the maximum position)

    public Transform slingPullbackPoint;
    public Transform drawFrom;

    public float shotForceMultiplier;

    [HideInInspector] public bool released;
    [HideInInspector] public bool streched;
    [HideInInspector] public bool thrown;

    private float strech_done;
    private Vector3 strech_done_pos;

    // Start is called before the first frame update
    void Start()
    {
        released = false;
        streched = false;
        thrown = false;

        strech_done = startValue;
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

        if (streched == false)
        {
            if (absMagnetometerValue > startValue)
                streched = true;
            else
                streched = false;
        }

        if (streched == true)
        {
            if (absMagnetometerValue > strech_done)
            {
                strech_done = absMagnetometerValue;
                strech_done_pos = transform.position;
            }
            if (absMagnetometerValue <= startValue)
                released = true;
        }

        if (!released)
        {
            slingPullbackPoint.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.1f);

            // Map the magnetometer value from the range startValue to endValue to the custom range (targetMinPos to targetMaxPos)
            Vector3 mappedPosition = MapRange(absMagnetometerValue, startValue, endValue, targetMinPos, targetMaxPos);

            // Apply the position to the object
            transform.position = mappedPosition;
        }
        else
        {
            if (!thrown)
                Throw();
        }
        
    }

    Vector3 MapRange(float value, float fromMin, float fromMax, Vector3 toMin, Vector3 toMax)
    {
        // Normalize the value between 0 and 1 based on the input range
        float normalizedValue = Mathf.InverseLerp(fromMin, fromMax, value);

        // Map the normalized value to the output range
        Vector3 mappedValue = Vector3.Lerp(toMin, toMax, normalizedValue);

        return mappedValue;
    }


    public void Throw()
    {
        thrown = true;
        Vector3 projectileDirection = drawFrom.transform.position - strech_done_pos;
        transform.parent = null;
        Rigidbody projectileRigidBody = GetComponent<Rigidbody>();
        projectileRigidBody.AddForce(projectileDirection * shotForceMultiplier, ForceMode.Impulse);
        projectileRigidBody.isKinematic = false;
        projectileRigidBody.useGravity = true;
        slingPullbackPoint.position = drawFrom.position;
    }
}
