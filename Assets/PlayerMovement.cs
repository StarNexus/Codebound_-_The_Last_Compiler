using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Movement speed
    public float maxSpeed = 3f; // Maximum horizontal speed
    public float jumpForce = 2f; // Reduced jump force
    private Rigidbody2D rb;
    private bool isGrounded;
    private Animator animator; // Reference to the Animator component
    private bool facingRight = true; // Track the direction the player is facing

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true; // Prevent rotation of the player model
        animator = GetComponent<Animator>(); // Get the Animator component
    }

    void Update()
    {
        // Get horizontal input for left and right movement
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        // Update the walking animation immediately
        bool isWalking = horizontalInput != 0; // Player is walking if there's horizontal input
        animator.SetBool("isWalking", isWalking); // Update the Animator parameter

        // Flip the player based on movement direction
        if (horizontalInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (horizontalInput < 0 && facingRight)
        {
            Flip();
        }

        // Apply horizontal velocity with a speed limit
        if (horizontalInput != 0)
        {
            float newVelocityX = horizontalInput * speed;
            rb.linearVelocity = new Vector2(Mathf.Clamp(newVelocityX, -maxSpeed, maxSpeed), rb.linearVelocity.y);
        }
        else
        {
            // Stop horizontal movement when no input is detected
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }

        // Check for jump input and if the player is grounded
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // Apply reduced jump velocity
            animator.SetBool("isJumping", true); // Trigger the jump animation
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player is touching the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("isJumping", false); // Stop the jump animation when grounded
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Check if the player is no longer touching the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void Flip()
    {
        // Flip the player's rotation to face the opposite direction
        facingRight = !facingRight;
        Vector3 rotation = transform.eulerAngles;
        rotation.y += 180f; // Rotate 180 degrees on the Y-axis
        transform.eulerAngles = rotation;
    }
}
