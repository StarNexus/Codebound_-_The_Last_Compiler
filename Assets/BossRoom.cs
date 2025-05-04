using UnityEngine;
using Cinemachine;
using TMPro;

public class BossRoom : MonoBehaviour
{
    public Transform playerSpawnPoint;
    public CinemachineVirtualCamera bossCamera;
    public float bossHealth = 100f;
    public TextMeshProUGUI inputPromptText;
    public string[] requiredInputs = { "DEBUG", "DUNGEON", "MASTER" }; // Example inputs
    
    private PlayerMovement playerMovement;
    private int currentInputIndex = 0;
    private bool bossActive = false;
    private string currentInput = "";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !bossActive)
        {
            StartBossFight(other.gameObject);
        }
    }

    private void StartBossFight(GameObject player)
    {
        bossActive = true;
        playerMovement = player.GetComponent<PlayerMovement>();
        
        // Lock player position and movement
        player.transform.position = playerSpawnPoint.position;
        playerMovement.LockMovement();
        
        // Activate boss camera
        bossCamera.Priority = 100; // Make sure this camera takes precedence
        
        // Show first input prompt
        UpdateInputPrompt();
    }

    private void Update()
    {
        if (!bossActive) return;

        // Handle text input
        foreach (char c in Input.inputString)
        {
            if (c == '\b') // Backspace
            {
                if (currentInput.Length > 0)
                {
                    currentInput = currentInput.Substring(0, currentInput.Length - 1);
                }
            }
            else if (c == '\n' || c == '\r') // Enter/Return
            {
                CheckInput();
            }
            else
            {
                currentInput += c;
            }
            UpdateInputPrompt();
        }
    }

    private void CheckInput()
    {
        if (currentInput.ToUpper() == requiredInputs[currentInputIndex])
        {
            DamageBoss();
            currentInputIndex++;
            
            if (currentInputIndex >= requiredInputs.Length)
            {
                EndBossFight();
            }
        }
        
        currentInput = "";
        UpdateInputPrompt();
    }

    private void DamageBoss()
    {
        float damageAmount = bossHealth / requiredInputs.Length;
        bossHealth -= damageAmount;
    }

    private void UpdateInputPrompt()
    {
        if (inputPromptText != null)
        {
            string prompt = $"Input '{requiredInputs[currentInputIndex]}' to damage the boss!\nCurrent input: {currentInput}";
            inputPromptText.text = prompt;
        }
    }

    private void EndBossFight()
    {
        bossActive = false;
        playerMovement.UnlockMovement();
        bossCamera.Priority = 0; // Return to normal camera
        inputPromptText.text = "Boss Defeated!";
    }
}