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
    
    public Image characterImage;
    public Text characterName;
    public Text characterPrice;
    public Text skillDescription;
    public GameObject buyButton;

    private void Start() {
        if(characters.Length > 0){
            Initialize();
        }
        else{
            // print("No Character Attached");
        }
    }

    private void Update() {
        ChangeToCurrentCharacter(GameManager.Instance.ShowCurrentCharacter());
    }

    private void Initialize(){
        ChangeToCurrentCharacter(GameManager.Instance.ShowCurrentCharacter());

        for(int i = 0; i < characters.Length; i++){
            GameObject panel = Instantiate(characterPanel, characterListPanel.transform);
            panel.GetComponent<SelectCharacter>().panelNumber = characters[i].ID;

            Sprite image = Instantiate(characters[i].Button);
            SetPosition(image, panel);
        }
    }

    private void SetPosition(Sprite child, GameObject parent){
        parent.GetComponent<Image>().sprite = child;
    }

    private void ChangeToCurrentCharacter(int current){
        characterImage.sprite       = characters[current].Image;
        characterName.text          = characters[current].Name;
        characterPrice.text         = characters[current].Price.ToString();
        skillDescription.text       = characters[current].SkillDescription;
        ChangeButtonOption(GameManager.Instance.ShowUnlockCharacter(current));
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
        int currentID = GameManager.Instance.ShowCurrentCharacter();
        GameManager.Instance.CurrentCharacter(currentID);
        if(GameManager.Instance.ShowCoin() >= characters[currentID].Price){
            GameManager.Instance.AddCoin(-characters[currentID].Price);
            GameManager.Instance.UnlockCharacter(currentID);
            ChangeButtonOption(characters[currentID].IsUnlock);
        }
        else{
            // print("NO U CANT!!!");
        }
    }
}
