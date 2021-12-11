using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelection : MonoBehaviour
{
    [Header("CHARACTER LIST")]
    [SerializeField] private Character[] characters;
    
    public GameObject characterListPanel;
    public GameObject characterPanel;

    public int coins = 5000;
    public int currentSelection;
    public int CurrentSelection{ get{ return currentSelection; } }
    
    public Text coin;
    // public Image characterImage;
    public Text characterName;
    public Text characterPrice;
    public Text skillDescription;
    public bool isCharacterUnlock;
    public GameObject buyButton;

    private void Start() {
        CurrentCoin();

        if(characters.Length > 0){
            Initialize();
        }
        else{
            print("No Character Attached");
        }
    }

    private void SetPosition(GameObject child, GameObject parent){
        child.transform.SetParent(parent.transform);
        child.transform.position = parent.transform.position;
    }

    private void Update() {
        ChangeToCurrentCharacter(currentSelection);
        ChangeButtonOption(isCharacterUnlock);
    }

    private void Initialize(){
        currentSelection = 0;

        ChangeToCurrentCharacter(currentSelection);

        for(int i = 0; i < characters.Length; i++){
            GameObject panel = Instantiate(characterPanel, characterListPanel.transform);
            panel.GetComponent<SelectCharacter>().panelNumber = i;

            GameObject image = Instantiate(characters[i].Image);
            SetPosition(image, panel);
        }
    }

    private void ChangeToCurrentCharacter(int current){
        // characterImage.sprite       = characters[currentSelection].Image;
        characterName.text          = characters[currentSelection].Name;
        characterPrice.text         = characters[currentSelection].Price.ToString();
        skillDescription.text       = characters[currentSelection].SkillDescription;
        isCharacterUnlock           = characters[currentSelection].IsUnlock;
    }

    private void ChangeButtonOption(bool isCharacterUnlock){
        if(isCharacterUnlock == true){
            buyButton.SetActive(false);
        }
        else{
            buyButton.SetActive(true);
        }
    }

    public void BuyCharacter(){
        if(coins >= characters[currentSelection].Price){
            coins -= characters[currentSelection].Price;
            CurrentCoin();
            characters[currentSelection].IsUnlock = true;
        }
        else{
            print("NO U CANT!!!");
        }
    }

    private void CurrentCoin(){
        coin.text = coins.ToString();
    }
}
