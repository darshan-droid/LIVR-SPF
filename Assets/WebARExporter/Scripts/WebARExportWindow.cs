//using LivrWebARPlugin;
//using UnityEditor;
//using UnityEditor.SceneManagement;
//using UnityEngine;

//namespace LivrWebARPlugin
//{
//    public class WebARExportWindow:EditorWindow
//    {
//        private string exportRoot = "Build/WebAR";
//        private string customSceneId = "";

//        [MenuItem("WebAR/Exporter")]
//        public static void ShowWindow() => GetWindow<WebARExportWindow>("WebARExporter");

//        private void OnGUI()
//        {
//            GUILayout.Label("WebAR Exporter", EditorStyles.boldLabel);
//            customSceneId = EditorGUILayout.TextField("Scene ID", customSceneId);
//            exportRoot = EditorGUILayout.TextField("Export Root", exportRoot);

//            if (GUILayout.Button("Export Current Scene"))
//            {
//                var scene = EditorSceneManager.GetActiveScene();
//                string id = string.IsNullOrEmpty(customSceneId) ? scene.name : customSceneId;
                
//            }
//        }
//    }
//}