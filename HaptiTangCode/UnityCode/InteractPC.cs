using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractPC : MonoBehaviour
{
    public float radius = 3f;
    public Camera PC_CAMERA;

    [HideInInspector]
    public bool pointing = false;

    [HideInInspector]
    public bool interacted = false;

    private Controller controls;

    private void Awake()
    {
        controls = new Controller();
    }

    void Update()
    {
        if (PC_CAMERA != null)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Ray ray = PC_CAMERA.ScreenPointToRay(mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, radius))
            {
                if (hit.collider == gameObject.GetComponent<Collider>() && hit.collider != null)
                    pointing = true;
                else
                    pointing = false;
            }
            else
                pointing = false;

            if (pointing && controls.Player.Interact.triggered)
                interacted = true;
            else
                interacted = false;
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
