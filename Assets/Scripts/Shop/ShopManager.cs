using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public ShopItem[] items;
    private List<ShopItem> booster = new List<ShopItem>();
    private List<ShopItem> powerUp = new List<ShopItem>();

    public GameObject boosterPanel;
    public GameObject boosterSliderPanel;
    public GameObject powerUpSliderPanel;
    
    public GameObject itemPanel;
    public GameObject sliderPanel;

    private void Awake(){
        foreach(ShopItem item in items){
            if(item.Category == ItemCategory.Booster){
                booster.Add(item);
            }
            else{
                powerUp.Add(item);
            }
        }

        SetPosition(booster, itemPanel, boosterPanel);
        SetPosition(booster, sliderPanel, boosterSliderPanel);
        SetPosition(powerUp, sliderPanel, powerUpSliderPanel);
    }

    public void BuyItem(string itemName, int itemPrice){
        int coin = GameManager.ShowCoin();
        if(coin >= itemPrice){
            GameManager.AddCoin(-itemPrice);
            GameManager.AddBooster(itemName, 1);
        }
        else{
            print("Not Enough Money");
        }
    }

    public void LevelUp(string itemName, int itemPrice){
        int coin = GameManager.ShowCoin();
        if(coin >= itemPrice){
            GameManager.AddCoin(-itemPrice);
            GameManager.AddBooster(itemName, 1);
        }
        else{
            print("Not Enough Money");
        }
    }

    private void SetPosition(List<ShopItem> items, GameObject itemPanel, GameObject parentPanel){
        if(items != null){
            for(int i = 0; i < items.Count; i++){
                Instantiate(itemPanel, parentPanel.transform);
            }
        }
        else{
            print("No Items");
        }
    }
}
