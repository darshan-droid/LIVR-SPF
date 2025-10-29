//using System.IO;
//using UnityEditor;
//using UnityEditor.SceneManagement;
//using UnityEngine;
//using GLTFast.Editor;
//using GLTFast;

//namespace LivrWebARPlugin
//{
//    public static class WebARExporter
//    {
//        private const string EXPORT_ROOT = "Build/WebAR";
//        private const string TEMPLATE_FOLDER = "Assets/WebARExporter/Template";

//        [MenuItem("WebAR/Export Active Scene")]
//        public static void ExportActiveSceneMenu()
//        {
//            var activeScene = EditorSceneManager.GetActiveScene();
//            if (!activeScene.isLoaded)
//            {
//                Debug.LogError("[WebARExporter] Active scene is not loaded.");
//                return;
//            }

//            ExportActiveScene(activeScene.name);
//        }

//        public static void ExportActiveScene(string sceneId)
//        {
//            string sceneFolder = Path.Combine(EXPORT_ROOT, sceneId);
//            if (!Directory.Exists(sceneFolder))
//                Directory.CreateDirectory(sceneFolder);

//            Debug.Log($"[WebARExporter] Exporting scene '{sceneId}' to {sceneFolder}");

//            CopyTemplate(sceneFolder);
//            WriteSceneMeta(sceneFolder, sceneId);
//            ExportGlb(sceneFolder);

//            AssetDatabase.Refresh();
//            EditorUtility.RevealInFinder(sceneFolder);
//            Debug.Log($"[WebARExporter] Export complete: {sceneFolder}");
//        }

//        private static void CopyTemplate(string destFolder)
//        {
//            if (!Directory.Exists(TEMPLATE_FOLDER))
//            {
//                Debug.LogError($"[WebARExporter] Template folder not found at {TEMPLATE_FOLDER}");
//                return;
//            }

//            foreach (var file in Directory.GetFiles(TEMPLATE_FOLDER))
//            {
//                var fileName = Path.GetFileName(file);
//                var destPath = Path.Combine(destFolder, fileName);
//                File.Copy(file, destPath, true);
//            }

//            foreach (var dir in Directory.GetDirectories(TEMPLATE_FOLDER))
//            {
//                CopyDirectoryRecursive(dir, Path.Combine(destFolder, Path.GetFileName(dir)));
//            }

//            Debug.Log("[WebARExporter] Copied template files.");
//        }

//        private static void CopyDirectoryRecursive(string srcDir, string dstDir)
//        {
//            if (!Directory.Exists(dstDir))
//                Directory.CreateDirectory(dstDir);

//            foreach (var file in Directory.GetFiles(srcDir))
//            {
//                var destPath = Path.Combine(dstDir, Path.GetFileName(file));
//                File.Copy(file, destPath, true);
//            }

//            foreach (var sub in Directory.GetDirectories(srcDir))
//            {
//                CopyDirectoryRecursive(sub, Path.Combine(dstDir, Path.GetFileName(sub)));
//            }
//        }

//        private static void WriteSceneMeta(string sceneFolder, string sceneId)
//        {
//            var meta = SceneMetaBuilder.BuildfromScene(sceneId);
//            var json = SceneMetaBuilder.ToJson(meta);
//            string metaOutPath = Path.Combine(sceneFolder, "scene.meta.json");
//            File.WriteAllText(metaOutPath, json);
//            Debug.Log($"[WebARExporter] Wrote meta: {metaOutPath}");
//        }

//        private static void ExportGlb(string sceneFolder)
//        {
//            var allBehaviours = Object.FindObjectsOfType<WebARBehaviour>(true);
//            if (allBehaviours == null || allBehaviours.Length == 0)
//            {
//                Debug.LogWarning("[WebARExporter] No WebARBehaviour found in scene.");
//                File.WriteAllText(Path.Combine(sceneFolder, "scene.glb"), "// No WebARBehaviour found");
//                return;
//            }

//            var rootToExport = allBehaviours[0].gameObject;
//            string glbPath = Path.Combine(sceneFolder, "scene.glb");

//            //var exporter = new GltfExport();
//            //exporter.AddScene(new GameObject[] { rootToExport });
//            //bool success = exporter.SaveToFileAndDispose(glbPath);

//            //if (success)
//            //    Debug.Log($"[WebARExporter] GLB exported: {glbPath}");
//            //else
//            //{
//            //    Debug.LogError("[WebARExporter] GLB export failed.");
//            //    File.WriteAllText(glbPath, "// GLB export failed");
//            //}
//        }
//    }
//}

