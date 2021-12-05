using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollactableCoin : MonoBehaviour
{
    public static int coinCount;
    public Text coinText;

    // Update is called once per frame
    void Update()
    {
        coinText.text = "" + coinCount;
    }
}
