using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondShot : MonoBehaviour, IInteractable, IBuffable
{
    public float duration;
    public float FinishTime{ get{ return duration; } set{ duration = value; } }

    public void Apply(CharacterControllers character){
        character.Invisible = true;
    }

    public void Finished(CharacterControllers character){
        character.Invisible = false;
    }

    public void Interaction(){
        AudioManager.instance.Play("Powerup Collect");
        GameManager.Instance.AddBuff(this);
        gameObject.SetActive(false);
    }
}