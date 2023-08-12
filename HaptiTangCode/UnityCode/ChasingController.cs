using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingController : MonoBehaviour
{
    public Transform target; // The target game object to chase (child object)
    public float chaseSpeed = 5f; // The speed at which the chasing object moves towards the target

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            // Calculate the direction to move towards the target
            Vector3 targetDirection = target.position - transform.position;

            // Normalize the direction to get a direction vector
            targetDirection.Normalize();

            // Move the chasing object towards the target using the chaseSpeed
            transform.position += targetDirection * chaseSpeed * Time.deltaTime;
        }
    }
}
