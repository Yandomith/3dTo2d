
using System;
using UnityEngine;
using UnityEngine.UI;

public class UVTronsform : MonoBehaviour
{
    public RawImage rawImage;
    public Camera camera;
    
    private void Start()
    {
        
        rawImage.uvRect = new Rect(-0.5f, -0.5f, 1, 1);
        
    }

    private void Update()
    {
        RectTransform rt = rawImage.rectTransform;
        
        Vector3 worldCenter = rt.position;
        Vector3 screenPos = camera.WorldToScreenPoint(worldCenter);

        
    }

    private void OnDrawGizmos()
    {
        if (rawImage == null) return;

        RectTransform rt = rawImage.rectTransform;

        // Get the full rect of the RawImage
        Rect rect = rt.rect;

        // ✅ Local point for new UV (0,0) = center of rect
        Vector2 localUV00 = new Vector2(0, 0); // center, since uvRect is centered

        // ✅ Local point for UV (0.5, 0.5) — top-right relative to center
        Vector2 localUV05 = new Vector2(
            rect.width * 0.5f,
            rect.height * 0.5f
        );

        // Convert local space to world space
        Vector3 worldUV00 = rt.TransformPoint(localUV00);
        Vector3 worldUV05 = rt.TransformPoint(localUV05);

        // 🔴 Red dot at UV (0,0)
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(worldUV00, 10f);

        // 🟢 Green dot at UV (0.5, 0.5)
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(worldUV05, 8f);

        // 🟩 Line between them
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(worldUV00, worldUV05);
    }

}
