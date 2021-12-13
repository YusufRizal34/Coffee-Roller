using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTile : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;

    public GameObject[] coinContainer;

    public void SpawnObject(){
        for(int i = 0; i < coinContainer.Length; i++){
            if(!coinContainer[i].activeSelf){
                coinContainer[i].SetActive(true);
            }
        }
    }
}