using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected string characterName;
    [SerializeField] [TextArea] protected string characterDescription;
    [SerializeField] [TextArea] protected string skillDescription;
    [SerializeField] protected int characterPrice;
    [SerializeField] protected GameObject characterImage;
    [SerializeField] protected bool isUnlock;

    public string Name{ get{ return characterName; } }
    public string Description{ get{ return characterDescription; } }
    public string SkillDescription{ get{ return skillDescription; } }
    public int Price{ get{ return characterPrice; } }
    public GameObject Image{ get{ return characterImage; } }
    public bool IsUnlock{ get{ return isUnlock; } set{ isUnlock = value; } }

    protected virtual void CastSkill(){
        //CAST
    }
}
