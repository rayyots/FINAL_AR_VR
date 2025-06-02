using UnityEngine;

public class CarYRotator : MonoBehaviour
{
    private bool isRotating = false;
    public float rotationSpeed = 50f;

    public void ToggleRotation()
    {
        isRotating = !isRotating;
        Debug.Log("Rotation toggled: " + isRotating);
    }

    void Update()
    {
        if (isRotating)
        {
            transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
        }
    }
}
