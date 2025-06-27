using UnityEngine;
using System.IO;

public class CanvasSaver : MonoBehaviour
{
    public RenderTexture canvasTexture;

    private string savePath;

    void Awake()
    {
        savePath = Path.Combine(Application.persistentDataPath, "saved_canvas.png");
    }

    public void SaveCanvas()
    {
        RenderTexture.active = canvasTexture;

        Texture2D tex = new Texture2D(canvasTexture.width, canvasTexture.height, TextureFormat.RGBA32, false);
        tex.ReadPixels(new Rect(0, 0, canvasTexture.width, canvasTexture.height), 0, 0);
        tex.Apply();

        byte[] bytes = tex.EncodeToPNG();
        File.WriteAllBytes(savePath, bytes);

        RenderTexture.active = null;
        Destroy(tex);

        Debug.Log("Canvas saved to: " + savePath);
    }

    public void LoadCanvas()
    {
        if (File.Exists(savePath))
        {
            byte[] bytes = File.ReadAllBytes(savePath);
            Texture2D loadedTex = new Texture2D(2, 2);
            loadedTex.LoadImage(bytes);

            Graphics.Blit(loadedTex, canvasTexture);
            Destroy(loadedTex);
        }
        else
        {
            ClearCanvas();
        }
    }

    public void ClearCanvas()
    {
        RenderTexture.active = canvasTexture;
        GL.Clear(true, true, Color.white);
        RenderTexture.active = null;
    }

    private void OnApplicationQuit()
    {
        SaveCanvas();
    }
}