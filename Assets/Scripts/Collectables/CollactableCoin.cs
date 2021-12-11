using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollactableCoin : MonoBehaviour
{
    public static int coinCount;
    public Text coinText;

     private void Awake()
    {
        coinText = GameObject.Find("Coin Text").GetComponent<Text>();
    }

    private void Update()
    {
        coinText.text = "" + coinCount;
    }
}
