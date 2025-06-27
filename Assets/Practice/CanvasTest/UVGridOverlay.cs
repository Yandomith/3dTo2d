using UnityEngine;
using UnityEngine.UI;

public class UVGridOverlay : MonoBehaviour
{
    public RectTransform canvasRect;  // Assign your RawImage's RectTransform
    public GameObject linePrefab;     // A thin UI line prefab (Image with RectTransform)
    public int divisions = 10;        // How many divisions (10 = 0.1 steps)

    void Start()
    {
        if (!canvasRect || !linePrefab)
        {
            Debug.LogError("Missing canvasRect or linePrefab!");
            return;
        }

        float width = canvasRect.rect.width;
        float height = canvasRect.rect.height;

        for (int i = 0; i <= divisions; i++)
        {
            float u = i / (float)divisions;

            // Vertical lines (U axis)
            GameObject vLine = Instantiate(linePrefab, canvasRect);
            RectTransform vRect = vLine.GetComponent<RectTransform>();
            vRect.sizeDelta = new Vector2(1f, height);
            vRect.anchoredPosition = new Vector2(u * width - width / 2f, 0);

            // Horizontal lines (V axis)
            GameObject hLine = Instantiate(linePrefab, canvasRect);
            RectTransform hRect = hLine.GetComponent<RectTransform>();
            hRect.sizeDelta = new Vector2(width, 1f);
            hRect.anchoredPosition = new Vector2(0, u * height - height / 2f);
        }
    }
}