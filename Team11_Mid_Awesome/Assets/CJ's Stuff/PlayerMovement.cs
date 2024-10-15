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
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;
    }

    void Update()
    {
        // Flip movement inputs
        float moveX = -Input.GetAxis("Horizontal"); // Corrected inversion
        float moveZ = -Input.GetAxis("Vertical");   // Corrected inversion
        _movementInput = new Vector3(moveX, 0, moveZ).normalized;

        float mouseX = Input.GetAxis("Mouse X");
        _rotationInput = new Vector3(0, mouseX, 0);
    }

    void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
    }

    private void MovePlayer()
    {
        Vector3 moveDirection = _movementInput * moveSpeed;
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
