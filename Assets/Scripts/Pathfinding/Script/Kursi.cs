using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kursi : MonoBehaviour, IInteractable
{
    public bool IsSit { get; set; }

    public void Interaction()
    {
        IsSit = !IsSit;
    }
}
