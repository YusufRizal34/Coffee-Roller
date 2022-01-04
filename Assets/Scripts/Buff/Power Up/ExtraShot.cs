using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraShot : MonoBehaviour, IInteractable, IBuffable
{
    public float duration;
    public float speedIncrease = 1.1f;
    public float FinishTime{ get{ return duration; } set{ duration = value; } }

    public void Apply(CharacterControllers character){
        float speed = character.CurrentSpeed * speedIncrease;
        character.CurrentSpeed = speed;
    }

    public void Finished(CharacterControllers character){
        float speed = character.CurrentSpeed / speedIncrease;
        character.CurrentSpeed = speed;
    }

    public void Interaction(){
        GameManager.Instance.AddBuff(this);
        gameObject.SetActive(false);
    }
}