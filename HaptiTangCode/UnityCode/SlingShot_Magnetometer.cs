using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShot_Magnetometer : MonoBehaviour
{
    public SensorDataReceiver sensorData;

    public GameObject stone;
    public GameObject cupTower;

    public Transform drawFrom;
    public Transform cupTowerPlace;
    public Transform slingPullbackPoint;

    public float reloadTime = 5f;

    private GameObject stoneObject;
    private GameObject cupTowerObject;

    private SlingShotStones_Magnetometer stoneProperties;

    private bool reloading = false;

    private float reloadTimer = 0f;

    // Start is called before the first frame update
    void OnEnable()
    {
        stoneObject = Instantiate(stone, drawFrom.position, drawFrom.rotation);
        stoneProperties = stoneObject.GetComponent<SlingShotStones_Magnetometer>();
        if (stoneProperties != null)
        {
            stoneProperties.sensorData = sensorData;
            stoneProperties.slingPullbackPoint = slingPullbackPoint;
            stoneProperties.drawFrom = drawFrom;
        }
        cupTowerObject = Instantiate(cupTower, cupTowerPlace.position, cupTowerPlace.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (stoneProperties != null)
        {
            if (stoneProperties.released == true && !reloading)
            {
                reloading = true;
                reloadTimer = reloadTime;
            }
        }

        if (reloading)
        {
            reloadTimer -= Time.deltaTime;

            if (reloadTimer <= 0f)
            {
                ReloadStone();
                ResetCupTower();
            }
        }
    }

    void ReloadStone()
    {
        reloading = false;
        // Destroy the current stone object
        Destroy(stoneObject);

        // Instantiate a new stone object
        stoneObject = Instantiate(stone, drawFrom.position, drawFrom.rotation);
        stoneProperties = stoneObject.GetComponent<SlingShotStones_Magnetometer>();
        if (stoneProperties != null)
        {
            stoneProperties.sensorData = sensorData;
            stoneProperties.slingPullbackPoint = slingPullbackPoint;
            stoneProperties.drawFrom = drawFrom;
        }
    }

    public void ResetCupTower()
    {
        Destroy(cupTowerObject);
        cupTowerObject = Instantiate(cupTower, cupTowerPlace.position, cupTowerPlace.rotation);
    }

    void OnDisable()
    {
        Destroy(stoneObject);
        Destroy(cupTowerObject);
    }
}