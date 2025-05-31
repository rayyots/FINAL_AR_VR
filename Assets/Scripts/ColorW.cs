using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

public class ColorWheel : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    public Material changedMaterial;
    public UnityEvent<Color> onColorChanged;

    private RawImage colorWheelImage;
    private Texture2D colorWheelTexture;

    private void Awake()
    {
        colorWheelImage = GetComponent<RawImage>();

        if (colorWheelImage.texture == null || !(colorWheelImage.texture is Texture2D))
        {
            Debug.LogError("ColorWheel requires a readable Texture2D assigned to RawImage.");
            return;
        }

        colorWheelTexture = colorWheelImage.texture as Texture2D;
    }

    public void OnPointerDown(PointerEventData eventData) => UpdateColor(eventData);

    public void OnDrag(PointerEventData eventData) => UpdateColor(eventData);

    private void UpdateColor(PointerEventData eventData)
    {
        if (colorWheelTexture == null)
            return;

        Vector2 localPoint;
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
                colorWheelImage.rectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out localPoint))
            return;

        Rect rect = colorWheelImage.rectTransform.rect;

        float px = (localPoint.x - rect.x) / rect.width;
        float py = (localPoint.y - rect.y) / rect.height;

        if (px < 0 || px > 1 || py < 0 || py > 1)
            return;

        Color pickedColor = colorWheelTexture.GetPixelBilinear(px, py);

        if (changedMaterial != null)
            changedMaterial.color = pickedColor;

        onColorChanged?.Invoke(pickedColor);
    }
}
