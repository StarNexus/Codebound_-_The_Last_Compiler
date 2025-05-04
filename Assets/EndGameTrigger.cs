using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameTrigger : MonoBehaviour
{
    [SerializeField] private string endGameSceneName = "EndGame"; // The name of your end game scene
    [SerializeField] private bool showDebugMessage = true; // For testing purposes

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (showDebugMessage)
            {
                Debug.Log("Player reached the end game trigger!");
            }
            SceneManager.LoadScene(endGameSceneName);
        }
    }
}