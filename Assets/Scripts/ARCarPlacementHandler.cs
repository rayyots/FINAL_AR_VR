using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.Interaction.Toolkit.AR;

public class ARCarPlacementHandler : MonoBehaviour
{
    public ARPlacementInteractable arPlacement;
    public Button rotateButton;

    private bool carPlaced = false;

    void OnEnable()
    {
        arPlacement.objectPlaced.AddListener(OnCarPlaced);
    }

    void OnDisable()
    {
        arPlacement.objectPlaced.RemoveListener(OnCarPlaced);
    }

    void OnCarPlaced(ARObjectPlacementEventArgs args)
    {
        if (carPlaced) return;

        GameObject placedCar = args.placementObject.gameObject;

        CarYRotator rotator = placedCar.GetComponent<CarYRotator>();
        if (rotator != null)
        {
            rotateButton.onClick.AddListener(rotator.ToggleRotation);
            carPlaced = true;
        }
    }
}
