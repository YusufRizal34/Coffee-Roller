using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public int rotatespeed = 1;

    void Update()
    {
        transform.Rotate(0, rotatespeed, 0, Space.World);   
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player Model")
        {
            GameManager.currentCoin++;
            gameObject.SetActive(false);
            FindObjectOfType<AudioManager>().Play("Coin Collect");
        }
    }
}
