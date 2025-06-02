using UnityEngine;

public class ColorPanel : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    private bool isPanelVisible = false;

    private void Awake()
    {
        if (panel != null)
            panel.SetActive(false);
    }

    public void OnChangeColorButtonPressed()
    {
        if (panel == null) return;

        isPanelVisible = !isPanelVisible;
        panel.SetActive(isPanelVisible);
    }
}
