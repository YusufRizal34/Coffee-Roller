using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player Model")
        {
            CollactableCoin.coinCount += 1;
            Destroy(gameObject);
        }
    }
}
