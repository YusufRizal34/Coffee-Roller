using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public static int distance;
    public Text scoreText;

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "" + (int)transform.position.z;
    }
}
