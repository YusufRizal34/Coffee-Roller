using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCharacter : MonoBehaviour
{
    private Button btn;
    public int panelNumber;
    public bool isSelected;

    private void Awake() {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(GetCurrentPanel);
    }

    private void GetCurrentPanel(){
        GameManager.Instance.CurrentCharacter(panelNumber);
        // SelectCharacter prevButton = FindObjectOfType<SelectCharacter>();
        // prevButton.ChangeSelectedCharacter();
        // btn.gameObject.GetComponent<Image>().color = new Color32(210,177,147,255);
    }

    // public void ChangeSelectedCharacter(){
    //     btn.gameObject.GetComponent<Image>().color = new Color32(255,255,255,255);
    // }
}
