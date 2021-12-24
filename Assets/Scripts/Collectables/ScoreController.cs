using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
   private void Update()
    {
        GameManager.Instance.currentScore = (int)transform.position.z;
    }
}
