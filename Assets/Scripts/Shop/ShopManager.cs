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

    public int maxTotalItem = 3; ///DEFAULT 3
    public int maxLevelItem = 5; ///DEFAULT 5

    public Slider scoreDoppioLevel;
    public Slider longBlackLevel;

    public Slider extraShotLevel;
    public Slider secondShotLevel;

    private void Awake(){
        foreach(ShopItem item in items){
            if(item.Category == ItemCategory.Booster){
                booster.Add(item);
            }
            else{
                powerUp.Add(item);
            }
        }

        if(booster != null){
            SetItemCollectionPosition(booster, itemPanel, boosterPanel);
            SetItemUpgradeItemPosition(booster, sliderPanel, boosterSliderPanel);
        }
        if(powerUp != null){
            SetItemUpgradeItemPosition(powerUp, sliderPanel, powerUpSliderPanel);
        }
    }

    private void Update() {
        UpdateSliderValue();
    }

    private void SetItemCollectionPosition(List<ShopItem> items, GameObject itemPanel, GameObject parentPanel){
        if(items != null){
            for(int i = 0; i < items.Count; i++){
                GameObject itemPanels = Instantiate(items[i].Image, parentPanel.transform);
                SetItemCollectionContext(items, itemPanels, i);
            }
        }
        else{
            // print("No Items");
        }
    }
    
    private void SetItemUpgradeItemPosition(List<ShopItem> items, GameObject itemPanel, GameObject parentPanel){
        if(items != null){
            for(int i = 0; i < items.Count; i++){
                GameObject itemPanels = Instantiate(itemPanel, parentPanel.transform);
                SetItemUpgradeContext(items, itemPanels, i);
            }
        }
        else{
            // print("No Items");
        }
    }

    private void SetItemCollectionContext(List<ShopItem> items, GameObject panel, int currentItem){
        Button btn = panel.GetComponent<Button>();
        btn.onClick.AddListener(() => BuyItem(items[currentItem].Name, items[currentItem].Price));
    }

    private void SetItemUpgradeContext(List<ShopItem> items, GameObject panel, int currentItem){
        UserDataManager.Load();
        Button btn = panel.transform.Find("Button").GetComponent<Button>();
        btn.onClick.AddListener(() => LevelUp(btn, items, currentItem));
        if(items[currentItem].Name == "Score Doppio"){
            scoreDoppioLevel = panel.transform.Find("Slider").GetComponent<Slider>();
        }
        else if(items[currentItem].Name == "Long Black"){
            longBlackLevel = panel.transform.Find("Slider").GetComponent<Slider>();
        }
        else if(items[currentItem].Name == "Extra Shot"){
            extraShotLevel = panel.transform.Find("Slider").GetComponent<Slider>();
        }
        else if(items[currentItem].Name == "Second Shot"){
            secondShotLevel = panel.transform.Find("Slider").GetComponent<Slider>();
        }
    }

    public void BuyItem(string itemName, double itemPrice){
        double coin = GameManager.Instance.ShowCoin();
        if(coin >= itemPrice){
            if(itemName == "Score Doppio" && GameManager.Instance.ShowTotalScoreDoppio() < maxTotalItem){
                GameManager.Instance.AddCoin(-itemPrice);
                GameManager.Instance.AddBooster(itemName, 1);
            }
            else if(itemName == "Long Black" && GameManager.Instance.ShowTotalLongBlack() < maxTotalItem){
                GameManager.Instance.AddCoin(-itemPrice);
                GameManager.Instance.AddBooster(itemName, 1);
            }
            else{
                // print("Not Enough Space");
            }
        }
        else{
            // print("Not Enough Money");
        }
    }

    public void LevelUp(Button btn, List<ShopItem> items, int currentItem){
        double coin = GameManager.Instance.ShowCoin();
        if(coin >= items[currentItem].Price){
            if(items[currentItem].Name == "Score Doppio" && GameManager.Instance.ShowLevelScoreDoppio() < maxLevelItem){
                GameManager.Instance.AddCoin(-items[currentItem].levelPrice[GameManager.Instance.ShowLevelScoreDoppio()]);
                GameManager.Instance.LevelUpItem(items[currentItem].Name, 1);
            }
            else if(items[currentItem].Name == "Long Black" && GameManager.Instance.ShowLevelLongBlack() < maxLevelItem){
                GameManager.Instance.AddCoin(-items[currentItem].levelPrice[GameManager.Instance.ShowLevelLongBlack()]);
                GameManager.Instance.LevelUpItem(items[currentItem].Name, 1);
            }
            else if(items[currentItem].Name == "Extra Shot" && GameManager.Instance.ShowLevelExtraShot() < maxLevelItem){
                GameManager.Instance.AddCoin(-items[currentItem].levelPrice[GameManager.Instance.ShowLevelExtraShot()]);
                GameManager.Instance.LevelUpItem(items[currentItem].Name, 1);
            }
            else if(items[currentItem].Name == "Second Shot" && GameManager.Instance.ShowLevelSecondShot() < maxLevelItem){
                GameManager.Instance.AddCoin(-items[currentItem].levelPrice[GameManager.Instance.ShowLevelSecondShot()]);
                GameManager.Instance.LevelUpItem(items[currentItem].Name, 1);
            }
            else{
                // print("Max Level");
            }
            
        }
        else{
            // print("Not Enough Money");
        }
    }

    private void UpdateSliderValue(){
        scoreDoppioLevel.value = GameManager.Instance.ShowLevelScoreDoppio();
        longBlackLevel.value = GameManager.Instance.ShowLevelLongBlack();
        extraShotLevel.value = GameManager.Instance.ShowLevelExtraShot();
        secondShotLevel.value = GameManager.Instance.ShowLevelSecondShot();
    }
}
