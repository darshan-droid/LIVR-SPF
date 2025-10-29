//using System;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using Vuforia;
//using ZXing;
//using ZXing.Common;

//[RequireComponent(typeof(VuforiaBehaviour))]
//public class VuforiaQrSceneLoader : MonoBehaviour
//{
//    public List<string> qrTexts;
//    public List<string> sceneNames;
//    public float scanInterval = 0.2f;

//    IBarcodeReader reader;
//    readonly Dictionary<string, string> map = new();
//    float nextScanTime;
//    bool hasLoaded;

//    void Awake()
//    {
//        reader = new BarcodeReader
//        {
//            AutoRotate = true,
//            Options = new DecodingOptions { TryInverted = true, TryHarder = true, PossibleFormats = new List<BarcodeFormat> { BarcodeFormat.QR_CODE } }
//        };

//        for (int i = 0; i < Mathf.Min(qrTexts.Count, sceneNames.Count); i++)
//        {
//            var k = qrTexts[i]?.Trim();
//            var v = sceneNames[i]?.Trim();
//            if (!string.IsNullOrEmpty(k) && !string.IsNullOrEmpty(v)) map[k] = v;
//        }
//    }

//    void OnEnable()
//    {
//        VuforiaApplication.Instance.OnVuforiaStarted += OnVuforiaStarted;
//        var world = VuforiaBehaviour.Instance?.World;
//        if (world != null) world.OnStateUpdated += OnStateUpdated;
//    }

//    void OnDisable()
//    {
//        VuforiaApplication.Instance.OnVuforiaStarted -= OnVuforiaStarted;
//        var world = VuforiaBehaviour.Instance?.World;
//        if (world != null) world.OnStateUpdated -= OnStateUpdated;
//    }

//    void OnVuforiaStarted()
//    {
//        var ok = VuforiaBehaviour.Instance.CameraDevice.SetFrameFormat(PixelFormat.RGBA8888, true);
//        Debug.Log($"[QR] SetFrameFormat RGBA8888: {ok}");
//        VuforiaBehaviour.Instance.CameraDevice.SetFocusMode(FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
//    }

//    void OnStateUpdated()
//    {
//        if (hasLoaded || Time.time < nextScanTime) return;
//        nextScanTime = Time.time + scanInterval;

//        var camDev = VuforiaBehaviour.Instance?.CameraDevice;
//        if (camDev == null)
//        {
//            Debug.LogWarning("[QR] CameraDevice is null.");
//            return;
//        }

//        var img = camDev.GetCameraImage(PixelFormat.RGBA8888);
//        if (Image.IsNullOrEmpty(img))
//        {
//            // This can happen for a few frames after start; also happens in Editor if Play Mode webcam isn’t configured
//            return;
//        }

//        var bytes = img.Pixels;
//        int w = img.Width, h = img.Height;
//        if (bytes == null || bytes.Length < w * h * 4) return;

//        // Downsample for speed (quarter res)
//        int step = 2; // 2 => half res each axis (quarter pixels)
//        int dw = w / step, dh = h / step;
//        var colors = new Color32[dw * dh];
//        for (int y = 0; y < dh; y++)
//        {
//            int sy = y * step;
//            for (int x = 0; x < dw; x++)
//            {
//                int sx = x * step;
//                int si = (sy * w + sx) * 4;
//                colors[y * dw + x] = new Color32(bytes[si], bytes[si + 1], bytes[si + 2], bytes[si + 3]);
//            }
//        }

//        try
//        {
//            var result = reader.Decode(colors, dw, dh);
//            if (result != null && !string.IsNullOrWhiteSpace(result.Text))
//            {
//                var text = result.Text.Trim();
//                Debug.Log($"[QR] Detected: {text}");
//                if (map.TryGetValue(text, out var scene))
//                {
//                    hasLoaded = true;
//                    Handheld.Vibrate();
//                    SceneManager.LoadScene(scene);
//                }
//                else
//                {
//                    Debug.Log($"[QR] No scene mapped for '{text}'.");
//                }
//            }
//        }
//        catch (Exception e)
//        {
//            Debug.LogWarning($"[QR] ZXing decode error: {e.Message}");
//        }
//    }
//}
