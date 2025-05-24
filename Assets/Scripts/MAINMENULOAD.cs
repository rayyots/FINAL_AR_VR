using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MAINMENULOAD : MonoBehaviour
{
    public void LoadMarkerBased()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadMarkerless()
    {
        SceneManager.LoadScene(2);
    }
}
