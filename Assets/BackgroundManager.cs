using UnityEngine;

[ExecuteInEditMode]
public class BackgroundManager : MonoBehaviour
{
    public Sprite backgroundSprite; // Assign your background sprite in the Inspector

    private SpriteRenderer backgroundRenderer;

    private void Start()
    {
        InitializeBackground();
        UpdateTiling(); // Ensure tiling is updated when the game starts
    }

    private void OnValidate()
    {
        // Avoid calling UpdateTiling directly during OnValidate
        if (!Application.isPlaying)
        {
            InitializeBackground();
        }
    }

    private void InitializeBackground()
    {
        if (backgroundSprite != null)
        {
            // Check if the background already exists
            if (backgroundRenderer == null)
            {
                Transform existingBackground = transform.Find("Background");
                if (existingBackground != null)
                {
                    backgroundRenderer = existingBackground.GetComponent<SpriteRenderer>();
                }
                else
                {
                    // Create the main background if it doesn't exist
                    GameObject background = new GameObject("Background");
                    backgroundRenderer = background.AddComponent<SpriteRenderer>();
                    backgroundRenderer.sortingLayerName = "Background";
                    backgroundRenderer.sortingOrder = 0;
                    background.transform.parent = transform;
                    background.transform.localPosition = Vector3.zero;
                }
            }

            // Update the background sprite
            backgroundRenderer.sprite = backgroundSprite;
            backgroundRenderer.drawMode = SpriteDrawMode.Tiled;

            // Update tiling only if in play mode
            if (Application.isPlaying)
            {
                UpdateTiling();
            }
        }
        else
        {
            Debug.LogWarning("No background sprite assigned!");
        }
    }

    private void Update()
    {
        // Ensure tiling is updated during runtime
        if (Application.isPlaying)
        {
            UpdateTiling();
        }
    }

    private void UpdateTiling()
    {
        if (backgroundRenderer != null && backgroundRenderer.drawMode == SpriteDrawMode.Tiled)
        {
            // Allow the user to manually adjust the x width without resetting it
            Vector2 currentSize = backgroundRenderer.size;
            float desiredHeight = backgroundSprite.bounds.size.y;

            // Only update the height, leave the x width as is
            backgroundRenderer.size = new Vector2(currentSize.x, desiredHeight);
        }
    }
}
