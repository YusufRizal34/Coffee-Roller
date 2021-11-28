using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [System.Serializable]

    public class Pool{
        public string type;
        public GameObject prefab;
        public int size;
    }

    public static SpawnerController Instance;
    
    private void Awake() {
        Instance = this;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    private GameObject objectToSpawn;

    private void Start() {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(Pool pools in pools){
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for(int i = 0; i < pools.size; i++){
                GameObject obj = Instantiate(pools.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pools.type, objectPool);
        }
    }

    public GameObject SpawnFromPool(string type, Vector3 position, Quaternion rotation){
        if(!poolDictionary.ContainsKey(type)){
            Debug.LogWarning("Pool with type :" + type + "doest exist");
            return null;
        }

        objectToSpawn = poolDictionary[type].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        poolDictionary[type].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
