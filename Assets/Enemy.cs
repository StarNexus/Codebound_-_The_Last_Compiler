using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100; // Health of the enemy
    public GameObject deathEffect; // Effect to play on death

    public void TakeDamage (int damage)
    {
        health -= damage; // Reduce health by damage amount
        if (health <= 0)
        {
            Die(); // Call the Die method if health is 0 or less
        }
    }

    void Die()
    {
        if(health <= 0)
        {
            Destroy(gameObject); // Destroy the enemy game object
        }
        Instantiate(deathEffect, transform.position, Quaternion.identity); // Instantiate the death effect at the enemy's position
        Destroy(gameObject);
    }
}
