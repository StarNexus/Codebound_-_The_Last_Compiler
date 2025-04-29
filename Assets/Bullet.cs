using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f; // Speed of the bullet
    public int damage = 40; // Damage dealt by the bullet
    public Rigidbody2D rb;
    public GameObject impactEffect; // Prefab for the impact effect

    void Start()
    {
        rb.linearVelocity = transform.right * speed; // Set the bullet's velocity in the direction it's facing
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage); // Deal damage to the enemy
        }

        // Create impact effect at the point of collision
        if (impactEffect != null)
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }
}
