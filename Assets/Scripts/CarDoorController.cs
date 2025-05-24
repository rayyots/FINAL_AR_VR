using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarDoorController : MonoBehaviour
{
    [SerializeField] private List<Animator> doorAnimators;

    private bool doorsOpen = false;

    // This function should be linked to your UI button's OnClick even
    public void ToggleDoors()
    {
        doorsOpen = !doorsOpen;

        foreach (Animator anim in doorAnimators)
        {
            if (anim != null)
            {
                anim.SetBool("Door", doorsOpen);
            }
        }
    }
}
