using UnityEngine;
using Cinemachine;

public class LockZoneTrigger : MonoBehaviour
{
    [SerializeField] private Vector3 cameraLockPosition;
    [SerializeField] private Vector3 playerLockPosition;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private QuizBoss quizBoss; // Make it assignable in Inspector
    
    private PlayerMovement playerMovement;
    private Transform originalCameraFollow;

    private void Start()
    {
        if (quizBoss == null)
        {
            quizBoss = GetComponent<QuizBoss>();
            if (quizBoss == null)
            {
                quizBoss = FindAnyObjectByType<QuizBoss>();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Store the player reference
            playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                // Lock player movement and position
                playerMovement.LockMovement();
                other.transform.position = playerLockPosition;
            }

            // Store original camera target and lock camera
            if (virtualCamera != null)
            {
                originalCameraFollow = virtualCamera.Follow;
                virtualCamera.Follow = null;
                virtualCamera.transform.position = cameraLockPosition;
            }

            // Start the quiz battle
            if (quizBoss != null)
            {
                quizBoss.StartBattle();
            }
        }
    }

    public void UnlockPlayerAndCamera()
    {
        // Unlock player movement
        if (playerMovement != null)
        {
            playerMovement.UnlockMovement();
        }

        // Restore camera following
        if (virtualCamera != null)
        {
            virtualCamera.Follow = originalCameraFollow;
        }
    }
}