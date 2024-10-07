using UnityEngine;

public class TopDownMovement : MonoBehaviour {

    public float walkSpeed = 5f;
    public Boundary boundary; // optional boundary rectangle

    private Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; 
    }

    void FixedUpdate() {
        // Get input
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized * walkSpeed * Time.fixedDeltaTime;

        // Move the rigidbody without manually setting velocity
        Vector3 newPosition = rb.position + movement;
        
        // Optional: clamp position inside a boundary
        newPosition = new Vector3(
            Mathf.Clamp(newPosition.x, boundary.xMin, boundary.xMax),
            rb.position.y,
            Mathf.Clamp(newPosition.z, boundary.zMin, boundary.zMax)
        );

        // Move the player
        rb.MovePosition(newPosition);
    }
}

[System.Serializable]
public struct Boundary {
    public float xMin;
    public float xMax;
    public float zMin;
    public float zMax;
}