using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour {
    public GameObject tutorial;
    public GameObject[] tutorialUI;
    private int _tutorialIndex;
    public float timeScale;
    void Start() {
        tutorial.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        this.timeScale = Time.timeScale;   
    }
    void Update() {
        for(int i=0; i<tutorialUI.Length; i++) {
            if (i == tutorialUI.Length) {
                //Time.timeScale = 1;
                //tutorial.SetActive(false);
                //Cursor.lockState = CursorLockMode.None;
            }
            else {
                 
                if (i == _tutorialIndex) {
                    Time.timeScale = 0;
                    tutorialUI[i].SetActive(true);
                }
                else {
                    tutorialUI[i].SetActive(false);
                    if(_tutorialIndex == tutorialUI.Length) {
                        Time.timeScale = 1;
                        Cursor.lockState = CursorLockMode.None;
                        tutorial.SetActive(false);
                    }
                }
            } 
        }

        if(_tutorialIndex!=tutorialUI.Length) {
            if(Input.GetKeyDown(KeyCode.Space)) {
                _tutorialIndex++;
            }
        }
        else {
            return;
        }
    }
}
