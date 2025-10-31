using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.VFX;
using WebXR;
using WebXRPlugin;


#if UNITY_INPUT_SYSTEM_1_4_4_OR_NEWER
using UnityEngine.InputSystems;
#endif

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

        private Vector3 reticlePosition;
        private Quaternion reticleRotation;

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
#if HAS_ROTATION_AND_ROTATION
            reticle.SetLocalPositionAndRotaion(originPosition, originRotation);
#else
            reticle.localPosition = reticlePosition;
            reticle.localRotation = reticleRotation;
#endif
            placePrefab.SetActive(false);
            if (state == WebXRState.AR)
            {
                Debug.Log("AR mode started — initiating hit test.");
                WebXRManager.Instance.StartViewerHitTest();
            }
        }

        void OnHitTestUpdate(WebXRHitPoseData pose)
        {
            placePrefab.SetActive(pose.available);
            if (pose.available)
            {
                if (!reticle.gameObject.activeSelf)
                    reticle.gameObject.SetActive(true);              

#if HAS_POSITION_AND_ROTATION
            transform.SetLocalPositionAndRotation(pose.position, pose.rotation);
#else
                transform.localPosition = pose.position;
                transform.localRotation = pose.rotation;
#endif
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
                //lastPlaced = Instantiate(placePrefab, reticle.position, reticle.rotation, root ? root : null);
                lastPlaced = Instantiate(placePrefab, transform.localPosition, transform.localRotation, root ? root : null);
                placePrefab.SetActive(false);
                placedOnce = true;
                Debug.Log("Object placed.");
            }
            else if (lastPlaced)
            {
                //lastPlaced.transform.SetPositionAndRotation(reticle.position, reticle.rotation);
                lastPlaced.transform.SetPositionAndRotation(transform.localPosition, transform.localRotation);
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