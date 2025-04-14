using UnityEngine;
using UnityEngine.UI;
using TMPro; // Import TextMeshPro namespace

public class InteractiveItem : MonoBehaviour
{
    public GameObject popupPanel; // Assign the Panel GameObject in the Inspector
    public TextMeshProUGUI popupText; // Use TextMeshProUGUI instead of Text
    public Button closeButton;    // Assign the Button component in the Inspector
    public string message;        // The message to display in the popup

    private void Start()
    {
        if (popupPanel != null)
        {
            popupPanel.SetActive(false);
        }

        if (closeButton != null)
        {
            closeButton.onClick.AddListener(ClosePopup);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ShowPopup();
        }
    }

    private void ShowPopup()
    {
        if (popupPanel != null && popupText != null)
        {
            popupPanel.SetActive(true);
            popupText.text = message;
        }
    }

    private void ClosePopup()
    {
        if (popupPanel != null)
        {
            popupPanel.SetActive(false);
        }
    }
}