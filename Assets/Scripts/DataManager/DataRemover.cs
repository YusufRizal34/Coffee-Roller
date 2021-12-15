using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataRemover : MonoBehaviour
{
    private void Awake() {
        UserDataManager.Remove();
    }
}
