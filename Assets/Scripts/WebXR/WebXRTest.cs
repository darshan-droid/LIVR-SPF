using UnityEngine;
using WebXR;
using WebXRPlugin;

namespace WebXRPlugin
{
    public class WebXRTest : MonoBehaviour
    {
        [Header("Scene Refs")]
        public Transform reticle;
        public Transform root;
        public GameObject placePrefab;

        private bool placedOnce = false;
        private WebXRManager webxr;
        private GameObject lastPlaced;

        void Awake()
        {
            webxr = WebXRManager.Instance;
            if (!webxr)
                Debug.LogError("WebXRManager not found in scene.");
        }

        void OnEnable()
        {
            WebXRManager.OnXRChange += OnXRChange;
            WebXRManager.OnViewerHitTestUpdate += OnHitTestUpdate;
        }

        void OnDisable()
        {
            WebXRManager.OnXRChange -= OnXRChange;
            WebXRManager.OnViewerHitTestUpdate -= OnHitTestUpdate;
        }

        void OnXRChange(WebXRState state, int views, Rect l, Rect r)
        {
            if (state == WebXRState.AR)
            {
                Debug.Log("AR mode started — initiating hit test.");
                WebXRManager.Instance.StartViewerHitTest();
            }
        }

        void OnHitTestUpdate(WebXRHitPoseData pose)
        {
            if (pose.available)
            {
                if (!reticle.gameObject.activeSelf)
                    reticle.gameObject.SetActive(true);

                reticle.localPosition = pose.position;
                reticle.localRotation = pose.rotation;
            }
            else
            {
                reticle.gameObject.SetActive(false);
            }
        }

        public void PlaceOrMove()
        {
            if (!reticle || !reticle.gameObject.activeSelf || !placePrefab)
            {
                Debug.LogWarning("Cannot place — reticle or prefab not ready.");
                return;
            }

            if (!placedOnce)
            {
                lastPlaced = Instantiate(placePrefab, reticle.position, reticle.rotation, root ? root : null);
                placePrefab.SetActive(false);
                placedOnce = true;
                Debug.Log("Object placed.");
            }
            else if (lastPlaced)
            {
                lastPlaced.transform.SetPositionAndRotation(reticle.position, reticle.rotation);
                Debug.Log("Object moved.");
            }
        }

        public void ResetPlacement()
        {
            if (lastPlaced)
                Destroy(lastPlaced);
            placedOnce = false;
        }
    }
}
