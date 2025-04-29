using UnityEngine;

public class enemyPatrol : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    public float speed = 2f;
    private Rigidbody2D rb;
    private Animator anim;
    private Transform targetPoint;
    private bool isFacingRight = true; // Start facing right
    private bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        targetPoint = pointB.transform;
        anim.SetBool("isRunning", true);
        // Removed initial flip
    }

    void Update()
    {
        if (isDead) return;

        float direction = targetPoint.position.x - transform.position.x;
        float move = Mathf.Sign(direction) * speed;
        rb.linearVelocity = new Vector2(move, rb.linearVelocity.y);

        // Set running animation based on movement
        anim.SetBool("isRunning", Mathf.Abs(move) > 0.01f);

        // Flip sprite if needed (corrected logic)
        if ((move > 0 && !isFacingRight) || (move < 0 && isFacingRight))
        {
            Flip();
        }

        // Switch target when close enough
        if (Mathf.Abs(direction) < 0.1f)
        {
            rb.linearVelocity = Vector2.zero;
            anim.SetBool("isRunning", false);
            if (targetPoint == pointB.transform)
                targetPoint = pointA.transform;
            else
                targetPoint = pointB.transform;
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void Die()
    {
        isDead = true;
        rb.linearVelocity = Vector2.zero;
        anim.SetTrigger("Die");
    }
}
