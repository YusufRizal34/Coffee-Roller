using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCharacter : MonoBehaviour
{
    private Button btn;
    public int panelNumber;

    private void Awake() {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(GetCurrentPanel);
    }

    private void GetCurrentPanel(){
        GameManager.Instance.CurrentCharacter(panelNumber);
    }
}
