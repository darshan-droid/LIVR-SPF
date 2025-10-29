//using System;
//using System.Collections.Generic;
//using UnityEngine;

//namespace LivrWebARPlugin
//{
//    [Serializable]
//    public class SceneMeta
//    {
//        [Serializable]
//        public class ObjectInfo
//        {
//            //Objects from WebARBehaviour
//            public string name;
//            public string ContentID;
//            public bool PlaneOnSurface;
//            public bool AllowRotation;
//            public bool AllowScaling;
//            public float[] InitialScale;
//        }
//        public string SceneID;
//        public List<ObjectInfo> Objects = new();
//    }
//    public static class SceneMetaBuilder
//    {
//        public static SceneMeta BuildfromScene(string sceneID)
//        {            
//            var meta = new SceneMeta {SceneID = sceneID };      
//            foreach (var beh in GameObject.FindObjectsOfType<WebARBehaviour>(true))
//            {
//                meta.Objects.Add(new SceneMeta.ObjectInfo
//                {
//                    ContentID = beh.Content_Id,                    
//                    name = beh.gameObject.name,
//                    PlaneOnSurface = beh.PlaneOnSurface,
//                    AllowRotation = beh.AllowRotate,
//                    AllowScaling = beh.AllowScale,
//                    InitialScale = new float[]
//                        {beh.InitialScale.x, beh.InitialScale.y, beh.InitialScale.z}
//                });
//            }
//            return meta;
//        }

//        public static string ToJson(SceneMeta meta) =>
//            JsonUtility.ToJson(meta, true);
//    }
//}  
