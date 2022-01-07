using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondShot : MonoBehaviour, IInteractable, IBuffable
{
    public string name = "SecondShot";
    public float duration = 3f;
    
    public string BuffName{ get{ return name; } set{ name = value; } }
    public float FinishTime{
        get{ return duration; }
        set{ duration = value; }
    }

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