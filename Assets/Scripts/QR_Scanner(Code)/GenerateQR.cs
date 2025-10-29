using System.IO;
using UnityEngine;
using ZXing;
using ZXing.QrCode;
using ZXing.Common;
using ZXing.Rendering;
using System.Net.NetworkInformation;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class QRCodeGeneratorWithUI : MonoBehaviour
{
    [Header("QR Settings")]
    public string textToEncode = "player1";
    [Range(128, 2048)] public int size = 512;
    public int margin = 1;

    [Header("Output")]
    public UnityEngine.UI.RawImage outputRawImage; 
    public string outputFileName = "GeneratedQR.png"; 

    public Texture2D GenerateQRCodeTexture()
    {
        if (string.IsNullOrEmpty(textToEncode))
        {
            Debug.LogWarning("[QR] textToEncode is empty.");
            return null;
        }

        var options = new QrCodeEncodingOptions
        {
            Height = size,
            Width = size,
            Margin = margin,
            ErrorCorrection = ZXing.QrCode.Internal.ErrorCorrectionLevel.M
        };

        var writer = new BarcodeWriterPixelData
        {
            Format = BarcodeFormat.QR_CODE,
            Options = options
        };

        PixelData pixelData;
        try
        {
            pixelData = writer.Write(textToEncode);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("[QR] ZXing write failed: " + ex);
            return null;
        }

        int w = pixelData.Width;
        int h = pixelData.Height;
        var tex = new Texture2D(w, h, TextureFormat.RGBA32, false);
        var colors = new Color32[w * h];

        byte[] bytes = pixelData.Pixels;
        if (bytes == null || bytes.Length < w * h * 4)
        {
            Debug.LogError("[QR] PixelData length mismatch.");
            return null;
        }

        for (int i = 0; i < w * h; i++)
        {
            int bi = i * 4;
            byte r = bytes[bi + 0];
            byte g = bytes[bi + 1];
            byte b = bytes[bi + 2];
            byte a = bytes[bi + 3];
            colors[i] = new Color32(r, g, b, a);
        }

        tex.SetPixels32(colors);
        tex.Apply();

        return tex;
    }

    public void GenerateAndShow()
    {
        var tex = GenerateQRCodeTexture();
        if (tex == null)
        {
            Debug.LogError("[QR] Generation returned null texture.");
            return;
        }

        if (outputRawImage != null)
        {
            outputRawImage.texture = tex;
            
            outputRawImage.rectTransform.sizeDelta = new Vector2(tex.width, tex.height) * 0.5f;
        }

        Debug.Log("[QR] QR generated and shown in RawImage (if assigned).");
    }

#if UNITY_EDITOR
    [ContextMenu("Generate and Save PNG to Assets/GeneratedQRs")]
    private void GenerateAndSave()
    {
        var tex = GenerateQRCodeTexture();
        if (tex == null)
        {
            Debug.LogError("[QR] Generation failed, no file saved.");
            return;
        }

        string folder = "Assets/GeneratedQRs";
        if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

        string path = Path.Combine(folder, outputFileName);
        try
        {
            byte[] png = tex.EncodeToPNG();
            File.WriteAllBytes(path, png);
            AssetDatabase.Refresh();
            Debug.Log("[QR] Saved QR PNG to: " + path + ". You can upload this to Vuforia Target Manager.");
        }
        catch (System.Exception ex)
        {
            Debug.LogError("[QR] Failed to save PNG: " + ex);
        }
    }
#endif
}