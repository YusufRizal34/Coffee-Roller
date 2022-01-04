using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaffeineBoost : MonoBehaviour, IBuffable
{
    public float duration;
    public float FinishTime{ get{ return duration; } set{ duration = value; } }

    public void Apply(CharacterControllers character){
        character.Invisible = true;
    }

    public void Finished(CharacterControllers character){
        character.Invisible = false;
    }
}
