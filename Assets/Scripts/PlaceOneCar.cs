using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit.AR;

public class PlaceOneCar : MonoBehaviour
{
    [SerializeField] private GameObject carPrefab;
    [SerializeField] private ARPlacementInteractable arPlacement;

    private GameObject spawnedCar;

    void OnEnable()
    {
        arPlacement.objectPlaced.AddListener(HandlePlacement);
    }

    void OnDisable()
    {
        arPlacement.objectPlaced.RemoveListener(HandlePlacement);
    }

    private void HandlePlacement(ARObjectPlacementEventArgs args)
    {
        if (spawnedCar != null)
        {
            Debug.Log("Car already placed.");
            return;
        }

        // Prevent auto placement from the inspector
        if (args.placementObject != null)
        {
            Destroy(args.placementObject);
        }

        // Fix: Use the transform of the placementObject to get position and rotation
        if (args.placementObject != null)
        {
            spawnedCar = Instantiate(carPrefab, args.placementObject.transform.position, args.placementObject.transform.rotation);
        }
        else
        {
            Debug.LogError("Placement object is null. Cannot determine placement position and rotation.");
        }
    }
    
}
