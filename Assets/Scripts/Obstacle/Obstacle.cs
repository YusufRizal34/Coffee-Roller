using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, IInteractable
{
    private float currentCharacterDeadTime;
    public float characterDeadTime = 2f; ///DEFAULT IS 2

    public async void Interaction()
    {
        var objects = FindObjectOfType<CharacterControllers>();
        
        if(objects.Invisible == true){
            GameManager.Instance.currentCoin += 5;
            gameObject.SetActive(false);
        }
        else{
            if(objects.maxStumble > 0){
                objects.maxStumble -= 1;
                gameObject.SetActive(false);
            }
            else if(objects.maxStumble < 1){
                objects.gameObject.GetComponent<CharacterControllers>().Dead();
                while(characterDeadTime > 0){
                    characterDeadTime -= Time.deltaTime;
                    await Task.Yield();
                }
                objects.gameObject.GetComponent<CharacterControllers>().enabled = false;
                GameManager.Instance.Result();
            }
        }
    }
}