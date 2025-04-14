using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public Vector3 offset; // Base offset between the camera and the player
    public float smoothSpeed = 0.125f; // Smoothness of the camera movement
    public Vector2 deadZone = new Vector2(1f, 1f); // Dead zone dimensions (width, height)
    public float lookAheadDistance = 2f; // Distance to look ahead based on player's movement
    public float lookAheadSmoothTime = 0.2f; // Smooth time for the look-ahead offset
    public float movementThreshold = 0.2f; // Minimum movement required to update look-ahead
    public float groundedYOffset = 4f; // Y offset when the player is grounded

    private Vector3 currentLookAheadOffset; // Smoothed look-ahead offset
    private Vector3 targetLookAheadOffset; // Target look-ahead offset
    private Vector3 velocity = Vector3.zero; // Velocity for SmoothDamp
    private Vector3 lookAheadVelocity = Vector3.zero; // Velocity for look-ahead SmoothDamp
    private Rigidbody2D playerRigidbody; // Reference to the player's Rigidbody2D for velocity

    void Start()
    {
        // Ensure the camera starts at the correct position
        if (player != null)
        {
            playerRigidbody = player.GetComponent<Rigidbody2D>();
            Vector3 startPosition = player.position + offset;
            startPosition.z = transform.position.z; // Maintain the camera's Z position
            transform.position = startPosition;
        }
    }

    void LateUpdate()
    {
        if (player == null || playerRigidbody == null) return;

        // Determine if the player is grounded based on their vertical velocity
        bool isGrounded = Mathf.Abs(playerRigidbody.linearVelocity.y) < 0.1f;

        // Adjust the y offset based on whether the player is grounded
        Vector3 dynamicOffset = offset;
        if (isGrounded)
        {
            dynamicOffset.y = groundedYOffset;
        }

        // Get the player's velocity
        Vector2 playerVelocity = playerRigidbody.linearVelocity;

        // Update the target look-ahead offset based on the player's velocity
        if (playerVelocity.magnitude > movementThreshold) // Ignore very small movements
        {
            targetLookAheadOffset = new Vector3(playerVelocity.x, playerVelocity.y, 0).normalized * lookAheadDistance;
        }
        else
        {
            targetLookAheadOffset = Vector3.zero; // Reset look-ahead offset when the player is idle
        }

        // Smoothly interpolate the look-ahead offset using SmoothDamp
        currentLookAheadOffset = Vector3.SmoothDamp(currentLookAheadOffset, targetLookAheadOffset, ref lookAheadVelocity, lookAheadSmoothTime);

        // Calculate the desired position of the camera
        Vector3 desiredPosition = player.position + dynamicOffset + currentLookAheadOffset;

        // Apply dead zone logic
        Vector3 cameraDelta = desiredPosition - transform.position;
        if (Mathf.Abs(cameraDelta.x) < deadZone.x) desiredPosition.x = transform.position.x;
        if (Mathf.Abs(cameraDelta.y) < deadZone.y) desiredPosition.y = transform.position.y;

        // Smoothly interpolate between the current position and the desired position using SmoothDamp
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);

        // Maintain a fixed Z position for the camera
        smoothedPosition.z = transform.position.z;

        // Update the camera's position
        transform.position = smoothedPosition;
    }
}
