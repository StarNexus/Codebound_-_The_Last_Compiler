using UnityEngine;

public class LockZone : MonoBehaviour
{
    public BossEnemy bossEnemy;
    private bool playerInside = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!playerInside && other.CompareTag("Player"))
        {
            playerInside = true;
            if (bossEnemy != null)
            {
                bossEnemy.StartQuestions();
            }
        }
    }
}
