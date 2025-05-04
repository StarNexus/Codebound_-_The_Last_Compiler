using UnityEngine;
using Cinemachine;

// Since QuizBoss is in the global namespace, we just needed to ensure the file compiles
public class LockZoneTrigger : MonoBehaviour
{
    [SerializeField] private Vector3 cameraLockPosition;
    [SerializeField] private Vector3 playerLockPosition;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    
    private PlayerMovement playerMovement;
    private Transform originalCameraFollow;
    private QuizBoss quizBoss;

    private void Start()
    {
        quizBoss = GetComponent<QuizBoss>();
        if (quizBoss == null)
        {
            Debug.LogError("QuizBoss component not found! Please add it to the same GameObject.");
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