using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCharacter : MonoBehaviour
{
    private PlayerSelection gameManager;
    private Button btn;
    public int panelNumber;

    private void Awake() {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(GetCurrentPanel);
        gameManager = GameObject.Find("Game Manager").GetComponent<PlayerSelection>();;
    }

    private void GetCurrentPanel(){
        gameManager.currentSelection = panelNumber;
    }
}
