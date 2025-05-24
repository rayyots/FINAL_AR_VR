using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineSoundToggle : MonoBehaviour
{
    [SerializeField] private AudioSource engineAudio;

    private bool isPlaying = false;

    public void ToggleEngineSound()
    {
        if (engineAudio == null)
        {
            Debug.LogWarning("Engine AudioSource not assigned.");
            return;
        }

        if (isPlaying)
        {
            engineAudio.Stop();
            isPlaying = false;
        }
        else
        {
            engineAudio.loop = true;
            engineAudio.Play();
            isPlaying = true;
        }
    }
}
