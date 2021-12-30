using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player"){
            int maxStumble = other.GetComponent<CharacterControllers>().maxStumble;
            if(maxStumble > 0){
                maxStumble -= 1;
                other.gameObject.SetActive(false);
            }
            else if(maxStumble < 1){
                gameObject.SetActive(false);
                GameManager.Instance.GameOver();
            }
        }
    }
}