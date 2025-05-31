using UnityEngine;
using UnityEngine.EventSystems;

public class CarUICanvasToggle : MonoBehaviour
{
    [SerializeField] private Canvas carCanvas;

    private void Start()
    {
        if (carCanvas != null)
            carCanvas.enabled = false;
    }

    void Update()
    {
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);
        if (touch.phase != TouchPhase.Began) return;

        // Ignore if touching UI
        if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            return;

        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // If we tapped this car or a child of it
            if (hit.transform.IsChildOf(transform))
            {
                if (carCanvas != null)
                    carCanvas.enabled = true;

                return;
            }
        }

        // Tapped elsewhere
        if (carCanvas != null)
            carCanvas.enabled = false;
    }
}