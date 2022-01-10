using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ShopItem", menuName = "Coffee-Roller/ShopItem", order = 0)]
public class ShopItem : ScriptableObject
{
    [SerializeField] protected string itemName;
    [SerializeField] protected ItemCategory category;
    [SerializeField] [TextArea] protected string itemDescription;
    [SerializeField] protected Sprite icon;
    [SerializeField] protected GameObject image;
    [SerializeField] protected double itemPrice;
    [SerializeField] protected int itemLevel;
    [SerializeField] protected int itemGet;
    public int[] levelPrice;

    public string Name{ get{ return itemName; } }
    public string Description{ get{ return itemDescription; } }
    public ItemCategory Category{ get{ return category; } }
    public Sprite Icon{ get{ return icon; } }
    public GameObject Image{ get{ return image; } }
    public double Price{ get{ return itemPrice; } }
    public int Level{ get{ return itemLevel; } set{ itemLevel = value; } }
    public int Get{ get{ return itemGet; } set{ itemGet = value; } }
}

public enum ItemCategory{
    Booster,
    PowerUp
}
