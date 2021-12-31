using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuffable
{
    float FinishTime{ get; set; }

    void Apply(CharacterControllers character);
    void Finished(CharacterControllers character);
}
