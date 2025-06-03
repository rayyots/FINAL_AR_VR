using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class TrackedImageCarSpawner : MonoBehaviour
{
    [Header("Assign the menu canvas in prefab")]
    public Canvas menuCanvas;

    void Start()
    {
        if (menuCanvas != null)
            menuCanvas.enabled = false;
    }

    void Update()
    {
#if UNITY_EDITOR
        // Mouse click support for editor testing
        if (Input.GetMouseButtonDown(0))
        {
            HandleTap(Input.mousePosition);
        }
#else
        // Touch support for mobile
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            HandleTap(Input.GetTouch(0).position);
        }
#endif
    }

    void HandleTap(Vector2 screenPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform == transform || hit.transform.IsChildOf(transform))
            {
                if (menuCanvas != null)
                    menuCanvas.enabled = !menuCanvas.enabled;
            }
        }
    }
}