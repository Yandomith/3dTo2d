using UnityEngine;
using UnityEngine.UI;

public class BrushCursor : MonoBehaviour
{
    public RectTransform canvasRect;           // Reference to the canvas RectTransform
    public RectTransform brushCursorUI;        // The UI element that shows the brush preview
    public Texture2D[] brushes;                // Brush textures to preview
    public int currentBrushIndex = 0;          // Index of selected brush
    [Range(0.005f, 0.1f)] public float brushRadius = 0.05f;  // Brush size relative to canvas

    void Update()
    {
        UpdateCursorPosition();
    }

    void UpdateCursorPosition()
    {
        if (canvasRect == null || brushCursorUI == null) return;

        Vector2 localPos;
        bool inside = RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect, Input.mousePosition, null, out localPos
        );

        if (inside)
        {
            brushCursorUI.gameObject.SetActive(true);

            // Move the cursor to the local mouse position on canvas
            brushCursorUI.localPosition = localPos;

            // Convert brush radius to pixel size based on canvas dimensions
            float brushWidthPixels = brushRadius * canvasRect.rect.width;
            float brushHeightPixels = brushRadius * canvasRect.rect.height;

            // Update brush preview size
            brushCursorUI.sizeDelta = new Vector2(brushWidthPixels, brushHeightPixels);

            // Show correct brush texture
            RawImage img = brushCursorUI.GetComponent<RawImage>();
            if (img != null && brushes != null && brushes.Length > currentBrushIndex)
            {
                img.texture = brushes[currentBrushIndex];
            }
        }
        else
        {
            // Hide cursor when pointer is outside canvas
            brushCursorUI.gameObject.SetActive(false);
        }
    }

    public void SetBrush(int index)
    {
        if (index >= 0 && index < brushes.Length)
        {
            currentBrushIndex = index;
        }
    }

    public void SetRadius(float radius)
    {
        brushRadius = Mathf.Clamp(radius, 0.005f, 0.1f);
    }
}
