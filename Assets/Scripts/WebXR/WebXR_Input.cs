using UnityEngine;
using WebXRPlugin;

public class WebXRInputTap : MonoBehaviour
{
    public WebXRTest placer;

    void Update()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Debug.Log("Touch screen tap detected");
            placer.PlaceOrMove();
        }

#if UNITY_EDITOR || UNITY_WEBGL
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse button clicked");
            placer.PlaceOrMove();
        }
#endif
    }
}
