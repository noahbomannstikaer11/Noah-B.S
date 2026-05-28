using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Camera mainCamera;

    private float xRotation = 0f;
    private Vector3 moveDirection = Vector3.zero;
    private float groundDrag = 5f;
    private float gravity = -9.81f;

    private void Start()
    {
        if (characterController == null)
            characterController = GetComponent<CharacterController>();

        if (mainCamera == null)
            mainCamera = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        HandleInput();
        ApplyGravity();
        Move();
        Look();
    }

    private void HandleInput()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDir = transform.forward * verticalInput + transform.right * horizontalInput;
        moveDir = moveDir.normalized;

        moveDirection.x = moveDir.x * moveSpeed;
        moveDirection.z = moveDir.z * moveSpeed;
    }

    private void ApplyGravity()
    {
        if (characterController.isGrounded)
        {
            moveDirection.y = -2f; // Small negative value to keep grounded
        }
        else
        {
            moveDirection.y += gravity * Time.deltaTime;
        }
    }

    private void Move()
    {
        if (characterController != null)
            characterController.Move(moveDirection * Time.deltaTime);
    }

    private void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        if (mainCamera != null)
            mainCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
