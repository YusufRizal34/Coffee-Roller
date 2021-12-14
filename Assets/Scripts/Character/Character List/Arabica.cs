using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arabica : Character
{
    private void Start() {
        CastSkill();
    }

    protected override void CastSkill(){
        GameManager.CastSkill(this.Name);
    }
}
