using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CarUICanvasToggleMarker : MonoBehaviour
{
    [SerializeField] private Canvas carCanvas;
    [SerializeField] private ARTrackedImageManager trackedImageManager;

    private bool isMarkerTracked = false;
    private Transform markerTransform;

    private void Start()
    {
        // assumes this object is child of the tracked image prefab
        markerTransform = transform.parent;

        if (carCanvas != null)
            carCanvas.enabled = false;
    }

    private void OnEnable()
    {
        if (trackedImageManager != null)
            trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnDisable()
    {
        if (trackedImageManager != null)
            trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (var trackedImage in args.updated)
        {
            // compare by Transform since we're inside the prefab instance
            if (trackedImage.transform == markerTransform)
            {
                isMarkerTracked = trackedImage.trackingState == TrackingState.Tracking;

                if (!isMarkerTracked && carCanvas != null)
                {
                    carCanvas.enabled = false;
                }
            }
        }
    }

    private void Update()
    {
        if (!isMarkerTracked) return;
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);
        if (touch.phase != TouchPhase.Began) return;

        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            return;

        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            // Check if the tapped object is part of the car
            if (hit.transform.IsChildOf(transform))
            {
                if (carCanvas != null)
                    carCanvas.enabled = true;

                return;
            }
        }

        if (carCanvas != null)
            carCanvas.enabled = false;
    }
}
