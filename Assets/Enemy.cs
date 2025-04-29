using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public GameObject deathEffect;
    private Animator animator;
    private bool isDying = false;
    private SpriteRenderer spriteRenderer;
    private Collider2D enemyCollider;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyCollider = GetComponent<Collider2D>();
    }

    public void TakeDamage(int damage)
    {
        if (isDying) return;
        
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isDying) return;
        isDying = true;

        // Disable collider immediately to prevent further interactions
        if (enemyCollider != null)
        {
            enemyCollider.enabled = false;
        }

        // Spawn the death effect
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        if (animator != null)
        {
            // Reset all animation states to ensure clean transition
            animator.ResetTrigger("Die");
            
            // Set animator to immediately go to death state without blending
            AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);
            animator.Play(currentState.fullPathHash, 0, 0f);
            
            // Trigger death animation
            animator.SetTrigger("Die");
            
            // Get the length of the death animation
            float animationLength = 0;
            foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
            {
                if (clip.name.Contains("die") || clip.name.Contains("death") || clip.name.Contains("Die") || clip.name.Contains("Death"))
                {
                    animationLength = clip.length;
                    break;
                }
            }
            
            // If we couldn't find the death animation length, use a default
            if (animationLength == 0)
            {
                animationLength = 1f;
            }
            
            // Destroy the game object after the animation finishes
            Destroy(gameObject, animationLength);
        }
        else
        {
            // If there's no animator, destroy immediately
            Destroy(gameObject);
        }
    }
}
