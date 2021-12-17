using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    [SerializeField] protected string itemName;
    [SerializeField] protected ItemCategory category;
    [SerializeField] [TextArea] protected string itemDescription;
    [SerializeField] protected int itemPrice;
    [SerializeField] protected int itemLevel;
    [SerializeField] protected int itemGet;

    public string Name{ get{ return itemName; } }
    public string Description{ get{ return itemDescription; } }
    public ItemCategory Category{ get{ return category; } }
    public int Price{ get{ return itemPrice; } }
    public int Level{ get{ return itemLevel; } set{ itemLevel = value; } }
    public int Get{ get{ return itemGet; } set{ itemGet = value; } }
}

public enum ItemCategory{
    Booster,
    PowerUp
}
