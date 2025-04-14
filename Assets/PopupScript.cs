using UnityEngine;
using UnityEngine.UI; // For Button
using TMPro; // For TextMeshProUGUI

public class InteractiveItem : MonoBehaviour
{
    [Header("Popup UI Elements")]
    public GameObject popupPanel; // The popup panel GameObject
    public TextMeshProUGUI popupTitle; // The TextMeshProUGUI component for the title
    public TextMeshProUGUI popupText; // The TextMeshProUGUI component for the main message
    public TextMeshProUGUI popupTip; // The TextMeshProUGUI component for the tip
    public Button closeButton; // The Button to close the popup

    [Header("Popup Content")]
    public string title; // The title to display in the popup
    [TextArea]
    public string message; // The message to display in the popup
    [TextArea]
    public string tip; // The tip to display in the popup

    [Header("Edit Mode Settings")]
    [Range(0f, 1f)]
    public float editModeTransparency = 0.2f; // Transparency value for Edit Mode

    private void Start()
    {
        if (popupPanel != null)
        {
            popupPanel.SetActive(false);
        }

        if (closeButton != null)
        {
            closeButton.onClick.AddListener(ClosePopup); // Add functionality to the close button
        }
    }

    private void OnValidate()
    {
        // Apply transparency only in Edit Mode
        if (!Application.isPlaying && popupPanel != null)
        {
            SetTransparency(editModeTransparency);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player interacts with the item
        if (other.CompareTag("Player"))
        {
            ShowPopup();
        }
    }

    private void ShowPopup()
    {
        if (popupPanel != null && popupTitle != null && popupText != null && popupTip != null)
        {
            popupPanel.SetActive(true);
            popupTitle.text = title;
            popupText.text = message;
            popupTip.text = tip;

            // Reset transparency for Play Mode
            SetTransparency(1f); // Fully opaque in Play Mode
        }
    }

    private void ClosePopup()
    {
        Debug.Log("ClosePopup method called");
        // Hide the popup
        if (popupPanel != null)
        {
            popupPanel.SetActive(false);
        }
    }

    private void SetTransparency(float alpha)
    {
        // Adjust transparency for all child elements of the popupPanel
        Image[] images = popupPanel.GetComponentsInChildren<Image>();
        foreach (Image image in images)
        {
            Color color = image.color;
            color.a = alpha;
            image.color = color;
        }

        TextMeshProUGUI[] texts = popupPanel.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI text in texts)
        {
            Color color = text.color;
            color.a = alpha;
            text.color = color;
        }
    }
}