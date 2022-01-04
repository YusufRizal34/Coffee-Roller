using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaffeineBoost : IBuffable
{
    public float duration = 10f;
    public float FinishTime{ get{ return duration; } set{ duration = value; } }

    public void Apply(CharacterControllers character){
        character.Invisible = true;
    }

    public void Finished(CharacterControllers character){
        character.Invisible = false;
        GameManager.Instance.coinFromTrack = 0;
        GameManager.Instance.isSpecialMode = false;
    }
}
