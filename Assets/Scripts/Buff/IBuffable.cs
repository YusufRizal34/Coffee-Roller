using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuffable
{
    string BuffName{ get; }
    float FinishTime{ get; set; }

    void Apply(CharacterControllers character);
    void Finished(CharacterControllers character);
    // void Refresh(IBuffable buffs);
}
