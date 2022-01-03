using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, IInteractable
{
    public void Interaction()
    {
        var objects = FindObjectOfType<CharacterControllers>();
        if(objects.Invisible == true){
            gameObject.SetActive(false);
        }
        else{
            if(objects.maxStumble > 0){
            objects.maxStumble -= 1;
            gameObject.SetActive(false);
            }
            else if(objects.maxStumble < 1){
                objects.gameObject.SetActive(false);
                GameManager.Instance.GameOver();
            }
        }
    }
}