using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    public Transform targetCamera1;
    public Transform targetCamera2;

    void Update()
    {
        if (targetCamera1 != null)
            transform.rotation = Quaternion.LookRotation(transform.position - targetCamera1.position);
        if (targetCamera2 != null)
            transform.rotation = Quaternion.LookRotation(transform.position - targetCamera2.position);
    }
}
