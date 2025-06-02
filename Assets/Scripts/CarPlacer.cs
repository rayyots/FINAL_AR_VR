using UnityEngine;
using System.Collections.Generic;

public class CarPlacer : MonoBehaviour
{
    [SerializeField] private List<GameObject> carPrefabs;

    private GameObject activeCar;
    private Vector3 spawnPosition;
    private Quaternion spawnRotation;
    private bool isAnchorSet = false;

    public void SetAnchor(Transform anchor)
    {
        spawnPosition = anchor.position;
        spawnRotation = anchor.rotation;
        isAnchorSet = true;
    }

    public void LoadCarByIndex(int index)
    {
        if (!isAnchorSet || carPrefabs == null || index < 0 || index >= carPrefabs.Count)
        {
            Debug.LogWarning($"Can't load car at index {index}. Check anchor and list.");
            return;
        }

        if (activeCar != null)
        {
            Destroy(activeCar);
        }

        activeCar = Instantiate(carPrefabs[index], spawnPosition, spawnRotation);
    }

    public void LoadFirstCar() => LoadCarByIndex(0);
    public void LoadSecondCar() => LoadCarByIndex(1);
}
