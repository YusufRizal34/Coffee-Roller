using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        // SetPosition(booster, sliderPanel, boosterSliderPanel);
        // SetPosition(powerUp, sliderPanel, powerUpSliderPanel);
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
                GameObject itemPanels = Instantiate(itemPanel, parentPanel.transform);
                // Button btn = itemPanels.GetComponent<Button>();
                // btn.onClick.AddListener(GameManager.AddBooster(items[i].Name, 1));
                SetContext(items, itemPanels, i);
            }
        }
        else{
            print("No Items");
        }
    }

    private void SetContext(List<ShopItem> items, GameObject panel, int currentItem){
        Text name  = panel.transform.Find("Item Title").GetComponent<Text>();
        name.text = items[currentItem].Name;
        Text price = panel.transform.Find("Item Price Panel/Item Price").GetComponent<Text>();
        price.text = items[currentItem].Price.ToString();
    }
}
