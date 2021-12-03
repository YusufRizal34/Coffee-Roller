using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveComponent : MonoBehaviour
{
    public float speed = 5f;
    public Transform player;
    public float spawnDistance = 40f;
    public float despawnDistances = 20f;
    public int tileCounter = 1;
    public bool canSpawnTile = true;

    Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void Update(){
        if(player == null){
            player = GameObject.Find("Player Model").transform;
        }
        // transform.position += -transform.forward * speed * Time.deltaTime;
        print(player.position.z + "," + (tileCounter * spawnDistance) + "," + (tileCounter * despawnDistances));

        if(player.position.z <= tileCounter * spawnDistance && transform.tag == "Ground" && canSpawnTile){
            ObjectSpawner.Instance.SpawnTile(tileCounter * spawnDistance);
            canSpawnTile = false;
            tileCounter++;
        }

        if(player.position.z >= tileCounter * despawnDistances){
            canSpawnTile = true;
            gameObject.SetActive(false);
        }
    }
}
