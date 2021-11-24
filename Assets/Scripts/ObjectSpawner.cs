using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    private bool spawningObject = false;
    public static ObjectSpawner Instance;

    private void Awake() {
        Instance = this;
    }

    public void SpawnTile(float zPosition){
        SpawnerController.Instance.SpawnFromPool("Ground", new Vector3(0,0,zPosition), Quaternion.identity);
    }
}
