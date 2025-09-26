using System;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.InputSystem;
public class FPSController : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.2f;

    [Header("Look")]
    public float mouseSensitivity = 1f;
    public Transform cameraTransform;
    

    private CharacterController controller;
    private PlayerInputActions input;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private float verticalVelocity;
    private float pitch;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        input= new PlayerInputActions();

        input.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        input.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        input.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        input.Player.Look.canceled += ctx => lookInput = Vector2.zero;

        input.Player.Jump.performed += ctx => Jump();
    }
    void OnEnable() => input.Player.Enable();
    void OnDisable() => input.Player.Disable();

    // Update is called once per frame
    void Update()
    {
        HandleLook();
        HandleMovement();
    }

    void HandleMovement()
    {
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        if (controller.isGrounded && verticalVelocity < 0) verticalVelocity = -2f;

        verticalVelocity += gravity * Time.deltaTime;
        Vector3 velocity = move * walkSpeed + Vector3.up * verticalVelocity;
        controller.Move(velocity * Time.deltaTime);
    }
    void HandleLook()
    {
        // horizontal (yaw)
        transform.Rotate(Vector3.up * lookInput.x * mouseSensitivity);

        // vertical (pitch)
        pitch -= lookInput.y * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, -80f, 80f);
        cameraTransform.localRotation = Quaternion.Euler(pitch, 0f, 0f);

    }
    void Jump()
    {
        if (controller.isGrounded)
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }
}
