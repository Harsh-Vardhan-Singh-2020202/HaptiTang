using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class DeviceChecker : MonoBehaviour
{
    public GameObject Controller_VR;
    public GameObject Controller_PC;
    public string DeviceName;

    private void Start()
    {
        Debug.Log(XRSettings.loadedDeviceName);
        if (XRSettings.loadedDeviceName == DeviceName)
            Action(Controller_VR, Controller_PC);
        else
            Action(Controller_PC, Controller_VR);
        enabled = false;
    }

    private void Action(GameObject device1, GameObject device2)
    {
        device1.SetActive(true);
        device2.SetActive(false);
        Destroy(device2);
    }
}
