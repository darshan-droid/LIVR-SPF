using UnityEngine;

namespace Livr.WebAR
{
    [DisallowMultipleComponent]
    public class LivrSceneID : MonoBehaviour
    {
        public string SceneId = "Scene_01";

        private void OnValidate()
        {
            if (string.IsNullOrWhiteSpace(SceneId))
                SceneId = gameObject.scene.name;
        }
    }
}
