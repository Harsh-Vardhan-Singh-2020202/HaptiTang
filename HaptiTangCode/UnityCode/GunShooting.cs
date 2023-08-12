using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunShooting : MonoBehaviour
{
    public SensorDataReceiver sensorData;

    public GameObject MuzzleFire;
    public Transform Muzzle;
    public Transform MuzzleForward;

    public AudioClip shooting;
    public AudioSource audioSrc;

    public float threshold;
    public float shootingRange = 100f;
    public float damage = 10f;
    public float impactForce = 30f;

    private bool shot;

    private Controller controls;

    void Awake()
    {
        controls = new Controller();
    }

    // Start is called before the first frame update
    void Start()
    {
        shot = true;
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


        if ((absMagnetometerValue >= threshold || controls.Player.Shoot.triggered) && !shot)
        {
            shot = true;
            Shoot();
        }

        else if (absMagnetometerValue < threshold && shot)
        {
            shot = false;
        }
    }

    void Shoot()
    {
        Debug.Log("Shooting");

        audioSrc.PlayOneShot(shooting);

        GameObject muzzleFire = Instantiate(MuzzleFire, Muzzle.position, Quaternion.identity);
        Destroy(muzzleFire, 0.15f);

        // Get the direction in which the bullet or particle effect should travel
        Vector3 shootDirection = MuzzleForward.position - Muzzle.position;

        // Perform the raycast from the Muzzle position in the shooting direction
        RaycastHit hit;
        if (Physics.Raycast(Muzzle.position, shootDirection, out hit, shootingRange))
        {
            Zombie zombie = hit.transform.GetComponent<Zombie>();
            if (zombie != null)
            {
                zombie.TakeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
        }
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisble()
    {
        controls.Disable();
    }
}