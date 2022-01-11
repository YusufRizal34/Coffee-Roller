using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraShot : MonoBehaviour, IInteractable, IBuffable
{
    private string buffName;
    public float duration = 3f;
    public float speedIncrease = 1.1f;

    public int rotatespeed = 1;

    void Update()
    {
        transform.Rotate(0, rotatespeed, 0, Space.World);
    }

    public string BuffName{ get{ return buffName; } }
    public float FinishTime{
        get{ return duration; }
        set{ duration = value; }
    }

    public void Apply(CharacterControllers character){
        character.IsSpeedUp = true;
        float speed = character.CurrentSpeed * speedIncrease;
        character.speedAfterBuff = character.CurrentSpeed;
        character.CurrentSpeed = speed;
    }

    public void Finished(CharacterControllers character){
        character.CurrentSpeed = character.speedAfterBuff;
        character.IsSpeedUp = false;
    }

    public void Interaction(){
        AudioManager.instance.Play("Powerup Collect");
        GameManager.Instance.AddBuff(this);
        gameObject.SetActive(false);
        AudioManager.instance.Play("Character Boost");
    }
}