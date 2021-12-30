using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraShot : MonoBehaviour, IInteractable
{
    public float duration = 3f; ///DEFAULT 3

    public void Interaction(){
        GameManager.Instance.IncreaseSpeed();
    }
}
