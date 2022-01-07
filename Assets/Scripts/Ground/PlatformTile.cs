using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTile : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;

    public GameObject[] coinContainer;
    public GameObject[] buffContainer;

    public Transform[] buffPosition;

    public void SpawnObject(){
        if(coinContainer != null){
            for(int i = 0; i < coinContainer.Length; i++){
                if(!coinContainer[i].activeSelf){
                    coinContainer[i].SetActive(true);
                }
            }
        }

        if(buffContainer != null){
            for(int i = 0; i < buffContainer.Length; i++){
                if(!buffContainer[i].activeSelf){
                    buffContainer[i].SetActive(true);
                    buffContainer[i].transform.position = buffPosition[Random.Range(0, buffPosition.Length)].position;
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