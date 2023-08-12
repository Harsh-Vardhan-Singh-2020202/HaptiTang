using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementPC : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform player;

    private Controller controls;
    private Vector2 mouseLook;
    private float xRotation = 0f;
    
    [HideInInspector]
    public bool mouse_toggle = false;

    private void Awake()
    {
        controls = new Controller();

        mouse_toggle = false;
        Cursor.lockState = CursorLockMode.Locked;
        transform.GetChild(0).gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisble()
    {
        controls.Disable();
    }

    void Update()
    {
        mouseLook = controls.Player.Look.ReadValue<Vector2>();

        float mouseX = mouseLook.x * mouseSensitivity * Time.deltaTime;
        float mouseY = mouseLook.y * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        player.Rotate(Vector3.up * mouseX);

        if (controls.Player.Toggle_Mouse.triggered && !mouse_toggle)
        {
            mouse_toggle = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else if (controls.Player.Toggle_Mouse.triggered && mouse_toggle)
        {
            mouse_toggle = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (Cursor.lockState == CursorLockMode.None)
        {
            Cursor.visible = true;
            transform.GetChild(0).gameObject.SetActive(false);
        }

        else if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.visible = false;
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
