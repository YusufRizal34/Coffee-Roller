using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    [Header("SPAWN CONTROLLER")]
    public Camera mainCamera;
    public Transform startPoint;

    [Header("SPAWN GROUND")]
    public PlatformTile[] tilePrefab;
    public PlatformTile[] earlyTilePrefab;
    List<PlatformTile> spawnedTiles = new List<PlatformTile>();

    [Header("SPAWN COIN")]
    public List<CoinController> coinSpawn;
    public float coin;

    void Start()
    {

        Vector3 spawnPosition = startPoint.position;

        //SPAWN EARLY TILE FIRST
        for (int i = 0; i < earlyTilePrefab.Length; i++) {
            spawnPosition -= earlyTilePrefab[i].startPoint.localPosition;
            PlatformTile spawnedTile = Instantiate(earlyTilePrefab[i], spawnPosition, Quaternion.identity) as PlatformTile;
            
            spawnPosition = spawnedTile.endPoint.position;
            spawnedTile.transform.SetParent(transform);
            spawnedTiles.Add(spawnedTile);
        }

        //SPAWN NEXT TILE
        for (int i = 0; i < tilePrefab.Length; i++) {
            spawnPosition -= tilePrefab[i].startPoint.localPosition;
            PlatformTile spawnedTile = Instantiate(tilePrefab[i], spawnPosition, Quaternion.identity) as PlatformTile;
            spawnPosition = spawnedTile.endPoint.position;
            spawnedTile.transform.SetParent(transform);
            spawnedTiles.Add(spawnedTile);
        }
    }

    void Update()
    {
        //IF CAMERA POINT(ENDPOINT ON CURRENT TILE) NEAR 0
        if (mainCamera.WorldToViewportPoint(spawnedTiles[0].endPoint.position).z < 0) {
            PlatformTile tileTmp = spawnedTiles[0];
            spawnedTiles.RemoveAt(0);
            tileTmp.ChangeObjectRotation();
            tileTmp.transform.position = spawnedTiles[spawnedTiles.Count - 1].endPoint.position - tileTmp.startPoint.localPosition;
            tileTmp.SpawnObject();
            spawnedTiles.Add(tileTmp);
        }
    }
}
