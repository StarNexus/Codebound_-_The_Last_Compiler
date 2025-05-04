using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class QuestionUI : MonoBehaviour
{
    public GameObject uiPanel;
    public TMP_Text questionText;
    public TMP_InputField answerInput;
    public Button submitButton;
    public BossEnemy bossEnemy;

    void Start()
    {
        uiPanel.SetActive(false);
        if (bossEnemy != null)
        {
            bossEnemy.QuestionAsked += ShowQuestion;
            bossEnemy.BossDefeated += HideUI;
        }
        submitButton.onClick.AddListener(OnSubmit);
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
    }

    void OnSubmit()
    {
        if (bossEnemy != null)
        {
            bossEnemy.SubmitAnswer(answerInput.text);
        }
    }

    void HideUI()
    {
        uiPanel.SetActive(false);
    }
}

// Assuming this is the BossEnemy class definition
public class BossEnemyDuplicate : MonoBehaviour
{
    public event Action<string> QuestionAsked;
    public event Action BossDefeated;

    // Other existing methods and properties

    public void SubmitAnswer(string answer)
    {
        // Handle the submitted answer here
        Debug.Log($"Answer submitted: {answer}");
    }
}
