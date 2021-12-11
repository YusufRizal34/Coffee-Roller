using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    public AudioClip CoinFX;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player Model")
        {
            CollactableCoin.coinCount += 1;
            Debug.Log("Coin :" + CollactableCoin.coinCount);
            Destroy(gameObject);
        }
    }
}
