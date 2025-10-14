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

    [Header("Head Bob")]
    public float bobAmplitude = 0.05f;    // How far to move (meters)
    public float bobFrequency = 6f;       // Speed of the bob

    private Vector3 cameraTransformDefaultPos;
    private float bobTimer;
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

        input.Player.Interact.performed += ctx => PlayerInteract();

        Cursor.visible = false;
        cameraTransformDefaultPos = cameraTransform.localPosition;
    }
    void OnEnable() => input.Player.Enable();
    void OnDisable() => input.Player.Disable();

    // Update is called once per frame
    void Update()
    {
        HandleLook();
        HandleMovement();
        HandleHeadBob();
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
    void HandleHeadBob()
    {
        // Magnitude of movement input (0 if standing still)
        bool isMoving = moveInput.sqrMagnitude > 0.01f && controller.isGrounded;

        if (isMoving)
        {
            bobTimer += Time.deltaTime * bobFrequency;
            float bobOffset = Mathf.Sin(bobTimer) * bobAmplitude;
            cameraTransform.localPosition = cameraTransformDefaultPos + new Vector3(0, bobOffset, 0);
        }
        else
        {
            // Reset to default when not moving
            bobTimer = 0;
            cameraTransform.localPosition = Vector3.Lerp(
                cameraTransform.localPosition,
                cameraTransformDefaultPos,
                Time.deltaTime * 5f
            );
        }
    }
    void PlayerInteract()
    {
        var layermask0 = 1 << 0;
        var layermask3=1 << 3;
        var finalMask= layermask0 | layermask3; //Selects the correct layers to block the raycasts

        RaycastHit hit;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0));
        if(Physics.Raycast(ray, out hit, 3, finalMask))
        {
            Interact interactScript = hit.transform.GetComponent<Interact>();
            if (interactScript) interactScript.CallInteract(this);
        }
    }

    public bool IsMoving()
    {
        return moveInput.sqrMagnitude > 0.01f && controller.isGrounded;
    }
}
