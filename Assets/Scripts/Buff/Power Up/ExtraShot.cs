using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraShot : MonoBehaviour, IInteractable, IBuffable
{
    public float duration;
    public float speedIncrease = 1.1f;
    public float FinishTime{ get{ return duration * (GameManager.Instance.ShowLevelExtraShot() + 1); } set{ duration = value; } }

    public void Apply(CharacterControllers character){
        float speed = character.CurrentSpeed * speedIncrease;
        print(speed);
        character.CurrentSpeed = speed;
    }

    public void Finished(CharacterControllers character){
        float speed = character.CurrentSpeed / speedIncrease;
        print(speed);
        character.CurrentSpeed = speed;
    }

    public void Interaction(){
        AudioManager.instance.Play("Powerup Collect");
        GameManager.Instance.AddBuff(this);
        gameObject.SetActive(false);
        AudioManager.instance.Play("Character Boost");
    }
}