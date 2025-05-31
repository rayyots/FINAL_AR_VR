using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPanel : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    private void Awake()
    {
        if (panel != null)
            panel.SetActive(false);
    }

    public void Show()
    {
        if (panel != null)
            panel.SetActive(true);
    }

    public void Hide()
    {
        if (panel != null)
            panel.SetActive(false);
    }

    public void Toggle()
    {
        if (panel != null)
            panel.SetActive(!panel.activeSelf);
    }
}
