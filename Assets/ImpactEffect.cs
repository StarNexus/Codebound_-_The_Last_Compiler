using UnityEngine;

public class ImpactEffect : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        // If we have an animator, get the length of the current animation
        if (animator != null)
        {
            AnimatorClipInfo[] currentClipInfo = animator.GetCurrentAnimatorClipInfo(0);
            if (currentClipInfo.Length > 0)
            {
                float clipLength = currentClipInfo[0].clip.length;
                Destroy(gameObject, clipLength);
            }
            else
            {
                // Fallback if no animation clip is found
                Destroy(gameObject, 0.5f);
            }
        }
        else
        {
            // Fallback if no animator is present
            Destroy(gameObject, 0.5f);
        }
    }

    // Called by animation event at the end of the impact animation
    public void OnAnimationComplete()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;
        }
    }
}