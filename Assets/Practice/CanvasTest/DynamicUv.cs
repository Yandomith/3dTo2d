using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class DynamicUv : MonoBehaviour
{
    private RawImage rawImage;
    private Material material;

    private float lastAspect = -1f;

    void Start()
    {
        rawImage = GetComponent<RawImage>();
        material = rawImage.material;
        UpdateAspect();
    }

    void Update()
    {
        if (rawImage.canvas.renderMode == RenderMode.ScreenSpaceOverlay ||
            rawImage.canvas.renderMode == RenderMode.ScreenSpaceCamera)
        {
            Rect rect = rawImage.rectTransform.rect;
            float aspect = rect.width / rect.height;

            if (!Mathf.Approximately(aspect, lastAspect))
            {
                material.SetFloat("_Aspect", aspect);
                lastAspect = aspect;
            }
        }
    }

    void UpdateAspect()
    {
        Rect rect = rawImage.rectTransform.rect;
        float aspect = rect.width / rect.height;
        material.SetFloat("_Aspect", aspect);
        lastAspect = aspect;
    }
}