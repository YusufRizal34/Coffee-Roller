using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraShot : MonoBehaviour, IInteractable, IBuffable
{
    public string name = "SecondShot";
    public float duration = 3f;
    public float speedIncrease = 1.1f;

    public string BuffName{ get{ return name; } }
    public float FinishTime{
        get{ return duration; }
        set{ duration = value; }
    }

    public void Apply(CharacterControllers character){
        character.IsSpeedUp = true;
        float speed = character.CurrentSpeed * speedIncrease;
        character.CurrentSpeed = speed;
    }

    public void Finished(CharacterControllers character){
        float speed = character.CurrentSpeed / speedIncrease;
        character.CurrentSpeed = speed;
        character.IsSpeedUp = false;
    }

    public void Interaction(){
        AudioManager.instance.Play("Powerup Collect");
        GameManager.Instance.AddBuff(this);
        gameObject.SetActive(false);
        AudioManager.instance.Play("Character Boost");
    }
}