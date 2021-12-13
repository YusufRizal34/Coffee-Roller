using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoin : MonoBehaviour
{
    public GameObject spawn;
    public bool stopSpawning = false;
    public float SpawnTime;
    public float spawnDelay;

    // void Start()
    // {
    //     InvokeRepeating("SpawnObject", SpawnTime, spawnDelay);
    // }

    // public void SpawnObject()
    // {
    //     Instantiate(spawn, transform.position, transform.rotation);
    //     if (stopSpawning)
    //         CancelInvoke("SpawnObject");
    // }


}
