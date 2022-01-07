using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondShot : MonoBehaviour, IInteractable, IBuffable
{
    public float duration;
    public float FinishTime{ get{ return duration * (GameManager.Instance.ShowLevelSecondShot() + 1); } set{ duration = value; } }

    public void Apply(CharacterControllers character){
        character.IsShielded = true;
    }

    public void Finished(CharacterControllers character){
        character.IsShielded = false;
    }

    public void Interaction(){
        AudioManager.instance.Play("Powerup Collect");
        GameManager.Instance.AddBuff(this);
        gameObject.SetActive(false);
    }
}