using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTile : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;

    public GameObject[] coinContainer;

    public void SpawnObject(){
        if(coinContainer != null){
            for(int i = 0; i < coinContainer.Length; i++){
                if(!coinContainer[i].activeSelf){
                    coinContainer[i].SetActive(true);
                }
            }
        }
    }

    public void ChangeObjectRotation(){
        if(coinContainer != null){
            Vector3 eulerRotation = new Vector3(0, 0, 0);

            for(int i = 0; i < coinContainer.Length; i++){
                coinContainer[i].transform.rotation = Quaternion.Euler(eulerRotation);
            }
        }
    }
}