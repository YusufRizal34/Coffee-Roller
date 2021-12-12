using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public static int distance;
    public Text scoreText;

    private void Awake()
    {
        scoreText = GameObject.Find("Score Value Text").GetComponent<Text>();
    }

    // Update is called once per frame
    private void Update()
    {
        scoreText.text = "" + (int)transform.position.z;
    }
}
