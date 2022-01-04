using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour, IInteractable
{
    public int rotatespeed = 1;

    void Update()
    {
        transform.Rotate(0, rotatespeed, 0, Space.World);   
    }

    public void Interaction()
    {
        GameManager.Instance.coinFromTrack++;
        FindObjectOfType<AudioManager>().Play("Coin Collect");
        gameObject.SetActive(false);
    }
}
