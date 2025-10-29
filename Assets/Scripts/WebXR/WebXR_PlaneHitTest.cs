//using UnityEngine;
//using WebXR;

//namespace WebXR.Interactions
//{
//    public class WebXR_PlaneHitTest : MonoBehaviour
//    {
//        public GameObject reticle;
//        public GameObject placeablePrefab;
//        public Camera xrCamera;

//        bool hasHit = false;
//        Pose hitPose;

//        void Update()
//        {
            
//            hasHit = WebXRManager.Instance.GetHitPost(hitPose);

//            if (reticle)
//            {
//                reticle.SetActive(hasHit);
//                if (hasHit)
//                    reticle.transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);
//            }

//            // Tap to place
//            if (hasHit && Input.GetMouseButtonDown(0))
//            {
//                GameObject go = Instantiate(placeablePrefab, hitPose.position, Quaternion.identity);

//                // Rotate to face the camera horizontally
//                Vector3 toCam = xrCamera.transform.position - go.transform.position;
//                toCam.y = 0;
//                if (toCam.sqrMagnitude > 0.001f)
//                    go.transform.rotation = Quaternion.LookRotation(toCam, Vector3.up);
//            }
//        }
//    }
//}
