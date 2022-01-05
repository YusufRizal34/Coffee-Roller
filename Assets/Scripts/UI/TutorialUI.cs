using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour {
    public GameObject tutorial;
    public GameObject[] tutorialUI;
    private int _tutorialIndex;
    public float timeScale;

    private Touch theTouch;
    private float timeTouchEnded;

    void Start() {
        tutorial.SetActive(true);
        this.timeScale = Time.timeScale;   
    }
    void Update() {
        for(int i=0; i<tutorialUI.Length; i++) {
            if (i == tutorialUI.Length) {
                //Time.timeScale = 1;
                //tutorial.SetActive(false);
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
            if(Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0) {
                _tutorialIndex++;
            }
        }
        else {
            return;
        }
    }
}
