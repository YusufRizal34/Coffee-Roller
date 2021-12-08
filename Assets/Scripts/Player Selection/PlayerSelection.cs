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

    [SerializeField] private int currentSelection;
    public int CurrentSelection{ get{ return currentSelection; } }
    
    // public Image characterImage;
    public Text characterName;
    public Text characterDescription;
    public Text skillDescription;

    private void Start() {
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
    }

    private void Initialize(){
        currentSelection = 0;

        ChangeToCurrentCharacter(currentSelection);

        for(int i = 0; i < characters.Length; i++){
            GameObject panel = Instantiate(characterPanel, characterListPanel.transform);
            GameObject image = Instantiate(characters[i].Image);
            SetPosition(image, panel);
        }
    }

    private void ChangeToCurrentCharacter(int current){
        // characterImage.sprite       = characters[currentSelection].Image;
        characterName.text          = characters[currentSelection].Name;
        // characterDescription.text   = characters[currentSelection].Description;
        skillDescription.text       = characters[currentSelection].SkillDescription;
    }
}
