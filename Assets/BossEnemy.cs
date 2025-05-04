using UnityEngine;
using System;

public class BossEnemy : MonoBehaviour
{
    public int maxHealth = 6;
    private int currentHealth;
    public string[] questions;
    public string[] answers;
    private int currentQuestionIndex = 0;

    public delegate void OnQuestionAsked(string question);
    public event OnQuestionAsked QuestionAsked;

    public delegate void OnBossDefeated();
    public event OnBossDefeated BossDefeated;

    public delegate void OnHealthChanged(int health);
    public event OnHealthChanged HealthChanged;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void StartQuestions()
    {
        currentQuestionIndex = 0;
        AskCurrentQuestion();
    }

    public void SubmitAnswer(string playerAnswer)
    {
        if (playerAnswer.Trim().ToLower() == answers[currentQuestionIndex].Trim().ToLower())
        {
            currentHealth--;
            HealthChanged?.Invoke(currentHealth);
        }
        currentQuestionIndex++;
        if (currentQuestionIndex < questions.Length && currentHealth > 0)
        {
            AskCurrentQuestion();
        }
        else
        {
            if (currentHealth <= 0)
            {
                BossDefeated?.Invoke();
                Destroy(gameObject);
            }
        }
    }

    private void AskCurrentQuestion()
    {
        if (QuestionAsked != null && currentQuestionIndex < questions.Length)
        {
            QuestionAsked(questions[currentQuestionIndex]);
        }
    }
}