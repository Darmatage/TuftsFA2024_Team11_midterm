using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 720f;

    private Rigidbody _rigidbody;
    private Vector3 _movementInput;
    private Vector3 _rotationInput;

    void Start()
    {
        // Cache the Rigidbody component
        _rigidbody = GetComponent<Rigidbody>();
        // Ensure Rigidbody settings are optimal for player control
        _rigidbody.freezeRotation = true; // Prevents Rigidbody from rotating on its own due to collisions
    }

    void Update()
    {
        // Get movement input from WASD/Arrow keys
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        _movementInput = new Vector3(moveX, 0, moveZ).normalized;

        // Get mouse rotation input
        float mouseX = Input.GetAxis("Mouse X");
        _rotationInput = new Vector3(0, mouseX, 0);
    }

    void FixedUpdate()
    {
        // Handle movement
        MovePlayer();

        // Handle rotation
        RotatePlayer();
    }

    private void MovePlayer()
    {
        Vector3 moveDirection = transform.TransformDirection(_movementInput) * moveSpeed;
        Vector3 newPosition = _rigidbody.position + moveDirection * Time.fixedDeltaTime;
        _rigidbody.MovePosition(newPosition);
    }

    private void RotatePlayer()
    {
        float rotationAngle = _rotationInput.y * rotationSpeed * Time.fixedDeltaTime;
        Quaternion deltaRotation = Quaternion.Euler(0, rotationAngle, 0);
        _rigidbody.MoveRotation(_rigidbody.rotation * deltaRotation);
    }
}
