using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit.AR;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR;
using System.Collections.Generic;
using Unity.XR.CoreUtils;

public class CarPlacerController : MonoBehaviour
{
    [SerializeField] private List<GameObject> carPrefabs;
    [SerializeField] private ARRaycastManager raycastManager;
    [SerializeField] private XROrigin xrOrigin;

    private GameObject currentCar;
    private Vector3? fixedPosition = null;
    private Quaternion fixedRotation;
    private int requestedModelIndex = -1;

    void Update()
    {
        // Skip if no model requested or position already fixed
        if (requestedModelIndex < 0 || fixedPosition != null)
            return;

        if (Input.touchCount == 0 || Input.GetTouch(0).phase != TouchPhase.Began)
            return;

        var touch = Input.GetTouch(0);
        var hits = new List<ARRaycastHit>();

        if (raycastManager.Raycast(touch.position, hits, TrackableType.Planes))
        {
            var hitPose = hits[0].pose;
            fixedPosition = hitPose.position;
            fixedRotation = hitPose.rotation;
            PlaceModel(requestedModelIndex);
        }
    }

    private void PlaceModel(int index)
    {
        if (index < 0 || index >= carPrefabs.Count || fixedPosition == null)
        {
            Debug.LogWarning("Cannot place car. Invalid index or placement not ready.");
            return;
        }

        if (currentCar != null)
            Destroy(currentCar);

        currentCar = Instantiate(carPrefabs[index], fixedPosition.Value, fixedRotation);
    }

    // UI Buttons should call these
    public void LoadFirstCar() => OnModelRequested(0);
    public void LoadSecondCar() => OnModelRequested(1);

    private void OnModelRequested(int index)
    {
        requestedModelIndex = index;

        if (fixedPosition != null)
            PlaceModel(index); // Instant switch if already placed
    }
}
