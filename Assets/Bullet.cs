using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f; // Speed of the bullet
    public int damage = 40; // Damage dealt by the bullet
    public Rigidbody2D rb;
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
        Destroy(gameObject);
    }


}
