using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class QuizBoss : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject quizPanel;
    public TextMeshProUGUI questionText;
    public Button[] answerButtons;
    public TextMeshProUGUI[] answerTexts;
    public Button continueButton;
    public TMP_InputField answerInputField; // Assign in Inspector
    public Button submitButton; // Assign in Inspector
    public TextMeshProUGUI feedbackText; // Optional: feedback display
    
    [Header("Quiz Settings")]
    [TextArea(3,10)]
    public string[] questions = new string[6]; // Exactly 6 questions
    [TextArea(2,5)]
    public string[][] answers = new string[6][]; // 6 answer arrays
    public int[] correctAnswers = new int[6]; // 6 correct answer indices
    public int bossHealth = 3; // Set boss health in Inspector
    
    private int currentQuestionIndex = -1;
    private int correctAnswersCount = 0;
    private bool isBattleActive = false;

    private void Awake()
    {
        if (submitButton != null)
            submitButton.onClick.AddListener(OnSubmitAnswer);
    }

    private void Start()
    {
        if (quizPanel != null)
        {
            quizPanel.SetActive(false);
        }

        // Remove continue button logic
        // Set up button listeners for answer buttons if you want to keep them
        for (int i = 0; i < answerButtons.Length; i++)
        {
            int buttonIndex = i; // Capture the index for the lambda
            if (answerButtons[i] != null)
            {
                answerButtons[i].onClick.AddListener(() => OnAnswerSelected(buttonIndex));
            }
        }
    }

    public void StartBattle()
    {
        if (!isBattleActive)
        {
            isBattleActive = true;
            currentQuestionIndex = -1;
            correctAnswersCount = 0;
            
            if (quizPanel != null)
            {
                quizPanel.SetActive(true);
            }
            
            ShowNextQuestion();
        }
    }

    private void ShowNextQuestion()
    {
        currentQuestionIndex++;
        if (currentQuestionIndex >= 6)
        {
            BossDies();
            return;
        }

        // Display the question
        if (questionText != null)
        {
            questionText.text = questions[currentQuestionIndex];
        }

        // Display the answers
        for (int i = 0; i < answerTexts.Length && i < answers[currentQuestionIndex].Length; i++)
        {
            if (answerTexts[i] != null)
            {
                answerTexts[i].text = answers[currentQuestionIndex][i];
            }
        }

        // Enable answer buttons and disable continue button
        SetAnswerButtonsInteractable(true);
        if (continueButton != null)
        {
            continueButton.gameObject.SetActive(false);
        }

        if (answerInputField != null)
            answerInputField.text = "";
        if (feedbackText != null)
            feedbackText.text = "";
    }

    private void OnAnswerSelected(int answerIndex)
    {
        if (!isBattleActive) return;

        SetAnswerButtonsInteractable(false);

        // Check if answer is correct
        if (answerIndex == correctAnswers[currentQuestionIndex])
        {
            correctAnswersCount++;
            // You can add visual feedback for correct answer here
        }
        else
        {
            // You can add visual feedback for wrong answer here
        }

        if (continueButton != null)
        {
            continueButton.gameObject.SetActive(true);
        }
    }

    private void OnSubmitAnswer()
    {
        if (!isBattleActive || answerInputField == null) return;
        string playerAnswer = answerInputField.text.Trim();
        string correct = answers[currentQuestionIndex][correctAnswers[currentQuestionIndex]].Trim();
        if (string.Equals(playerAnswer, correct, System.StringComparison.OrdinalIgnoreCase))
        {
            bossHealth--;
            if (feedbackText != null) feedbackText.text = "Correct! Boss health: " + bossHealth;
            if (bossHealth <= 0 || currentQuestionIndex == 5)
            {
                BossDies();
                return;
            }
            else
            {
                ShowNextQuestion();
            }
        }
        else
        {
            if (feedbackText != null) feedbackText.text = "Incorrect! Try again.";
        }
    }

    private void BossDies()
    {
        isBattleActive = false;
        if (quizPanel != null) quizPanel.SetActive(false);
        if (feedbackText != null) feedbackText.text = "Boss Defeated!";
        Debug.Log("Boss defeated!");
        // Add any additional logic for boss death here
    }

    private void SetAnswerButtonsInteractable(bool interactable)
    {
        foreach (Button button in answerButtons)
        {
            if (button != null)
            {
                button.interactable = interactable;
            }
        }
    }

    private void EndBattle()
    {
        isBattleActive = false;
        
        if (quizPanel != null)
        {
            quizPanel.SetActive(false);
        }

        // You can add logic here to determine if the player won based on correctAnswersCount
        bool victory = correctAnswersCount >= (questions.Length / 2); // Example: Need 50% correct to win
        
        if (victory)
        {
            Debug.Log("Quiz Battle Victory!");
            // Add your victory logic here
        }
        else
        {
            Debug.Log("Quiz Battle Failed!");
            // Add your failure logic here
        }
    }
}