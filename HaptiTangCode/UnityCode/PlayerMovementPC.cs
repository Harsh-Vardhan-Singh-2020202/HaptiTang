using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementPC : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float jumpHeight = 0.5f;

    private Controller controls;
    private CharacterController controller;

    private Vector3 velocity;
    private Vector3 move;
    private float gravity = -9.81f;
    private bool isGrounded;

    public Transform groundCheck;
    public float distanceToGround = 0.4f;
    public LayerMask groundMask;


    void Awake()
    {
        controls = new Controller();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Movement();
        Gravity();
        Jump();
    }

    private void Movement()
    {
        move = controls.Player.Movement.ReadValue<Vector2>();

        Vector3 movement = (move.y * transform.forward) + (move.x * transform.right);
        controller.Move(movement * moveSpeed * Time.deltaTime);
    }

    private void Gravity()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, distanceToGround, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void Jump()
    {
        if (controls.Player.Jump.triggered)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
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
