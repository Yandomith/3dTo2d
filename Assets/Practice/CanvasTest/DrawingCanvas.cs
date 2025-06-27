using UnityEngine;
using UnityEngine.UI;

public class DrawingCanvas : MonoBehaviour
{
    [Header("Canvas Setup")]
    public RenderTexture canvasTexture;
    public RawImage canvasImage;
    [Header("Brush Settings")]
    public Material drawMaterial;
     // Optional, for brush preview
    public Color brushColor = Color.black;
    public Texture2D[] brushes;
    [Range(0.005f, 0.1f)] public float brushRadius = 0.05f;
    private int currentBrushIndex = 0;

    private RectTransform canvasRect;
    private RenderTexture tempRT;
    private Vector2? lastDrawUV = null;

    private float spacing = 0.01f;
    private float drawInterval = 0.02f;
    private float lastDrawTime = 0f;

    void Start()
    {

        canvasRect = canvasImage.GetComponent<RectTransform>();
            

        
        canvasImage.texture = canvasTexture;

        
        InitializeBrushMaterial();

        tempRT = new RenderTexture(canvasTexture.width, canvasTexture.height, 0, RenderTextureFormat.ARGB32);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
            HandleDrawing();
        else
            lastDrawUV = null;
        

    }

  

    private void HandleDrawing()
    {
        Vector2 localPos;
        Camera eventCam = GetEventCamera();

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, Input.mousePosition, eventCam, out localPos))
        {
            Vector2 uv = RectPointToUV(localPos);

            float timeNow = Time.time;
            if (timeNow - lastDrawTime >= drawInterval)
            {
                if (lastDrawUV.HasValue)
                    DrawInterpolated(lastDrawUV.Value, uv);
                else
                    DrawAtUV(uv);

                lastDrawUV = uv;
                lastDrawTime = timeNow;
            }
        }
    }

    private void DrawInterpolated(Vector2 from, Vector2 to)
    {
        float dist = Vector2.Distance(from, to);
        int steps = Mathf.CeilToInt(dist / spacing);
        for (int i = 0; i <= steps; i++)
        {
            Vector2 lerped = Vector2.Lerp(from, to, i / (float)steps);
            DrawAtUV(lerped);
        }
    }

    public void DrawAtUV(Vector2 uv)
    {
        Graphics.Blit(canvasTexture, tempRT);

        drawMaterial.SetTexture("_MainTex", tempRT);

        Texture2D currentBrush = brushes[currentBrushIndex];
        drawMaterial.SetTexture("_BrushTex", currentBrush);
        drawMaterial.SetFloat("_BrushTexAspect", currentBrush.width / (float)currentBrush.height);

        float brushWidthUV = (brushRadius * canvasRect.rect.width) / canvasRect.rect.width;
        float brushHeightUV = (brushRadius * canvasRect.rect.height) / canvasRect.rect.height;

        Vector4 brushUV = new Vector4(
            uv.x - brushWidthUV * 0.5f,
            uv.y - brushHeightUV * 0.5f,
            brushWidthUV,
            brushHeightUV
        );
        drawMaterial.SetVector("_BrushUV", brushUV);
        drawMaterial.SetColor("_BrushColor", brushColor);

        Graphics.Blit(tempRT, canvasTexture, drawMaterial);
    }

    private void InitializeBrushMaterial()
    {
        if (brushes != null && brushes.Length > 0)
        {
            drawMaterial.SetTexture("_BrushTex", brushes[currentBrushIndex]);
            drawMaterial.SetFloat("_BrushTexAspect", brushes[currentBrushIndex].width / (float)brushes[currentBrushIndex].height);
        }
        else
        {
            Debug.LogWarning("No brushes assigned!");
        }
    }

    private Camera GetEventCamera()
    {
        Canvas canvas = canvasRect.GetComponentInParent<Canvas>();
        if (canvas != null && canvas.renderMode != RenderMode.ScreenSpaceOverlay)
            return canvas.worldCamera;
        return null;
    }

    private Vector2 RectPointToUV(Vector2 localPos)
    {
        float u = Mathf.InverseLerp(-canvasRect.rect.width * 0.5f, canvasRect.rect.width * 0.5f, localPos.x);
        float v = Mathf.InverseLerp(-canvasRect.rect.height * 0.5f, canvasRect.rect.height * 0.5f, localPos.y);
        return new Vector2(u, v);
    }

    public void SetBrush(int index)
    {
        if (index >= 0 && index < brushes.Length)
        {
            currentBrushIndex = index;
            InitializeBrushMaterial();
        }
    }

    void OnDestroy()
    {
        if (tempRT != null) tempRT.Release();
    }
}
