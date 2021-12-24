using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Character : MonoBehaviour
{
    public int characterID;
    public string characterName;
    [TextArea] public string characterDescription;
    [TextArea] public string skillDescription;
    public int characterPrice;
    public GameObject characterImage;
    public bool isUnlock;

    public int ID{ get{ return characterID; } }
    public string Name{ get{ return characterName; } }
    public string Description{ get{ return characterDescription; } }
    public string SkillDescription{ get{ return skillDescription; } }
    public int Price{ get{ return characterPrice; } }
    public GameObject Image{ get{ return characterImage; } }
    public bool IsUnlock{ get{ return isUnlock; } set{ isUnlock = value; } }
}
