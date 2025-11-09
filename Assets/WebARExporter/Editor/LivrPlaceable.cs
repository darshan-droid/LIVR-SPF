using UnityEngine;

namespace Livr.WebAR
{
    [DisallowMultipleComponent]
    public class LivrPlaceable : MonoBehaviour
    {
        [Header("Export ID (should match GLB node name)")]
        public string ContentId = "MainBoard";
        public bool PlaceOnSurface = true;
        public Vector3 InitialScale = Vector3.one;
    }
}
