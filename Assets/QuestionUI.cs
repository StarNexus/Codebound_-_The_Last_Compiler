using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Cinemachine;

public class QuestionUI : MonoBehaviour
{
    public GameObject uiPanel;
    public TMP_Text questionText;
    public TMP_InputField answerInput;
    public Button submitButton;
    public BossEnemy bossEnemy;
    public PlayerMovement playerMovement;
    public CinemachineVirtualCamera virtualCamera;
    public Transform playerTransform;

    private Transform originalCameraFollow;

    void Start()
    {
        uiPanel.SetActive(false);
        if (bossEnemy != null)
        {
            bossEnemy.QuestionAsked += ShowQuestion;
            bossEnemy.BossDefeated += OnBossDefeated;
        }
        submitButton.onClick.AddListener(OnSubmit);

        if (virtualCamera != null)
        {
            originalCameraFollow = virtualCamera.Follow;
        }
    }

    void ShowQuestion(string question)
    {
        if (string.IsNullOrEmpty(question))
        {
            uiPanel.SetActive(false);
            return;
        }
        uiPanel.SetActive(true);
        questionText.text = question;
        answerInput.text = "";
        
        // Lock player movement when questions start
        if (playerMovement != null)
        {
            playerMovement.LockMovement();
        }

        // Lock camera
        if (virtualCamera != null)
        {
            virtualCamera.Follow = null;
        }
    }

    void OnSubmit()
    {
        if (bossEnemy != null)
        {
            bossEnemy.SubmitAnswer(answerInput.text);
        }
    }

    void OnBossDefeated()
    {
        // Hide the UI
        uiPanel.SetActive(false);

        // Re-enable player movement
        if (playerMovement != null)
        {
            playerMovement.UnlockMovement();
        }

        // Restore camera follow
        if (virtualCamera != null)
        {
            virtualCamera.Follow = originalCameraFollow;
        }
    }
}
